using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using CubosChallenge.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CubosChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountsController(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("{accountId}/cards")]
        public async Task<ActionResult<CardToReturnDTO>> AddCardToAccount(Guid accountId, CardForCreationDTO cardForCreationDTO)
        {
            var hasPhisicalCard = await _accountRepository.HasPhisicalCard(accountId);
            if (cardForCreationDTO.Type.Equals("fisico") && hasPhisicalCard)
                return BadRequest("A sua conta ja possui um cartao fisico cadastrado");

            try
            {
                if (cardForCreationDTO.Number.Any(x => char.IsLetter(x)) || cardForCreationDTO.Number.Length != 16)
                    throw new ArgumentException("Operação cancelada, o número do cartão informado não á válido: " + cardForCreationDTO.Number);

                if (cardForCreationDTO.Cvv.Any(x => char.IsLetter(x)) || cardForCreationDTO.Cvv.Length != 3)
                    throw new ArgumentException("Operação cancelada, o código de verificação do cartão informado não á válido: " + cardForCreationDTO.Cvv);

                if (!cardForCreationDTO.Type.Contains("fisico") && !cardForCreationDTO.Type.Contains("virtual"))
                    throw new ArgumentException("Operação cancelada. Tipo de cartão informado inválido");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var account = await _accountRepository.GetAccountAsync(accountId);
            cardForCreationDTO.AddOwnerId(account.PersonId, account.Id);

            var card = _mapper.Map<Card>(cardForCreationDTO);

            await _accountRepository.AddCardsAccountAsync(accountId, card);
            await _accountRepository.SaveChangesAsync();

            var cardToReturn = _mapper.Map<CardToReturnDTO>(card);
            return Ok(cardToReturn);
        }

        [HttpGet]
        [Route("{accountId}/cards")]
        public async Task<ActionResult<AccountWithCardsToReturnDTO>> GetAccountWithCards(Guid accountId)
        {
            if (!await _accountRepository.AccountExists(accountId))
                return BadRequest(accountId);

            var account = await _accountRepository.GetAccountAsync(accountId);
            var accountToReturn = _mapper.Map<AccountWithCardsToReturnDTO>(account);

            return Ok(accountToReturn);

        }

        [HttpPost]
        [Route("{accountId}/transaction")]
        public async Task<ActionResult<TransactionToReturnDTO>> PerformTransaction(Guid accountId, TransactionForCreationDTO transactionForCreationDTO)
        {
            if (!await _accountRepository.AccountExists(accountId))
                return BadRequest(accountId);

            var account = await _accountRepository.GetAccountAsync(accountId);

            if (account.Balance + transactionForCreationDTO.Value < 0)
                return BadRequest("Transação não altorizada. Saldo não suficiente \nSaldo atual: " + account.Balance);

            transactionForCreationDTO.AddAccountId(accountId);
            var transaction = _mapper.Map<Transaction>(transactionForCreationDTO);
            
            await _accountRepository.AddTransactionToAccountAsync(accountId, transaction);
            await _accountRepository.SaveChangesAsync();

            var transactionToReturn = _mapper.Map<TransactionToReturnDTO>(transaction);
            return Ok(transactionToReturn);
            
        }

        [HttpGet]
        [Route("{accountId}/transaction")]
        public async Task<ActionResult<IEnumerable<TransactionToReturnDTO>>> GetAccountTransactions(Guid accountId)
        {
            if (!await _accountRepository.AccountExists(accountId))
                return BadRequest(accountId);

            var transactions = await _accountRepository.GetAccountTransactionsAsync(accountId);
            var transactionToReturn = _mapper.Map<IEnumerable<TransactionToReturnDTO>>(transactions);

            return Ok(transactionToReturn);
        }

        [HttpGet]
        [Route("{accountId}/balance")]
        public async Task<ActionResult<Object>> GetAccountBallance(Guid accountId)
        {
            if (!await _accountRepository.AccountExists(accountId))
                return BadRequest(accountId);

            var account = await _accountRepository.GetAccountAsync(accountId);
            var balance = new
            {
                balance = account.Balance
            };

            return Ok(balance);
        }

        [HttpPost]
        [Route("{accountId}/transactions/{transactionId}/revert")]
        public async Task<ActionResult<TransactionToReturnDTO>> RevertTransaction(Guid accountId, Guid transactionId, string description)
        {
            if (!await _accountRepository.AccountExists(accountId))
                return BadRequest(accountId);

            if (!await _accountRepository.TransactionExists(transactionId))
                return BadRequest(transactionId);

            var transaction = await _accountRepository.GetAccountTransactionAsync(accountId, transactionId);
            TransactionForCreationDTO transactionForCreation = new()
            {
                Value = (transaction.Value * -1),
                Description = description,
                AccountId = accountId
            };
            
            var revertedTransaction = _mapper.Map<Transaction>(transactionForCreation);

            await _accountRepository.AddTransactionToAccountAsync(accountId, revertedTransaction);
            await _accountRepository.SaveChangesAsync();

            var transactionToReturn = _mapper.Map<TransactionToReturnDTO>(revertedTransaction);
            return Ok(transactionToReturn);
        }

    }
}
