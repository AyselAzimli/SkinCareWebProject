using AutoMapper;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;


namespace ECommerce.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, CreateCategoryViewModel>().ReverseMap();
            CreateMap<Category, UpdateCategoryViewModel>().ReverseMap();

            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, CreateProductViewModel>().ReverseMap();
            CreateMap<Product, UpdateProductViewModel>().ReverseMap();

            CreateMap<HomeViewModel, HomeViewModel>().ReverseMap();

            CreateMap<ProductVariant, ProductVariantViewModel>().ReverseMap();
            CreateMap<ProductVariant, CreateProductVariantViewModel>().ReverseMap();
            CreateMap<ProductVariant, UpdateProductVariantViewModel>().ReverseMap();

            CreateMap<ProductImage, ProductImageViewModel>().ReverseMap();
        }
    }
}
