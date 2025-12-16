using Ecommerce_Solution.Domain;
using Ecommerce_Solution.Repository;

namespace Ecommerce_Solution.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderResponse?> GetLatestOrderAsync(OrderRequest request)
        {
            var customer = await _repository.GetCustomerAsync(
                request.User, request.CustomerId);

            if (customer == null)
                return null;

            var order = await _repository.GetLatestOrderAsync(request.CustomerId);

            return new OrderResponse
            {
                Customer = customer,
                Order = order
            };
        }
    }

}
