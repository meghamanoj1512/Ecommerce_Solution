using Dapper;
using Ecommerce_Solution.Domain;
using Ecommerce_Solution.Repository;
using System.Data;
using Microsoft.Data.SqlClient;


public class OrderRepository : IOrderRepository
{
    private readonly IConfiguration _configuration;

    public OrderRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<CustomerDto?> GetCustomerAsync(string email, string customerId)
    {
        var sql = """
            SELECT FIRSTNAME AS FirstName, LASTNAME AS LastName
            FROM CUSTOMERS
            WHERE EMAIL = @Email AND CUSTOMERID = @CustomerId
        """;

        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<CustomerDto>(
            sql, new { Email = email, CustomerId = customerId });
    }

    public async Task<OrderDto?> GetLatestOrderAsync(string customerId)
    {
        var orderSql = """
            SELECT TOP 1 
                o.ORDERID,
                o.ORDERDATE,
                o.DELIVERYEXPECTED,
                c.HOUSENO + ' ' + c.STREET + ', ' + c.TOWN + ', ' + c.POSTCODE AS DeliveryAddress
            FROM ORDERS o
            JOIN CUSTOMERS c ON o.CUSTOMERID = c.CUSTOMERID
            WHERE o.CUSTOMERID = @CustomerId
            ORDER BY o.ORDERDATE DESC
        """;

        using var connection = CreateConnection();
        var order = await connection.QueryFirstOrDefaultAsync<dynamic>(
            orderSql, new { CustomerId = customerId });

        if (order == null)
            return null;

        var itemsSql = """
            SELECT 
                CASE WHEN o.CONTAINSGIFT = 1 THEN 'Gift' ELSE p.PRODUCTNAME END AS Product,
                oi.QUANTITY AS Quantity,
                oi.PRICE AS PriceEach
            FROM ORDERITEMS oi
            JOIN PRODUCTS p ON oi.PRODUCTID = p.PRODUCTID
            JOIN ORDERS o ON oi.ORDERID = o.ORDERID
            WHERE oi.ORDERID = @OrderId
        """;

        var items = (await connection.QueryAsync<OrderItemDto>(
            itemsSql, new { OrderId = order.ORDERID })).ToList();

        return new OrderDto
        {
            OrderNumber = order.ORDERID,
            OrderDate = ((DateTime)order.ORDERDATE).ToString("dd-MMM-yyyy"),
            DeliveryExpected = ((DateTime)order.DELIVERYEXPECTED).ToString("dd-MMM-yyyy"),
            DeliveryAddress = order.DeliveryAddress,
            OrderItems = items
        };
    }
}
