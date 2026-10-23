# Prueba técnica Deadlock Solutions

Aplicación full-stack desarrollada para la prueba técnica de Deadlock Solutions utilizando ASP.NET Core, Entity Framework Core, SQL Server y JavaScript vanilla.

## Funcionalidades

- Registro y consulta de clientes.
- Catálogo de productos con control de stock.
- Creación de órdenes con un mínimo de dos productos diferentes.
- Validación de stock y actualización transaccional del inventario.
- Actualización de cantidades dentro de una orden.
- Restauración del stock al eliminar un ítem de una orden.
- Generación de una vista previa de factura.
- Frontend básico servido desde `wwwroot`.

## Arquitectura

El backend utiliza una separación sencilla por responsabilidades:

- `Controllers`: endpoints HTTP y códigos de respuesta.
- `Services`: reglas de negocio y operaciones asíncronas.
- `Data`: contexto de Entity Framework Core e inicialización de la base de datos.
- `Models`: entidades de la base de datos.
- `DTOs`: contratos de entrada y salida.
- `wwwroot`: frontend básico con HTML, CSS y JavaScript.

## Requisitos

- .NET 10 SDK
- SQL Server

## Ejecución

1. Configurar `ConnectionStrings:DefaultConnection` en `appsettings.json`.
2. Ejecutar la aplicación:

```bash
dotnet run
```

3. Abrir la URL mostrada por ASP.NET Core. El frontend está disponible en `/`.
4. OpenAPI está disponible en `/openapi/v1.json` durante el entorno de desarrollo.

La aplicación crea la base de datos y los productos iniciales al iniciar, si todavía no existen.

## Endpoints principales

| Método | Endpoint | Descripción |
|---|---|---|
| GET | `/api/clientes` | Listar clientes |
| POST | `/api/clientes` | Registrar un cliente |
| GET | `/api/productos` | Listar productos y stock |
| POST | `/api/ordenes` | Crear una orden |
| PATCH | `/api/ordenes/{ordenId}/items/{itemId}` | Actualizar la cantidad de un ítem |
| DELETE | `/api/ordenes/{ordenId}/items/{itemId}` | Eliminar un ítem y restaurar el stock |
| POST | `/api/ordenes/{ordenId}/factura` | Generar una vista previa de factura |

## Ejemplo de solicitud para crear una orden

```json
{
  "clienteId": 1,
  "items": [
    { "productoId": 1, "cantidad": 1 },
    { "productoId": 2, "cantidad": 2 }
  ]
}
```

## Tecnologías utilizadas

- C# y ASP.NET Core
- Entity Framework Core
- SQL Server
- HTML, CSS y JavaScript vanilla
