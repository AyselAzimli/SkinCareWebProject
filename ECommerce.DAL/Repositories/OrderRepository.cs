using ECommerce.DAL.DataContext.Entities;
using ECommerce.DAL.DataContext;
using ECommerce.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using ECommerce.DAL.Repositories.Contracts;


namespace ECommerce.DAL.Repositories
{
    public class OrderRepository : EfCoreRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetUserOrdersAsync(string userId)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.AppUserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId, string userId)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.AppUserId == userId);
        }

        public async Task<List<Order>> GetOrdersByStatusAsync(string userId, OrderStatus status)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.AppUserId == userId && o.OrderStatus == status)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersWithUserAsync()
        {
            return await _dbContext.Orders
                .Include(o => o.AppUser)
                .Include(o => o.OrderItems)
                .Where(o => !o.IsDeleted)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdWithDetailsAsync(int orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.AppUser)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}