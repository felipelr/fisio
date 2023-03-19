using AutoMapper;
using fisio.domain.Mappers.Interfaces;
using fisio.domain.Mappers.Profiles;

namespace fisio.domain.Mappers;

public class MapperConfig : IMapperConfig
{
    private MapperConfiguration Configuration;
    public IMapper Mapper { get; private set; }
    public MapperConfig()
    {
        Configuration = new MapperConfiguration(cfg =>
         {
             cfg.AddProfile<UserMapProfile>();
         });

        Mapper = Configuration.CreateMapper();
    }

    public IMapper GetMapper()
    {
        return Mapper;
    }
}