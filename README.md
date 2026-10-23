# Deadlock Solutions Technical Assessment

Full-stack technical assessment built with ASP.NET Core, Entity Framework Core, SQL Server and vanilla JavaScript.

## Features

- Client registration and listing.
- Product catalog with stock tracking.
- Order creation with a minimum of two different products.
- Stock validation and transactional stock updates.
- Order item quantity updates.
- Stock restoration when an order item is removed.
- Invoice preview generated from an order.
- Basic frontend served from `wwwroot`.

## Architecture

The backend follows a simple layered structure:

- `Controllers`: HTTP endpoints and response codes.
- `Services`: business rules and asynchronous operations.
- `Data`: Entity Framework Core context and database initialization.
- `Models`: database entities.
- `DTOs`: request and response contracts.
- `wwwroot`: basic frontend using HTML, CSS and JavaScript.

## Requirements

- .NET 10 SDK
- SQL Server

## Running the project

1. Configure `ConnectionStrings:DefaultConnection` in `appsettings.json`.
2. Run the application:

```bash
dotnet run
```

3. Open the URL shown by ASP.NET Core. The frontend is available at `/`.
4. OpenAPI is available at `/openapi/v1.json` in development.

The application creates the database and initial products on startup when they do not exist.

## Main endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/clientes` | List clients |
| POST | `/api/clientes` | Create a client |
| GET | `/api/productos` | List products and stock |
| POST | `/api/ordenes` | Create an order |
| PATCH | `/api/ordenes/{ordenId}/items/{itemId}` | Update item quantity |
| DELETE | `/api/ordenes/{ordenId}/items/{itemId}` | Remove an item and restore stock |
| POST | `/api/ordenes/{ordenId}/factura` | Generate invoice preview |

## Example order request

```json
{
  "clienteId": 1,
  "items": [
    { "productoId": 1, "cantidad": 1 },
    { "productoId": 2, "cantidad": 2 }
  ]
}
```
