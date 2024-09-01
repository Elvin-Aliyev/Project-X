using AutoMapper;
using Project_X.Models.Domain;
using Project_X.Models.DTO;

namespace Project_X.Mappings
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Faq, FaqsDto>().ReverseMap();
            CreateMap<AddFaqRequestDto, Faq>().ReverseMap();
            CreateMap<UpdateFaqRequestDto, Faq>().ReverseMap();
            CreateMap<UpdateFaqRequestDto, FaqsDto>().ReverseMap();

            CreateMap<Sponsors, SponsorsDto>().ReverseMap();

            CreateMap<BCategory, BCategoryDto>().ReverseMap();

            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<Blog, AddBlogRequestDto>().ReverseMap();
            CreateMap<Blog, UpdateBlogRequestDto>().ReverseMap();

            CreateMap<Contact, ContactDto>().ReverseMap();
        }
    }
}
