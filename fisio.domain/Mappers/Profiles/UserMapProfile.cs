using AutoMapper;
using fisio.domain.Dtos;
using fisio.domain.Entities;

namespace fisio.domain.Mappers.Profiles;

public class UserMapProfile : Profile
{
    public UserMapProfile()
    {
        CreateMap<User, UserDto>();
    }
}