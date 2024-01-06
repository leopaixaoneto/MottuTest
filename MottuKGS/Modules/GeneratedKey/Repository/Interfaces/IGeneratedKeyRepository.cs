using MottuKGS.Database.Entities;

namespace MottuKGS.Modules.GeneratedKey.Repository.Interfaces
{
    public interface IGeneratedKeyRepository
    {
        public Task<GeneratedKeyEntity?> GetByGeneratedKeyAsync(string key, CancellationToken cancellationToken = default);
    
        public Task Add(GeneratedKeyEntity generatedKey);

        public Task AddBulk(List<GeneratedKeyEntity> generatedKeys);

        public Task<List<GeneratedKeyEntity>> GetBulk(int number);

        public  Task UpdateUsedKeys(List<GeneratedKeyEntity> generatedKeys);
        public Task<int> CountNotUsedKeys();
    }
}
