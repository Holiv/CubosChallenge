using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.SpecificationParams;
using CubosChallenge.DTOs;
using CubosChallenge.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CubosChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IMapper _mapper;

        public PeopleController(IPeopleRepository peopleRepository, IMapper mapper)
        {
            _peopleRepository = peopleRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonToReturnDTO>> CreatePerson(PersonForCreationDTO personForCreationDTO) 
        {
            if(personForCreationDTO.Document.Length is 11)
            {
                if (!CPFValidation.IsCPFValid(personForCreationDTO.Document))
                    return BadRequest("O documento informado não é válido: " + personForCreationDTO.Document);
            } else if (personForCreationDTO.Document.Length is 14)
            {
                if (!CNPJValidation.IsCNPJValid(personForCreationDTO.Document))
                    return BadRequest("O documento informado não é válido: " + personForCreationDTO.Document);
            }
            else
            {
                return BadRequest("O documento informado não é válido: " + personForCreationDTO.Document);
            }

            if (await _peopleRepository.DocumentExistis(personForCreationDTO.Document))
                return BadRequest("Cadastro para o documento informado ja existente: " + personForCreationDTO.Document);

            var person = _mapper.Map<Person>(personForCreationDTO);
            try
            {
                await _peopleRepository.AddPersonAsync(person);
                await _peopleRepository.SaveChangesAsync();
                var personToReturn = _mapper.Map<PersonToReturnDTO>(person);
                return Ok(personToReturn);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPost]
        [Route("{peopleId}/accounts")]
        public async Task<ActionResult<AccountToReturnDTO>> CreatePersonAccount(AccountForCreationDTO accountForCreationDTO, Guid peopleId)
        {
            if (!await _peopleRepository.PersonExists(peopleId))
                return NotFound(peopleId);
            try
            {
                if (accountForCreationDTO.Branch.Any(x => char.IsLetter(x)))
                    throw new ArgumentException(accountForCreationDTO.Branch);

                if (accountForCreationDTO.AccountNumber.Any(x => char.IsLetter(x)))
                    throw new ArgumentException(accountForCreationDTO.AccountNumber);
            }
            catch(ArgumentException ex)
            {
                return BadRequest("Operação invalida, letras não são permitidas. Valor informado: " + ex.Message);
            }
           

            var account = _mapper.Map<Account>(accountForCreationDTO);

            try
            {
                await _peopleRepository.AddPersonAccountAsync(peopleId, account);
                await _peopleRepository.SaveChangesAsync();

                var accountToReturn = _mapper.Map<AccountToReturnDTO>(account);
                return Ok(accountToReturn);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonToReturnDTO>>> GetPeople()
        {
            var people = await _peopleRepository.GetPeopleAsync();
            var peopleToReturn = _mapper.Map<IEnumerable<PersonToReturnDTO>>(people);
            return Ok(peopleToReturn);
        }

        [HttpGet]
        [Route("{peopleId}/accounts")]
        public async Task<ActionResult<IEnumerable<AccountToReturnDTO>>> GetPersonAccounts(Guid peopleId)
        {
            if(!await _peopleRepository.PersonExists(peopleId))
                return NotFound(peopleId);

            var accounts = await _peopleRepository.GetPersonAccountsAsync(peopleId);
            var accountsToReturn = _mapper.Map<IEnumerable<AccountToReturnDTO>>(accounts);

            return Ok(accountsToReturn);
        }

        [HttpGet]
        [Route("{peopleId}/cards")]
        public async Task<ActionResult<IEnumerable<CardToReturnDTO>>> GetPersonCards(Guid peopleId, [FromQuery] SpecParams specParams)
        {
            if (!await _peopleRepository.PersonExists(peopleId))
                return NotFound(peopleId);

            var paginationEvaluator = new SpecParamsEvaluator(specParams);

            var cards = await _peopleRepository.GetPersonCardsAsync(peopleId, paginationEvaluator);
            var cardsToReturn = _mapper.Map<IEnumerable<CardToReturnDTO>>(cards);

            //Adicionar Paginacao e retornar objecto com duas propriedades, sendo a 1 a lista de cardsToReturn e a 2 os dados da paginacao.

            return Ok(new
            {
                cards = cardsToReturn,
                pagination = new
                {
                    itemsPerPage = specParams.ItemsPerPage,
                    currentPage = specParams.CurrentPage
                }
            });
        }
    }
}
