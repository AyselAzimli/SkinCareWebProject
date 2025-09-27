using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.DataContext.Entities
{
    public class Social:Entity
    {
        public string IconClass { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
