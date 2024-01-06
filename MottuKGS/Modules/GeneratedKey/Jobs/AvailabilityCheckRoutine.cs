using Quartz;
using MottuShared.Logger;
using MottuKGS.Modules.GeneratedKey.Services;
using MottuKGS.Modules.GeneratedKey.Repository.Implementation;

namespace MottuKGS.Modules.GeneratedKey.Jobs
{
    [DisallowConcurrentExecution]
    public class AvailabilityCheckRoutine: IJob
    {
        private readonly ILogger<AvailabilityCheckRoutine> _logger;
        private readonly GenerateBatchOfNewKeysAction _generateBatchOfNewKeysAction;
        private readonly GeneratedKeyRepository _respository;

        public AvailabilityCheckRoutine(ILogger<AvailabilityCheckRoutine> logger, GenerateBatchOfNewKeysAction generateBatchOfNewKeysAction, GeneratedKeyRepository respository)
        {
            _logger = logger;
            _generateBatchOfNewKeysAction = generateBatchOfNewKeysAction;
            _respository = respository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogSimpleMessage($"{DateTime.Now} - Checking for database availability of generated keys");

            if(await _respository.CountNotUsedKeys() < 20)
            {
                await _generateBatchOfNewKeysAction.Execute();
            }
        }
    }
}
