# 📋 División de Trabajo - TruequeU

## Equipo de 2 personas

---

## 👤 Persona 1 — Backend Core (Listings, Search, Favorites, Categories)

### Responsabilidades:
1. **Models** (ya creados, revisar y entender):
   - `Listing.cs`, `ListingImage.cs`, `Category.cs`, `Favorite.cs`
   - Enums: `ListingState.cs`, `ListingCondition.cs`

2. **DTOs** (ya creados, revisar):
   - `DTOs/Listings/` → `CreateListingDto`, `UpdateListingDto`, `UpdateListingStateDto`, `ListingResponseDto`, `ListingSearchDto`

3. **Interfaces & Services**:
   - `IListingService` / `ListingService` — CRUD completo de publicaciones
   - `ICategoryService` / `CategoryService` — Listar categorías
   - `IFavoriteService` / `FavoriteService` — Agregar/quitar favoritos

4. **Controllers**:
   - `ListingsController` — GET (buscar), GET/{id}, POST, PUT, PATCH/state, DELETE
   - `CategoriesController` — GET, GET/{id}
   - `FavoritesController` — POST (toggle), GET (mis favoritos), GET/check

5. **Reglas de negocio que debe manejar**:
   - Listing requiere mínimo 3 imágenes
   - Estados: Available → Reserved → Sold
   - Solo el dueño puede cambiar el estado
   - **Sold NO puede regresar a Available**
   - Usuarios suspendidos no pueden crear listings

6. **Seed Data (su parte)**:
   - 15 listings con categorías variadas
   - 35 imágenes (mínimo 3 por listing)
   - 8 categorías

### Archivos clave:
```
Models/Listing.cs
Models/ListingImage.cs
Models/Category.cs
Models/Favorite.cs
Models/Enums/ListingState.cs
Models/Enums/ListingCondition.cs
DTOs/Listings/*
Interfaces/IListingService.cs
Interfaces/ICategoryService.cs
Interfaces/IFavoriteService.cs
Services/ListingService.cs
Services/CategoryService.cs
Services/FavoriteService.cs
Controllers/ListingsController.cs
Controllers/CategoriesController.cs
Controllers/FavoritesController.cs
```

---

## 👤 Persona 2 — Auth, Chat, Reports, Moderation, Profiles

### Responsabilidades:
1. **Models** (ya creados, revisar y entender):
   - `ApplicationUser.cs` (extiende IdentityUser)
   - `ChatRoom.cs`, `ChatMessage.cs`
   - `Report.cs`, `ModerationAction.cs`
   - Enums: `ReportReason.cs`, `ReportTargetType.cs`

2. **DTOs** (ya creados, revisar):
   - `DTOs/Auth/` → `RegisterDto`, `LoginDto`, `AuthResponseDto`
   - `DTOs/Chat/` → `CreateChatMessageDto`, `ChatRoomResponseDto`, `ChatMessageDto`
   - `DTOs/Reports/` → `CreateReportDto`, `ReportResponseDto`
   - `DTOs/Moderation/` → `HideListingDto`, `SuspendUserDto`, `ModerationActionDto`
   - `DTOs/Profile/` → `ProfileResponseDto`, `UpdateProfileDto`

3. **Interfaces & Services**:
   - `IChatService` / `ChatService` — Iniciar chat, enviar mensajes
   - `IReportService` / `ReportService` — Crear/listar/resolver reportes
   - `IModerationService` / `ModerationService` — Ocultar listings, suspender usuarios

4. **Controllers**:
   - `AuthController` — POST/register, POST/login (JWT)
   - `ChatController` — POST/start, POST/messages, GET/messages, GET (mis chats)
   - `ReportsController` — POST, GET (admin), PATCH/resolve (admin)
   - `ModerationController` — POST/hide, POST/unhide, POST/suspend, POST/unsuspend, GET/log
   - `ProfileController` — GET/{userId}, GET/me, PUT/me

5. **Reglas de negocio que debe manejar**:
   - JWT Authentication con Identity
   - No puedes chatear contigo mismo
   - Solo buyer/seller del chat pueden enviar mensajes
   - **Usuarios suspendidos NO pueden crear listings ni enviar mensajes**
   - Acciones de moderación se auditan
   - Solo Admin puede moderar y ver reportes

6. **Seed Data (su parte)**:
   - 6 usuarios (1 admin + 5 usuarios)
   - 20 chat rooms
   - 30 mensajes
   - 10 reportes

### Archivos clave:
```
Models/ApplicationUser.cs
Models/ChatRoom.cs
Models/ChatMessage.cs
Models/Report.cs
Models/ModerationAction.cs
Models/Enums/ReportReason.cs
Models/Enums/ReportTargetType.cs
DTOs/Auth/*
DTOs/Chat/*
DTOs/Reports/*
DTOs/Moderation/*
DTOs/Profile/*
Interfaces/IChatService.cs
Interfaces/IReportService.cs
Interfaces/IModerationService.cs
Services/ChatService.cs
Services/ReportService.cs
Services/ModerationService.cs
Controllers/AuthController.cs
Controllers/ChatController.cs
Controllers/ReportsController.cs
Controllers/ModerationController.cs
Controllers/ProfileController.cs
```

---

## 🤝 Archivos Compartidos (ambos deben entender)

```
Data/ApplicationDbContext.cs    → Contexto de base de datos
Data/DbSeeder.cs               → Datos semilla
Program.cs                     → Configuración de DI, Identity, JWT, middleware
appsettings.json                → Connection string, JWT config
PTruequeU.csproj               → Paquetes NuGet
```

---

## 🔧 Tecnologías Usadas

| Tecnología | Uso |
|---|---|
| ASP.NET Core 10.0 | Framework web |
| Entity Framework Core | ORM (persistencia) |
| SQLite | Base de datos |
| ASP.NET Identity | Autenticación/autorización |
| JWT Bearer | Tokens de autenticación |
| Scalar | Documentación API (como Swagger) |
| Data Annotations | Validación de modelos |
| AddScoped | Inyección de dependencias |

---

## 📌 Cómo correr el proyecto

```bash
cd PTruequeU
dotnet restore
dotnet build
dotnet run
```

- API disponible en: `http://localhost:5088`
- Documentación Scalar: `http://localhost:5088/scalar/v1`
- OpenAPI spec: `http://localhost:5088/openapi/v1.json`

---

## 🔐 Usuarios de prueba (seed)

| Email | Password | Rol |
|---|---|---|
| admin@truequeu.edu | Password123! | Admin |
| carlos.martinez@truequeu.edu | Password123! | User |
| maria.lopez@truequeu.edu | Password123! | User |
| juan.garcia@truequeu.edu | Password123! | User |
| ana.rodriguez@truequeu.edu | Password123! | User |
| pedro.sanchez@truequeu.edu | Password123! | User |

---

## 📊 Verbos HTTP utilizados

| Verbo | Uso |
|---|---|
| **GET** | Obtener datos (listings, categorías, favoritos, chats, perfil, reportes) |
| **POST** | Crear recursos (registro, login, listings, mensajes, reportes, acciones de moderación) |
| **PUT** | Actualizar completo (perfil, listing) |
| **PATCH** | Actualización parcial (cambiar estado del listing, resolver reporte) |
| **DELETE** | Eliminar (listing) |

---

## ✅ Requisitos del proyecto cubiertos

- [x] A) Authentication & Profiles (Identity + JWT)
- [x] B) Listings con imágenes, estados, validaciones
- [x] C) Search & Filters (keyword, category, price, condition, state, date)
- [x] D) Favorites (toggle, listar)
- [x] E) Chat (por listing, persistente)
- [x] F) Reports & Moderation (reportar, dashboard admin, suspender, ocultar)
- [x] Seed: 15 listings, 35 images, 20 chats, 30 messages, 10 reports
