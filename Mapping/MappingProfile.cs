using AutoMapper;
using OnlineShop.DTOs.Auth;
using OnlineShop.DTOs.Category;
using OnlineShop.DTOs.Product;
using OnlineShop.Models;
using OnlineShop.ViewModels.Auth;
using OnlineShop.ViewModels.Product;
using OnlineShop.ViewModels.Category;
using OnlineShop.Admin.ViewModels.Dashboard;
using OnlineShop.DTOs.Address;
using OnlineShop.Admin.DTOs.Dashboard;
using OnlineShop.Admin.DTOs.Category;
using OnlineShop.Admin.ViewModels.Category;
using OnlineShop.Admin.DTOs.Product;
using OnlineShop.Admin.ViewModels.Product;

namespace OnlineShop.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity → DTO
        CreateMap<Category, CategoryDto>();

        // DTO → Entity
        CreateMap<CreateCategoryDto, Category>();

        CreateMap<UpdateCategoryDto, Category>();

        CreateMap<ProductImage, ProductImageDto>();

        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<ProductDto, ProductDetailsVm>();

        CreateMap<ProductImageDto, ProductImageVm>();

        CreateMap<LoginVm, LoginDto>();

        CreateMap<RegisterVm, RegisterDto>();

        CreateMap<CategoryDto, CategoryVm>();

        CreateMap<CreateCategoryDto, CreateCategoryVm>();

        CreateMap<UpdateCategoryDto, UpdateCategoryVm>();

        CreateMap<CreateCategoryVm, CreateCategoryDto>();

        CreateMap<UpdateCategoryVm, UpdateCategoryDto>();
        
        // Address map

        CreateMap<Address, AddressDto>();

        CreateMap<CreateAddressDto, Address>();

        CreateMap<UpdateAddressDto, Address>();

        // Admin Dashbord map

        CreateMap<DashboardDto, DashboardVm>();

        CreateMap<DashboardStatsDto, DashboardStatsVm>();

        CreateMap<RecentOrderDto, RecentOrderVm>();

        CreateMap<LowStockProductDto, LowStockProductVm>();

        CreateMap<SalesChartDto, SalesChartVm>();

        // Admin Category

        CreateMap<CategoryAdminDto, CategoryItemAdminVm>();

        CreateMap<CreateCategoryAdminDto, CreateCategoryAdminVm>();

        CreateMap<UpdateCategoryAdminDto, UpdateCategoryAdminVm>();

        CreateMap<CategoryListAdminDto, CategoryListAdminVm>();

        CreateMap<UpdateCategoryAdminVm, UpdateCategoryAdminDto>();

        CreateMap<CategoryItemAdminVm, CategoryAdminDto>();

        CreateMap<CreateCategoryAdminVm, CreateCategoryAdminDto>();

        CreateMap<CategoryListAdminVm, CategoryListAdminDto>();

        // AdminCategoryDTO → Entity

        CreateMap<CreateCategoryAdminDto, Category>();

        CreateMap<UpdateCategoryAdminDto, Category>();

        // Admin Product

        CreateMap<ProductCreateVm, CreateProductAdminDto>();

        CreateMap<UpdateProductVm, UpdateProductDto>();

        CreateMap<ProductImageAdminVm, ProductImageAdminDto>();

        CreateMap<ProductAdminDto, ProductAdminVm>();

        CreateMap<ProductDto, ProductAdminDto>();

        CreateMap<ProductDto, ProductAdminVm>();

        CreateMap<ProductImageAdminDto, ProductImageAdminVm>();

        CreateMap<List<ProductImageAdminVm>, ProductImageAdminDto>();

        // AdminProductDTO → Entity

        CreateMap<CreateProductAdminDto, Product>().ForMember(dest => dest.Images, opt => opt.Ignore());;

    }
}