using ECommerce.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL.Services.Contracts
{
    public interface IHomeService
    {
        Task<HomeViewModel> GetHomeViewModel();
    }
}
