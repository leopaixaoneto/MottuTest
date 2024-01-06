using MottuKGS.Database.Entities;
using MottuKGS.Modules.GeneratedKey.Repository.Implementation;

namespace MottuKGS.Modules.GeneratedKey.Services
{
    public class GenerateBatchOfNewKeysAction
    {
        private readonly int _bulkSize = 20;
        private readonly GeneratedKeyRepository _respository;
        private readonly GenerateShortenedCodeAction _shortenedCodeAction;

        public GenerateBatchOfNewKeysAction(GeneratedKeyRepository respository, GenerateShortenedCodeAction shortenedCodeAction)
        {
            _respository = respository;
            _shortenedCodeAction = shortenedCodeAction;
        }

        public async Task Execute()
        {
            //TO-DO Implement an more concise way to generate N new keys as necessary
            //Maybe some multi thread generation
            var toAddKeys = new List<GeneratedKeyEntity>();

            for (int i = 0; i < _bulkSize; i++)
            {
                string generatedKey = await _shortenedCodeAction.Execute();

                toAddKeys.Add(new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    IsUsed = false,
                    Key = generatedKey
                });   
            }

            await _respository.AddBulk(toAddKeys);
        }
    }
}
