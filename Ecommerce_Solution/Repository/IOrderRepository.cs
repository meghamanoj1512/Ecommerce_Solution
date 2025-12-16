using Ecommerce_Solution.Domain;

namespace Ecommerce_Solution.Repository
{
    public interface IOrderRepository
    {
        Task<CustomerDto?> GetCustomerAsync(string email, string customerId);
        Task<OrderDto?> GetLatestOrderAsync(string customerId);
    }
}
