using System;
using AutoMapper;
using MilkManagement.Core.Entities;
using MilkManagement.Web.ViewModels;

namespace MilkManagement.Web.Mapper
{
	public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly);
                cfg.AddProfile<MilkManagementProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class MilkManagementProfile:Profile
    {
        public MilkManagementProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProductName, act => act.MapFrom(src => src.Name)).ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
