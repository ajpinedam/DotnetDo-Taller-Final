using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public interface IWineRepository
    {
        Task Delete(long id);
        Task<IEnumerable<Wine>> GetAll();
        Task<Wine> GetById(long id);
        Task Insert(Wine wine);
        Task Update(long id, Wine wine);
    }

    public class WineRepository : IWineRepository
    {
        private readonly AppDbContext _context;

        public WineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Delete(long id)
        {
            if (_context.Wines == null)
            {
                throw new WineNotFoundException();
            }
            var wine = await _context.Wines.FindAsync(id);

            if (wine == null)
            {
                throw new WineNotFoundException();
            }

            _context.Wines.Remove(wine);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Wine>> GetAll()
        {
            return await _context.Wines.ToListAsync();
        }

        public async Task<Wine> GetById(long id)
        {
            return await _context.Wines.FindAsync(id);
        }

        public async Task Insert(Wine wine)
        {
            if (_context.Wines == null)
            {
                throw new ArgumentNullException(nameof(wine));
            }
            _context.Wines.Add(wine);

            await _context.SaveChangesAsync();
        }


        public async Task Update(long id, Wine wine)
        {
            _context.Entry(wine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WineExists(id))
                {
                    throw new WineNotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool WineExists(long id)
        {
            return (_context.Wines?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }

    public class WineNotFoundException : Exception
    { 

    }
}
