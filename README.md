# SumoSushi API

API REST de un restaurante de sushi, desarrollada en C# con ASP.NET Core 8. Los clientes pueden consultar la información del restaurante y gestionar su carrito. El personal con rol de administrador puede actualizar los datos del restaurante.

## Funcionalidades

| Operación | Ruta | Acceso |
|---|---|---|
| Consultar el restaurante | `GET /api/Rest/ObtenerInfoRestaurante` | Público |
| Actualizar el restaurante | `PUT /api/Rest/ActualizarInfoRestaurante` | Administrador |
| Agregar un artículo al carrito | `POST /api/Carrito/Agregar` | Usuario autenticado |
| Consultar el carrito propio | `GET /api/Carrito/Obtener` | Usuario autenticado |

## Cómo funciona el carrito

Al agregar un artículo, la API identifica al cliente a partir de su autenticación. La solicitud debe indicar exactamente una presentación de producto o una promoción. Si incluye las dos opciones o deja ambas vacías, el controlador devuelve `400 Bad Request`. Si cumple la regla, delega la operación al servicio del carrito.

## Tecnologías

C#, ASP.NET Core 8, MySQL, Dapper, JWT y Swagger.

## Recorrido por el código

- [Controlador del carrito](SumoSushiApp-BackEnd/SumoSushiApp-BackEnd/Controllers/CarritoController.cs). Muestra la validación de artículos y la consulta del carrito del cliente.
- [Controlador del restaurante](SumoSushiApp-BackEnd/SumoSushiApp-BackEnd/Controllers/RestController.cs). Muestra cómo se permite la consulta pública y se restringe la actualización al rol administrador.
