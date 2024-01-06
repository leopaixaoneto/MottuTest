using MottuKGS.Database;
using MottuKGS.Database.Entities;
using Microsoft.EntityFrameworkCore;
using MottuKGS.Modules.GeneratedKey.Repository.Interfaces;

namespace MottuKGS.Modules.GeneratedKey.Repository.Implementation
{
    public class GeneratedKeyRepository : IGeneratedKeyRepository
    {
        private readonly DatabaseContext _context;

        public GeneratedKeyRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Add(GeneratedKeyEntity generatedKey)
        {
            _context.GeneratedKeys.Add(generatedKey);
            await _context.SaveChangesAsync();
        }

        public async Task<GeneratedKeyEntity?> GetByGeneratedKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            return await _context.GeneratedKeys
               .FirstOrDefaultAsync(t => t.Key == key, cancellationToken);
        }

        public async Task AddBulk(List<GeneratedKeyEntity> generatedKeys)
        {
            _context.GeneratedKeys.AddRange(generatedKeys);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GeneratedKeyEntity>> GetBulk(int number)
        {
            return await _context.GeneratedKeys
                .Where(k => !k.IsUsed)
                .OrderBy(k => k.CreatedAt)
                .Take(number)
                .ToListAsync();
        }

        public async Task UpdateUsedKeys(List<GeneratedKeyEntity> generatedKeys)
        {
            var idList = generatedKeys.Select(k => k.Id).ToList();

            await _context.GeneratedKeys
                .Where(k => idList.Contains(k.Id))
                .ExecuteUpdateAsync(k =>
                    k.SetProperty(p => p.IsUsed, true));
        }


        public async Task<int> CountNotUsedKeys()
        {
            return await _context.GeneratedKeys.CountAsync(k => !k.IsUsed);
        }
    }
}
