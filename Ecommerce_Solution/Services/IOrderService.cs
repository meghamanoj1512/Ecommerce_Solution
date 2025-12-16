using Ecommerce_Solution.Domain;

namespace Ecommerce_Solution.Services
{
    public interface IOrderService
    {
        Task<OrderResponse?> GetLatestOrderAsync(OrderRequest request);
    }
}
