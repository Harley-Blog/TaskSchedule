using AutoMapper;

namespace BC.TS.Domain.Mapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile<DoToPoMappingProfile>();
                config.AddProfile<PoToDoMappingProfile>();
            });
        }
    }
}
