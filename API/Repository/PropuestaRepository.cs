using API.Data;
using API.Models.Entity;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace API.Repository
{
    public class PropuestaRepository : IPropuestaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string PropuestaCacheKey = "PropuestaCacheKey";
        private readonly int CacheExpirationTime = 3600;
        public PropuestaRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public void ClearCache()
        {
            _cache.Remove(PropuestaCacheKey);
        }

        public async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync() >= 0;
            if (result)
            {
                ClearCache();
            }
            return result;
        }

        public async Task<ICollection<PropuestaEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(PropuestaCacheKey, out ICollection<PropuestaEntity> propuestasCached))
                return propuestasCached;

            var propuestasFromDb = await _context.Propuesta.OrderBy(c => c.Titulo).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(PropuestaCacheKey, propuestasFromDb, cacheEntryOptions);
            return propuestasFromDb;
        }

        public async Task<PropuestaEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(PropuestaCacheKey, out ICollection<PropuestaEntity> propuestasCached))
            {
                var propuesta = propuestasCached.FirstOrDefault(c => c.Id == id);
                if (propuesta != null)
                    return propuesta;
            }

            return await _context.Propuesta.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Propuesta.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(PropuestaEntity propuesta)
        {
            _context.Propuesta.Add(propuesta);
            return await Save();
        }

        public async Task<bool> UpdateAsync(PropuestaEntity propuesta)
        {
            // Validar que el objeto propuesta no sea nulo
            if (propuesta == null)
            {
                throw new ArgumentNullException(nameof(propuesta), "La propuesta no puede ser nula.");
            }

            // Buscar la propuesta existente en la base de datos
            var propuestaExistente = await _context.Propuesta
                .Include(p => p.Users) // Incluir los usuarios para actualizar las relaciones
                .FirstOrDefaultAsync(p => p.Id == propuesta.Id);

            // Si no se encuentra la propuesta, retornar false
            if (propuestaExistente == null)
            {
                return false;
            }

            // Actualizar solo los campos permitidos
            propuestaExistente.Email = propuesta.Email;
            propuestaExistente.Titulo = propuesta.Titulo;
            propuestaExistente.Descripcion = propuesta.Descripcion;
            propuestaExistente.Tipo = propuesta.Tipo;
            propuestaExistente.Estado = propuesta.Estado;

            // Actualizar la relación con los usuarios
            if (propuesta.Users != null)
            {
                // Limpiar la lista actual de usuarios
                propuestaExistente.Users.Clear();

                // Añadir los nuevos usuarios
                foreach (var user in propuesta.Users)
                {
                    propuestaExistente.Users.Add(user);
                }
            }

            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var propuesta = await GetAsync(id);
            if (propuesta == null)
                return false;

            _context.Propuesta.Remove(propuesta);
            return await Save();
        }
    }
}
