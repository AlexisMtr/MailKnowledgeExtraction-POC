using AutoMapper;
using AutoMapper.Configuration;
using Common;
using System.Linq;
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
            cfg.CreateMap<Document, DocumentDto>()
                .ForMember(d => d.IndustryTerms, opt => opt.MapFrom(s => s.IndustryTerms.Select(e => e.Value)))
                .ForMember(d => d.Technologies, opt => opt.MapFrom(s => s.Technologies.Select(e => e.Value)))
                .ForMember(d => d.Urls, opt => opt.MapFrom(s => s.Urls.Select(e => e.Value)))
                .ForMember(d => d.ProgrammingLanguages, opt => opt.MapFrom(s => s.ProgrammingLanguage.Select(e => e.Value)));

            Mapper.Initialize(cfg);
        }
    }
}
