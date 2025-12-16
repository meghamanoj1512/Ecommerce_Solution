## Ecommerce Order API

### Tech Stack
- .NET 8
- Dapper
- SQL Server

### Endpoint
POST /api/orders/latest

### Notes
- Returns latest order by order date
- Handles gift items
- Returns order as null if no orders exist
