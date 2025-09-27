using ECommerce.BLL.ViewModels;
using ECommerce.DataContext.Entities;
namespace ECommerce.BLL.Services.Contracts
{
    public interface ICategoryService : ICrudService<Category, CategoryViewModel, CreateCategoryViewModel, UpdateCategoryViewModel>
    {
    }
}
