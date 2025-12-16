namespace Ecommerce_Solution.Domain
{
    public class OrderResponse
    {
        public CustomerDto Customer { get; set; }
        public OrderDto? Order { get; set; }
    }
    public class CustomerDto
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class OrderDto
    {
        public int OrderNumber { get; set; }
        public string OrderDate { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public List<OrderItemDto> OrderItems { get; set; } = new();
        public string DeliveryExpected { get; set; } = string.Empty;
    }
    public class OrderItemDto
    {
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceEach { get; set; }
    }


}
