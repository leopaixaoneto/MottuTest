using MottuTest.Database.Entities;

namespace MottuTest.Modules.TinyURL.Repository.Interfaces
{
    public interface ITinyUrlRepository
    {
        public Task Add(TinyUrlEntity tinyUrl);

        public Task<TinyUrlEntity?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default);

        public Task<TinyUrlEntity?> GetById(Guid id, CancellationToken cancellationToken = default);

    }
}
