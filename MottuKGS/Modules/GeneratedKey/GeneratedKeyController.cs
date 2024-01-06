using Microsoft.AspNetCore.Mvc;
using MottuKGS.Modules.GeneratedKey.Services;
using MottuShared.Logger;

namespace MottuKGS.Modules.GeneratedKey
{
    [ApiController]
    [Route("/")]
    public class GeneratedKeyController : ControllerBase
    {
        private readonly ILogger<GeneratedKeyController> _logger;
        private readonly GenerateBatchOfNewKeysAction _generateBatchOfNewKeysAction;

        public GeneratedKeyController(ILogger<GeneratedKeyController> logger, GenerateBatchOfNewKeysAction generateBatchOfNewKeysAction)
        {
            _logger = logger;
            _generateBatchOfNewKeysAction = generateBatchOfNewKeysAction;
        }


        [HttpGet("generate")]
        public async Task<IResult> Generate()
        {
            try
            {
                _logger.LogSimpleMessage($"Trying to generate new keys");
                await _generateBatchOfNewKeysAction.Execute();

                _logger.LogSimpleMessage($"Keys generated with success");
                return Results.Ok();

            }
            catch (InvalidDataException)
            {
                _logger.LogSimpleErrorMessage($"Trying to generate new keys new keys");
                return Results.NoContent();
            }
        }
    }
}
