using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BLL.Services
{
    public class ShopManager : IShopService
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;


        public ShopManager(ICategoryService categoryService, IProductService productService, IBrandService brandService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _brandService = brandService;
        }

        public async Task<ShopViewModel> GetShopViewModelAsync(
    int? categoryId,
    int? brandId,
    string? search,
    int page
)
        {
            const int pageSize = 8;

            var categories = await _categoryService.GetAllAsync(x => !x.IsDeleted);
            var brands = await _brandService.GetAllAsync(x => !x.IsDeleted);

            var productsQuery = (await _productService.GetAllAsync(
                predicate: x => !x.IsDeleted,
                include: q => q
                    .Include(p => p.Images)
                    .Include(p => p.Variants)
                    .Include(p => p.Brand!)
                    .Include(p => p.Category!)
            )).AsQueryable();

            if (categoryId.HasValue)
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);

            if (brandId.HasValue)
                productsQuery = productsQuery.Where(p => p.BrandId == brandId);

            if (!string.IsNullOrWhiteSpace(search))
                productsQuery = productsQuery
                    .Where(p => p.Name!.ToLower().Contains(search.ToLower().Trim()));

            var totalProducts = productsQuery.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            var products = productsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new ShopViewModel
            {
                Products = products,
                Categories = categories.ToList(),
                Brands = brands.ToList(),

                SelectedCategoryId = categoryId,
                SelectedBrandId = brandId,
                Search = search,

                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };
        }


    }
}
