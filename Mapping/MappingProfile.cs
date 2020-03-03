using AutoMapper;
using ExTrackAPI.Dto;
using ExTrackAPI.Models;

namespace ExTrackAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryForUpdateDto, Category>();

            CreateMap<TransactionForCreationDto, Transaction>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionForUpdateDto, Transaction>();
            CreateMap<Transaction, TransactionForDetailDto>();

            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}