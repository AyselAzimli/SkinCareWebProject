using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.BLL.Services
{
    public class BasketManager
    {
        private const string BasketCookieName = "basket";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductVariantService _productVariantService;
        private readonly IProductService _productService;

        public BasketManager(IHttpContextAccessor httpContextAccessor, IProductVariantService productVariantService, IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productVariantService = productVariantService;
            _productService = productService;
        }

        public void AddToBasket(int productVariantId, int quantity = 1)
        {
            var basket = GetBasketFromCookie();

            var basketItem = basket.FirstOrDefault(x => x.ProductVariantId == productVariantId);

            if (basketItem != null)
                basketItem.Quantity += quantity;
            else
                basket.Add(new BasketCookieItemViewModel
                {
                    ProductVariantId = productVariantId,
                    Quantity = quantity
                });

            SaveBasketToCookie(basket);
        }

        public void RemoveFromBasket(int productVariantId)
        {
            var basket = GetBasketFromCookie();
            var basketItem = basket.FirstOrDefault(x => x.ProductVariantId == productVariantId);
            if (basketItem != null)
            {
                basket.Remove(basketItem);
                SaveBasketToCookie(basket);
            }
        }

        public async Task<BasketViewModel> GetBasketAsync()
        {
            var basket = GetBasketFromCookie();
            var basketViewModel = new BasketViewModel();

            foreach (var item in basket)
            {
                // Get the variant
                var variant = await _productVariantService.GetAsync(x => x.Id == item.ProductVariantId);
                if (variant == null) continue;

                // Use variant.Product for name and id
                var product = variant.Product;
                if (product == null)
                {
                    // Optionally fetch product from ProductService if navigation property is null
                    var productVm = await _productService.GetByIdAsync(variant.ProductId);
                    if (productVm == null) continue;

                    product = new Product
                    {
                        Id = productVm.Id,
                        Name = productVm.Name ?? "Unknown"
                    };
                }

                basketViewModel.Items.Add(new BasketItemViewModel
                {
                    ProductId = product.Id,
                    ProductVariantId = variant.Id,  // assign it here!
                    ProductName = product.Name ?? "Unknown",
                    Price = variant.Price,
                    Quantity = item.Quantity
                });
            }

            return basketViewModel;
        }




        public async Task<BasketViewModel> ChangeQuantityAsync(int productVariantId, int change)
        {
            var basket = GetBasketFromCookie();
            var basketItem = basket.FirstOrDefault(x => x.ProductVariantId == productVariantId);

            if (basketItem != null)
            {
                basketItem.Quantity += change;

                if (basketItem.Quantity <= 0)
                    basket.Remove(basketItem);

                SaveBasketToCookie(basket);
            }

            return await GetBasketAsync();
        }


        private List<BasketCookieItemViewModel> GetBasketFromCookie()
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies[BasketCookieName];
            if (string.IsNullOrEmpty(cookie)) return new List<BasketCookieItemViewModel>();
            return JsonSerializer.Deserialize<List<BasketCookieItemViewModel>>(cookie) ?? new List<BasketCookieItemViewModel>();
        }

        private void SaveBasketToCookie(List<BasketCookieItemViewModel> basket)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                HttpOnly = true
            };
            var cookieValue = JsonSerializer.Serialize(basket);
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(BasketCookieName, cookieValue, cookieOptions);
        }

        public void CleanBasket()
        {
            var emptyBasket = new List<BasketCookieItemViewModel>();
            SaveBasketToCookie(emptyBasket);
        }

    }
}