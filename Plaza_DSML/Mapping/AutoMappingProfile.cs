using AutoMapper;
using Plaza_DSML.Models;
using Plaza_DSML.Models.DTOs;

namespace Plaza_DSML.Mapping
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<Blog, UpdateBlogDto>().ReverseMap();
            CreateMap<BlogDto, UpdateBlogDto>().ReverseMap();

            CreateMap<Faq, FaqDto>().ReverseMap();

            CreateMap<Contact, ContactDto>().ReverseMap();

            CreateMap<Sponsor, SponsorDto>().ReverseMap();

            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Service, UpdateSponsorDto>().ReverseMap();
            CreateMap<ServiceDto, UpdateServiceDto>().ReverseMap();
        }
    }
}
