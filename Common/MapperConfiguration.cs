using AutoMapper;
using AutoMapper.Configuration;
using Common.Dtos;
using OC = OpenCalais.Objects;

namespace Common
{
    public class MapperConfiguration
    {
        public static bool MapperConfigured { get; set; }

        public static void ConfigureMapper()
        {
            if (MapperConfigured) return;

            var cfg = new MapperConfigurationExpression();

            cfg.CreateMap<OC.Topic, Topic>();
            cfg.CreateMap<OC.Entity, Entity>().ForMember(d => d.Value, opt => opt.MapFrom(s => s.Name));
            cfg.CreateMap<OC.SocialTag, Tag>()
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Importance, opt => opt.MapFrom(s => s.Importance));
            cfg.CreateMap<OC.Relations.ContactDetails, Contact>();

            Mapper.Initialize(cfg);
            MapperConfigured = true;
        }
    }
}
