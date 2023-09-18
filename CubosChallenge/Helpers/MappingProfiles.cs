using AutoMapper;
using Core.Entities;
using CubosChallenge.DTOs;

namespace CubosChallenge.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<PersonForCreationDTO, Person>();
            CreateMap<Person, PersonToReturnDTO>();
        }
    }
}
