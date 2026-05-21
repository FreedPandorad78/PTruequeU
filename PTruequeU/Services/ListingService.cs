using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Listings;
using PTruequeU.Interfaces;
using PTruequeU.Models;
using PTruequeU.Models.Enums;

namespace PTruequeU.Services
{
    // Este servicio concentra toda la lógica de negocio de publicaciones (listings):
    // Crear, consultar, buscar, actualizar, cambiar estado y eliminar.
    public class ListingService : IListingService
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos el DbContext para interactuar con la base de datos.
        public ListingService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Crea una nueva publicación para el usuario autenticado.
        // También guarda las imágenes asociadas y devuelve el listing completo.
        public async Task<ListingResponseDto> Create(string userId, CreateListingDto dto)
        {
            var listing = new Listing
            {
                Listing_id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Condition = dto.Condition,
                Price = (decimal)dto.Price, //No dejaba con double, entonces puse un cast decimal para arreglar el error.
                Location = dto.Location,
                Category_Id = dto.CategoryId,
                User_Id = userId,
                State = ListingState.Available,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            // Guardamos las imágenes en el orden en que llegan.
            for (int i = 0; i < dto.ImageUrls.Count; i++)
            {
                _context.ListingImages.Add(new ListingImage
                {
                    ListingImage_Id = Guid.NewGuid(),
                    Listing_Id = listing.Listing_id,
                    ImageUrl = dto.ImageUrls[i],
                    DisplayOrder = i
                });
            }

            await _context.SaveChangesAsync();

            // Reconsultamos para devolver DTO completo (con relaciones incluidas).
            return (await GetById(listing.Listing_id))!;
        }

        // Obtiene un listing por ID incluyendo usuario, categoría e imágenes.
        // Si está oculto (IsHidden), no se devuelve.
        public async Task<ListingResponseDto?> GetById(Guid id, bool isAdmin = false)
        {
            var query = _context.Listings
                .Include(l => l.User)
                .Include(l => l.Category)
                .Include(l => l.Images.OrderBy(i => i.DisplayOrder))
                .AsQueryable();

            if (!isAdmin)
                query = query.Where(l => !l.IsHidden);

            var listing = await query.FirstOrDefaultAsync(l => l.Listing_id == id);

            if (listing == null) return null;

            return MapToDto(listing);
        }

        // Búsqueda de publicaciones con filtros dinámicos + paginación.
        // Solo retorna publicaciones visibles (no hidden).
        public async Task<List<ListingResponseDto>> Search(ListingSearchDto search, string userId, bool isAdmin)
        {
            var query = _context.Listings
                .Include(l => l.User)
                .Include(l => l.Category)
                .Include(l => l.Images.OrderBy(i => i.DisplayOrder))
                .AsQueryable();

            // hidden solo para no-admin
            if (!isAdmin)
                query = query.Where(l => !l.IsHidden);

            // Filtro por palabra clave en título o descripción.
            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                var keyword = search.Keyword.ToLower();
                query = query.Where(l =>
                    l.Title.ToLower().Contains(keyword) ||
                    l.Description.ToLower().Contains(keyword));
            }

            if (search.CategoryId.HasValue)
                query = query.Where(l => l.Category_Id == search.CategoryId.Value);

            if (search.MinPrice.HasValue)
                query = query.Where(l => l.Price >= search.MinPrice.Value);

            if (search.MaxPrice.HasValue)
                query = query.Where(l => l.Price <= search.MaxPrice.Value);

            if (search.Condition.HasValue)
                query = query.Where(l => l.Condition == search.Condition.Value);

            if (search.State.HasValue)
                query = query.Where(l => l.State == search.State.Value);

            if (search.PostedAfter.HasValue)
                query = query.Where(l => l.CreatedAt >= search.PostedAfter.Value);

            if (search.PostedBefore.HasValue)
                query = query.Where(l => l.CreatedAt <= search.PostedBefore.Value);

            // Ordenamos por más recientes y aplicamos paginación.
            var listings = await query
                .OrderByDescending(l => l.CreatedAt)
                .Skip((search.Page - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();

            return listings.Select(MapToDto).ToList();
        }

        // Actualiza datos editables del listing.
        // Regla: solo el dueño del listing puede editar.
        public async Task<ListingResponseDto?> Update(Guid id, string userId, UpdateListingDto dto)
        {
            var listing = await _context.Listings.FirstOrDefaultAsync(l => l.Listing_id == id);
            if (listing == null || listing.User_Id != userId) return null;

            if (dto.Title != null) listing.Title = dto.Title;
            if (dto.Description != null) listing.Description = dto.Description;
            if (dto.Condition.HasValue) listing.Condition = dto.Condition.Value;
            if (dto.Price.HasValue) listing.Price = (decimal)dto.Price.Value;
            if (dto.Location != null) listing.Location = dto.Location;
            if (dto.CategoryId.HasValue) listing.Category_Id = dto.CategoryId.Value;

            listing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetById(id);
        }

        // Cambia el estado del listing.
        // Regla actual: si ya está vendido (Sold), no puede volver a Available.
        // También solo el owner puede cambiar el estado.
        public async Task<ListingResponseDto?> UpdateState(Guid id, string userId, UpdateListingStateDto dto)
        {
            var listing = await _context.Listings.FirstOrDefaultAsync(l => l.Listing_id == id);
            if (listing == null || listing.User_Id != userId) return null;

            // Transiciones válidas del flujo:
            // Available -> Reserved
            // Reserved  -> Available (el dueño desmarca la reserva)
            // Reserved  -> Sold
            bool validTransition =
                (listing.State == ListingState.Available && dto.State == ListingState.Reserved) ||
                (listing.State == ListingState.Reserved && dto.State == ListingState.Available) ||
                (listing.State == ListingState.Reserved && dto.State == ListingState.Sold);

            if (!validTransition)
                return null;

            listing.State = dto.State;
            listing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetById(id);
        }

        // Elimina un listing.
        // Regla: solo el dueño puede eliminarlo.
        public async Task<bool> Delete(Guid id, string userId)
        {
            var listing = await _context.Listings.FirstOrDefaultAsync(l => l.Listing_id == id);
            if (listing == null || listing.User_Id != userId) return false;

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();
            return true;
        }

        // Convierte entidad Listing -> DTO de respuesta.
        // Esto evita exponer directamente el modelo de base de datos.
        private static ListingResponseDto MapToDto(Listing listing)
        {
            return new ListingResponseDto
            {
                ListingId = listing.Listing_id,
                Title = listing.Title,
                Description = listing.Description,
                Condition = listing.Condition,
                Price = listing.Price,
                Location = listing.Location,
                State = listing.State,
                CreatedAt = listing.CreatedAt,
                UpdatedAt = listing.UpdatedAt,
                UserId = listing.User_Id,
                CategoryId = listing.Category_Id,
                Images = listing.Images.Select(i => new ListingImageDto
                {
                    ListingImageId = i.ListingImage_Id,
                    ImageUrl = i.ImageUrl,
                    DisplayOrder = i.DisplayOrder
                }).ToList()
            };
        }
    }
}