using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
namespace ECommerce.BLL.Services.Contracts
{
    public interface ICategoryService : ICrudService<Category, CategoryViewModel, CreateCategoryViewModel, UpdateCategoryViewModel>
    {
    }
}
