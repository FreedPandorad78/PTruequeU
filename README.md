# TruequeU API

API desarrollada en **ASP.NET Core** para gestionar publicaciones, chats y reportes en una plataforma de trueque/compra-venta universitaria.

## Tecnologías usadas

- .NET (ASP.NET Core)
- Entity Framework Core
- SQL Server
- C#

## Características implementadas

- Registro e inicio de sesión de usuarios.
- Gestión de publicaciones (listings).
- Gestión de imágenes por publicación.
- Chats entre comprador y vendedor.
- Mensajes dentro de cada chat.
- Reportes de usuarios o publicaciones.
- Seed de datos de prueba para entorno de desarrollo.
- Persistencia con Entity Framework Core y SQL Server.

## Seguridad

### Seguridad implementada actualmente
- Control de acceso a endpoints autenticados.
- Asociación de datos por usuario (propietario de publicación, emisor de mensaje, reportante).
- Restricciones de integridad para evitar combinaciones duplicadas en chats.

### Seguridad planificada (siguiente fase)
- Autorización por roles (usuario/administrador).
- Políticas de autorización por endpoint.
- Validación de ownership (solo el dueño modifica su recurso).
- Endurecimiento de autenticación (tokens, expiración, refresh según diseño final).

---

## Requisitos previos

- .NET SDK instalado
- SQL Server activo
- `dotnet-ef` instalado globalmente (opcional pero recomendado)

```bash
dotnet tool install --global dotnet-ef
```

---

## Configuración y ejecución

1. Clonar repositorio:

```bash
git clone https://github.com/FreedPandorad78/PTruequeU.git
cd PTruequeU
```

2. Restaurar paquetes:

```bash
dotnet restore
```

3. Aplicar migraciones:

```bash
dotnet ef database update
```

4. Ejecutar la API:

```bash
dotnet run
```

---

## Seed de datos (desarrollo)

El proyecto incluye un `DbSeeder` que se ejecuta al iniciar la API.

### Importante
El seed requiere **mínimo 3 usuarios** en `AspNetUsers` para poder generar los chats esperados.

Si hay menos de 3 usuarios, el seed se omite de forma segura hasta que existan.

### Flujo recomendado

1. Ejecutar API
2. Registrar 3 usuarios
3. Reiniciar API
4. Verificar que el seed corrió

---

## Datos generados por seed

Cuando se cumplen las condiciones, se crean:

- **5** categorías
- **15** publicaciones
- **35** imágenes de publicaciones
- **20** chats
- **30** mensajes
- **10** reportes

---

## Verificación rápida en SQL

```sql
SELECT COUNT(*) AS UsersCount FROM AspNetUsers;
SELECT COUNT(*) AS CategoriesCount FROM Categories;
SELECT COUNT(*) AS ListingsCount FROM Listings;
SELECT COUNT(*) AS ListingImagesCount FROM ListingImages;
SELECT COUNT(*) AS ChatThreadsCount FROM ChatThreads;
SELECT COUNT(*) AS ChatMessagesCount FROM ChatMessages;
SELECT COUNT(*) AS ReportsCount FROM Reports;
```

Valores esperados (mínimo):
- UsersCount >= 3
- CategoriesCount = 5
- ListingsCount = 15
- ListingImagesCount = 35
- ChatThreadsCount = 20
- ChatMessagesCount = 30
- ReportsCount = 10

---

## Estructura general (resumen)

- `Data/`
  - `ApplicationDbContext.cs`
  - `DbSeeder.cs`
- `Models/`
  - Entidades del dominio
- `Controllers/`
  - Endpoints de la API
- `Program.cs`
  - Configuración general y ejecución del seed

---

## Notas

- El seed está diseñado para entorno de desarrollo/pruebas.
- Si la base ya tiene publicaciones, no vuelve a sembrar datos para evitar duplicados.
- La generación de chats respeta combinaciones únicas entre `Listing_Id`, `Buyer_Id` y `Seller_Id`.

---

## Autores

- GitHub: [@FreedPandorad78](https://github.com/FreedPandorad78)
- GitHub: [@tgarcesm](https://github.com/tgarcesm)
