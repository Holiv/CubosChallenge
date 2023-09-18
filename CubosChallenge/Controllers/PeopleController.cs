using AutoMapper;
using Core.Entities;
using Core.Interfaces;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonToReturnDTO>>> GetPeople()
        {
            var people = await _peopleRepository.GetPeopleAsync();
            var peopleToReturn = _mapper.Map<IEnumerable<PersonToReturnDTO>>(people);
            return Ok(peopleToReturn);
        }
    }
}
