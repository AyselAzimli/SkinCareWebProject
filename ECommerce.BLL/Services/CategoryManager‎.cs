using AutoMapper;
using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using ECommerce.DAL.Repositories.Contracts;


namespace ECommerce.BLL.Services
{
    public class CategoryManager : CrudManager<Category, CategoryViewModel, CreateCategoryViewModel, UpdateCategoryViewModel>, ICategoryService
    {
        public CategoryManager(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<IEnumerable<CategoryViewModel>> GetActiveCategoriesAsync()
        {
            return await GetAllAsync(c => !c.IsDeleted); 
        }
    }
}
