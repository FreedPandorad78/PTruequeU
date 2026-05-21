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
- Dashboard de administración (listar, crear y eliminar usuarios; suspender/reactivar cuentas).
- Endpoint de perfil público `GET /users/{id}`.
- Reactivar publicaciones ocultas desde el panel de admin.
- Transición de estado `Reserved → Available` (el dueño puede desmarcar una reserva).
- Seed de datos de prueba mediante `HasData` en `OnModelCreating`.
- Persistencia con Entity Framework Core y SQL Server.

---

## Seguridad

### Implementada actualmente
- Autenticación con JWT (JSON Web Tokens).
- Control de acceso a endpoints autenticados.
- Autorización por roles (usuario/administrador).
- Validación de ownership (solo el dueño modifica su recurso).
- Asociación de datos por usuario (propietario de publicación, emisor de mensaje, reportante).
- Restricciones de integridad para evitar combinaciones duplicadas en chats.
- Suspensión de usuarios (usuarios suspendidos no pueden publicar).

### Planificada (siguiente fase)
- Endurecimiento de autenticación (refresh tokens).
- Políticas de autorización más granulares por endpoint.

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
    "Audience": "PTruequeUUsers"
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

## Endpoints principales

### Autenticación
| Método | Ruta              | Descripción                        |
|--------|-------------------|------------------------------------|
| POST   | /auth/register    | Registrar nuevo usuario            |
| POST   | /auth/login       | Iniciar sesión y obtener JWT       |

### Usuarios
| Método | Ruta              | Descripción                        |
|--------|-------------------|------------------------------------|
| GET    | /users/{id}       | Perfil público de un usuario       |

### Publicaciones
| Método | Ruta                          | Descripción                          |
|--------|-------------------------------|--------------------------------------|
| GET    | /api/Listings                 | Listar/buscar publicaciones          |
| GET    | /api/Listings/{id}            | Detalle de una publicación           |
| POST   | /api/Listings                 | Crear publicación (auth)             |
| PUT    | /api/Listings/{id}            | Editar publicación (owner)           |
| PATCH  | /api/Listings/{id}/state      | Cambiar estado (owner)               |
| DELETE | /api/Listings/{id}            | Eliminar publicación (owner)         |

### Administración (requiere rol Admin)
| Método | Ruta                            | Descripción                          |
|--------|---------------------------------|--------------------------------------|
| GET    | /admin/users                    | Listar todos los usuarios            |
| POST   | /admin/users                    | Crear usuario con rol asignado       |
| DELETE | /admin/users/{id}               | Eliminar usuario                     |
| PATCH  | /admin/users/{id}/suspend       | Suspender usuario                    |
| PATCH  | /admin/users/{id}/unsuspend     | Reactivar usuario suspendido         |
| PATCH  | /admin/listings/{id}/hide       | Ocultar publicación                  |
| PATCH  | /admin/listings/{id}/show       | Reactivar publicación oculta         |
| GET    | /admin/reports                  | Listar reportes                      |
| GET    | /admin/audit                    | Registro de acciones de moderación   |

---

## Documentación interactiva (Scalar)

Una vez ejecutada la API, acceder a:

http://localhost:5088/scalar/v1

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

POST http://localhost:5088/auth/login

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

GET http://localhost:5088/chats
GET http://localhost:5088/favorites
GET http://localhost:5088/api/Listings

---

## Credenciales de prueba (seed)

Los siguientes usuarios se crean automáticamente con el seed:

| Usuario | Email            | Contraseña | Rol   |
|---------|------------------|------------|-------|
| admin   | admin@email.com  | Test1234!  | Admin |
| andrea  | andrea@email.com | Test1234!  | User  |
| bruno   | bruno@email.com  | Test1234!  | User  |
| carla   | carla@email.com  | Test1234!  | User  |

> Para probar el chat, iniciar sesión como **bruno** o **carla** e iniciar conversación en una publicación de **andrea**.

---

## Seed de datos

El proyecto incluye seed automático mediante `HasData` en `OnModelCreating`. No requiere pasos adicionales.

### Datos generados

| Entidad              | Cantidad |
|----------------------|----------|
| Usuarios             | 4        |
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
├── DTOs/               # Objetos de transferencia de datos
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
- El backend corre en HTTP en `http://localhost:5088` para compatibilidad con el frontend en desarrollo.

---

## Autores

- [@FreedPandorad78](https://github.com/FreedPandorad78) — David Orozco
- [@tgarcesm](https://github.com/tgarcesm) — Tomás Garcés
