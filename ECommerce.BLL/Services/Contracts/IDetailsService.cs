using ECommerce.BLL.ViewModels;
using System.Threading.Tasks;

namespace ECommerce.BLL.Services.Contracts
{
    public interface IDetailsService
    {
        Task<DetailsViewModel> GetServiceViewModel();
    }
}
