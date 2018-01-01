using AutoMapper;
using AutoMapper.Configuration;
using Common;
using WabAPI.Dtos;

namespace WabAPI
{
    public class MapperConfig
    {
        public static void ConfigureMapper()
        {
            var cfg = new MapperConfigurationExpression();

            cfg.CreateMap<MailItem, MailItemDto>()
                .ForMember(d => d.AttachmentsCount, opt => opt.MapFrom(e => e.Attachments.Count));

            Mapper.Initialize(cfg);
        }
    }
}
