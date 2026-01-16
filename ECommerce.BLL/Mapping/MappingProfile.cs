using AutoMapper;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using ECommerce.Web.Models;


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
            CreateMap<ShopViewModel, ShopViewModel>().ReverseMap();

            CreateMap<ProductVariant, ProductVariantViewModel>().ReverseMap();
            CreateMap<ProductVariant, CreateProductVariantViewModel>().ReverseMap();
            CreateMap<ProductVariant, UpdateProductVariantViewModel>().ReverseMap();

            CreateMap<ProductImage, ProductImageViewModel>().ReverseMap();

            CreateMap<Bio, BioViewModel>().ReverseMap();
            CreateMap<Bio, BioCreateViewModel>().ReverseMap();
            CreateMap<Bio, BioUpdateViewModel>().ReverseMap();

            CreateMap<Social, SocialViewModel>().ReverseMap();
            CreateMap<Social, CreateSocialViewModel>().ReverseMap();
            CreateMap<Social, UpdateSocialViewModel>().ReverseMap();

            CreateMap<Currency, CurrencyViewModel>().ReverseMap();
            CreateMap<Currency, CreateCurrencyViewModel>().ReverseMap();
            CreateMap<Currency, UpdateCurrencyViewModel>().ReverseMap();

            CreateMap<Language, LanguageViewModel>().ReverseMap();
            CreateMap<Language, CreateLanguageViewModel>().ReverseMap();
            CreateMap<Language, UpdateLanguageViewModel>().ReverseMap();

            CreateMap<UpdateProductViewModel, Product>()
            .ForMember(dest => dest.CoverImageName, opt => opt.Ignore()) // Don't map the file
            .ForMember(dest => dest.Images, opt => opt.Ignore()); // Don't map image files

            CreateMap<Brand, BrandViewModel>().ReverseMap();
            CreateMap<Brand, CreateBrandViewModel>().ReverseMap();
            CreateMap<Brand, UpdateBrandViewModel>().ReverseMap();

            CreateMap<CreateProductViewModel, Product>()
                .ForMember(dest => dest.CoverImageName, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<CreateCategoryViewModel, Category>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<UpdateCategoryViewModel, Category>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());


            CreateMap<Product, ProductViewModel>()
     .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
     .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null));

            CreateMap<ProductImage, ProductImageViewModel>();

            CreateMap<ProductVariant, ProductVariantViewModel>();

            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Address, AddressCreateViewModel>().ReverseMap();
            CreateMap<Address, AddressUpdateViewModel>().ReverseMap();
            // Orders
            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.AppUserName, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.UserName : ""));
            CreateMap<Order, OrderCreateViewModel>().ReverseMap();
            CreateMap<Order, OrderUpdateViewModel>().ReverseMap();

            // Order Items
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemCreateViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemUpdateViewModel>().ReverseMap();

            // Order Detail (optional if you also have OrderDetail entity)
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemCreateViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemUpdateViewModel>().ReverseMap();

        }
    }
}
