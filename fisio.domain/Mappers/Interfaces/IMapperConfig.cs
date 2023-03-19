using AutoMapper;

namespace fisio.domain.Mappers.Interfaces;
public interface IMapperConfig
{
    IMapper GetMapper();
}