using AutoMapper;
using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using ECommerce.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL.Services
{
    public class ProductVariantManager
      : CrudManager<ProductVariant,
                    ProductVariantViewModel,
                    CreateProductVariantViewModel,
                    UpdateProductVariantViewModel>,
        IProductVariantService
    {
        public ProductVariantManager(
            IRepository<ProductVariant> repository,
            IMapper mapper
        ) : base(repository, mapper)
        {
        }

        public Task<List<ProductViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CreateProductViewModel> GetCreateViewModelAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UpdateProductViewModel> GetUpdateViewModelAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
