﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Store.Contracts;
using Store.Contracts.ViewModel;
using Store.Data.EF.Entities;
using Store.Data.Entities.Identity;

namespace GC5.Application.AutoMapper
{
    public class MapperExpression
    {
        

        public  Action<IMapperConfigurationExpression> MapperDomainProfile( IServiceCollection services)
        {
            // Build the intermediate service provider
            var sp = services.BuildServiceProvider();


            StorageHelper storageHelper = sp.GetService<StorageHelper>();

            return (x) =>
            {
                

                x.CreateMap<ApplicationUser, ApplicationUserViewModel>()
                    .ReverseMap();


               x. CreateMap<RegisterUserViewModel, ApplicationUser>()
                  .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EUserStatus.Blocked))
                  .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                  .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                  .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company))
                  .ForMember(dest => dest.CompanyAddress, opt => opt.MapFrom(src => src.CompanyAddress))
                  .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                 ;

               x.CreateMap<MyAccountViewModel, ApplicationUser>()
                 .ForMember(dest => dest.UserName, opt => opt.Ignore())
                 .ForMember(dest => dest.Status, opt => opt.Ignore())
                 .ForMember(a => a.Email, opt => opt.Ignore())
                 .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))

                 .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                 .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company))
                 .ForMember(dest => dest.CompanyAddress, opt => opt.MapFrom(src => src.CompanyAddress))

                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                 .ReverseMap()
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))

                ;

                x.CreateMap<ApplicationRole, ApplicationRoleViewModel>()
                    .ReverseMap();



               x. CreateMap<Category, CategoryViewModel>()
                    .ForMember(a => a.Logo, opt => opt.MapFrom(s => new ContentViewModel() { Name = s.Logo }))
                    .ForMember(a => a.SubCategories, opt => opt.Ignore())
                    .ForMember(a => a.ParentCategory, opt => opt.Ignore())
                    .ReverseMap().ForMember(a => a.Logo, opt => opt.MapFrom(s => s.Logo.Name ?? null))
                    ;

                x.CreateMap<GalleryImage, GalleryImageViewModel>()
                    .ReverseMap();

               x.CreateMap<File, FileViewModel>()
                    .ReverseMap();

                x.CreateMap<Product, ProductViewModel>()
                    .ReverseMap();


               x.CreateMap<Movie, MovieViewModel>()
                    .ReverseMap();

               x.CreateMap<AcceptanceFormula, AcceptanceFormulaViewModel>()
                    .ReverseMap();

               x.CreateMap<Page, PageViewModel>()
                    .ReverseMap();


                x.CreateMap<ProductFile, ProductFileViewModel>()
                   .ReverseMap();

                //  CreateMap(typeof(DataTableSearchViewModel<>), typeof(HtmlResultViewModel)).ForMember(nameof(HtmlResultViewModel.HtmlResult), opt=> opt.Ignore());
            };
        }
    }
}