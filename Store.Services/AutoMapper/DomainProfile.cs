using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Store.Contracts;
using Store.Contracts.ViewModel;
using Store.Data.EF.Entities;
using Store.Data.Entities.Identity;

namespace GC5.Application.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ReverseMap();


            CreateMap<RegisterUserViewModel, ApplicationUser>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EUserStatus.Blocked))
              .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src =>src.Name))
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
              .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company))
              .ForMember(dest => dest.CompanyAddress, opt => opt.MapFrom(src => src.CompanyAddress))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
             ;

            CreateMap<MyAccountViewModel, ApplicationUser>()
             .ForMember(dest => dest.UserName, opt => opt.Ignore())
             .ForMember(dest => dest.Status, opt => opt.Ignore())
             .ForMember(a=>a.Email, opt=> opt.Ignore())
             .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
             
             .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
             .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company))
             .ForMember(dest => dest.CompanyAddress, opt => opt.MapFrom(src => src.CompanyAddress))
                      
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
             .ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                
            ;

            CreateMap<ApplicationRole, ApplicationRoleViewModel>()
                .ReverseMap();



            CreateMap<Category, CategoryViewModel>()
                .ForMember(a=>a.Logo, opt=>opt.MapFrom(s =>new ImageViewModel() { Name= s.Logo }))
                .ReverseMap();

            CreateMap<GalleryImage, GalleryImageViewModel>()
                .ReverseMap();

            CreateMap<File, FileViewModel>()
                .ReverseMap();

            CreateMap<Product, ProductViewModel>()
                .ReverseMap();


            CreateMap<Movie, MovieViewModel>()
                .ReverseMap();

            CreateMap<AcceptanceFormula, AcceptanceFormulaViewModel>()
                .ReverseMap();

            CreateMap<Page, PageViewModel>()
                .ReverseMap();

     
            CreateMap<ProductFile, ProductFileViewModel>()
               .ReverseMap();

          //  CreateMap(typeof(DataTableSearchViewModel<>), typeof(HtmlResultViewModel)).ForMember(nameof(HtmlResultViewModel.HtmlResult), opt=> opt.Ignore());
        }
    }
}