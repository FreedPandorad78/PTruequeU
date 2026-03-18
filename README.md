# TruequeU API

API desarrollada en ASP.NET Core para gestionar publicaciones, chats y reportes en una plataforma de trueque/compra-venta universitaria.

---

## Tecnologías usadas

- .NET 10 (ASP.NET Core)
- Entity Framework Core
- SQL Server
- C#
- Scalar (documentación interactiva)

---

## Características implementadas

- Registro e inicio de sesión de usuarios con JWT.
- Gestión de publicaciones (listings).
- Gestión de imágenes por publicación.
- Chats entre comprador y vendedor.
- Mensajes dentro de cada chat.
- Reportes de usuarios o publicaciones.
- Seed de datos de prueba mediante `HasData` en `OnModelCreating`.
- Persistencia con Entity Framework Core y SQL Server.

---

## Seguridad

### Implementada actualmente
- Autenticación con JWT (JSON Web Tokens).
- Control de acceso a endpoints autenticados.
- Asociación de datos por usuario (propietario de publicación, emisor de mensaje, reportante).
- Restricciones de integridad para evitar combinaciones duplicadas en chats.

### Planificada (siguiente fase)
- Autorización por roles (usuario/administrador).
- Políticas de autorización por endpoint.
- Validación de ownership (solo el dueño modifica su recurso).
- Endurecimiento de autenticación (expiración, refresh tokens).

---

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server activo
- `dotnet-ef` instalado globalmente (opcional pero recomendado)
```bash
dotnet tool install --global dotnet-ef
```

---

## Configuración

### 1. Clonar repositorio
```bash
git clone https://github.com/FreedPandorad78/PTruequeU.git
cd PTruequeU
```

### 2. Configurar `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=PTruequeU;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "tu-clave-secreta-minimo-32-caracteres",
    "Issuer": "PTruequeU",
    "Audience": "PTruequeU"
  }
}
```

### 3. Restaurar paquetes
```bash
dotnet restore
```

### 4. Ejecutar la API
```bash
dotnet run
```

> Las migraciones y el seed se aplican automáticamente al iniciar la API.

---

## Documentación interactiva (Scalar)

Una vez ejecutada la API, acceder a:
```
https://localhost:{puerto}/scalar/v1
```

### Cómo autorizarse en Scalar

1. Ejecutar el endpoint `POST /auth/login` con las credenciales de prueba.
2. Copiar el token JWT que devuelve la respuesta.
3. En el endpoint que se quiera probar, expandirlo y buscar la sección **Headers**.
4. Agregar el siguiente header:

| Key           | Value                      |
|---------------|----------------------------|
| Authorization | Bearer eyJhbGci...tu_token |

5. Ejecutar el request.

---

## Pruebas con Postman (alternativa)

Como alternativa a Scalar, se puede usar [Postman](https://www.postman.com/downloads/) para probar los endpoints.

### Flujo de autenticación en Postman

**Paso 1 — Obtener el token:**
```
POST https://localhost:{puerto}/auth/login
```
```json
{
  "email": "andrea@email.com",
  "password": "Test1234!"
}
```

**Paso 2 — Usar el token en requests protegidos:**

En cada request, ir a la pestaña **Authorization**:
- Type: `Bearer Token`
- Token: pegar el JWT copiado del paso anterior

**Paso 3 — Ejecutar cualquier endpoint protegido**, por ejemplo:
```
GET https://localhost:{puerto}/chats
GET https://localhost:{puerto}/favorites
GET https://localhost:{puerto}/api/Listings
```

---

## Credenciales de prueba (seed)

Los siguientes usuarios se crean automáticamente con el seed:

| Usuario | Email            | Contraseña |
|---------|------------------|------------|
| andrea  | andrea@email.com | Test1234!  |
| bruno   | bruno@email.com  | Test1234!  |
| carla   | carla@email.com  | Test1234!  |

---

## Seed de datos

El proyecto incluye seed automático mediante `HasData` en `OnModelCreating`. No requiere pasos adicionales.

### Datos generados

| Entidad              | Cantidad |
|----------------------|----------|
| Usuarios             | 3        |
| Categorías           | 5        |
| Publicaciones        | 15       |
| Imágenes             | 35       |
| Chats                | 20       |
| Mensajes             | 30       |
| Reportes             | 10       |

### Verificación rápida en SQL
```sql
SELECT COUNT(*) AS UsersCount         FROM AspNetUsers;
SELECT COUNT(*) AS CategoriesCount    FROM Categories;
SELECT COUNT(*) AS ListingsCount      FROM Listings;
SELECT COUNT(*) AS ListingImagesCount FROM ListingImages;
SELECT COUNT(*) AS ChatThreadsCount   FROM ChatThreads;
SELECT COUNT(*) AS ChatMessagesCount  FROM ChatMessages;
SELECT COUNT(*) AS ReportsCount       FROM Reports;
```

---

## Estructura del proyecto
```
PTruequeU/
├── Controllers/        # Endpoints de la API
├── Data/
│   └── ApplicationDbContext.cs   # Contexto y seed
├── Interfaces/         # Contratos de servicios
├── Models/             # Entidades del dominio
├── Services/           # Lógica de negocio
├── appsettings.json    # Configuración
└── Program.cs          # Configuración general
```

---

## Notas

- El seed está diseñado para entorno de desarrollo/pruebas.
- Al usar `HasData`, el seed se aplica una sola vez en la migración inicial.
- La generación de chats respeta combinaciones únicas entre `Listing_Id`, `Buyer_Id` y `Seller_Id`.
- Los endpoints de chats, favoritos y reportes filtran datos por el usuario autenticado.

---

## Autores

- [@FreedPandorad78](https://github.com/FreedPandorad78)
- [@tgarcesm](https://github.com/tgarcesm)
