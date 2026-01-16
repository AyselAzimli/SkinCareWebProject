using ECommerce.DAL.DataContext;
using ECommerce.DAL.DataContext.Entities;
using ECommerce.DAL.Repositories.Contracts;

namespace ECommerce.DAL.Repositories
{

    public class OrderItemRepository : EfCoreRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext) { }
    }


}
