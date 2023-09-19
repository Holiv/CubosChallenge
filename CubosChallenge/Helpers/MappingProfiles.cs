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
            CreateMap<AccountForCreationDTO, Account>();
            CreateMap<Account, AccountToReturnDTO>();
            CreateMap<Card, CardToReturnDTO>();
            CreateMap<CardForCreationDTO, Card>();
            CreateMap<Account, AccountWithCardsToReturnDTO>();
            CreateMap<TransactionForCreationDTO, Transaction>();
            CreateMap<Transaction, TransactionToReturnDTO>();
        }
    }
}
