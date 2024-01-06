using Microsoft.AspNetCore.Mvc;
using MottuTest.Modules.TinyURL.DTO;
using MottuTest.Modules.TinyURL.Services;

using MottuShared.Logger;
using MottuApi.Modules.TinyURL.Services;

namespace MottuTest.Modules.TinyURL
{
    [ApiController]
    [Route("/")]
    public class TinyUrlController : ControllerBase
    {
        private readonly GetTinyUrlAction _getTinyUrlUseCase;
        private readonly CreateTinyUrlAction _createShortenedUrlUseCase;
        private readonly GetTinyUrlInfoAction _getTinyUrlInfoUseCase;

        private readonly ILogger<TinyUrlController> _logger;

        public TinyUrlController(
            ILogger<TinyUrlController> logger,
            GetTinyUrlAction getTinyUrlUseCase,
            CreateTinyUrlAction createShortenedUrlUseCase,
            GetTinyUrlInfoAction getTinyUrlInfoUseCase)
        {
            _logger = logger;
            _createShortenedUrlUseCase = createShortenedUrlUseCase;
            _getTinyUrlUseCase = getTinyUrlUseCase;
            _getTinyUrlInfoUseCase = getTinyUrlInfoUseCase;
        }

        [HttpPost("shorten")]
        public async Task<IResult> Post(
            [FromBody] CreateTinyUrlRequest request)
        {
            _logger.LogSimpleMessage("Creating shortened Url - Start");

            try
            {
                var response = await _createShortenedUrlUseCase.Execute(request, HttpContext);
                
                return Results.Ok(response);
            }
            catch(InvalidCastException)
            {
                return Results.BadRequest("Invalid URL format");
            }
        }

        [HttpGet("{code}")]
        public async Task<IResult> RedirectToTinyUrl(string code)
        {
            try
            {
                _logger.LogSimpleMessage($"Trying to redirect user to: {code}");
                var tinyUrl = await _getTinyUrlUseCase.Execute(new() { ShortenedCode = code });

                _logger.LogSimpleMessage($"Redirecting user to: {tinyUrl.Original}");
                return Results.Redirect(tinyUrl.Original);

            }catch(InvalidDataException)
            {
                _logger.LogSimpleErrorMessage($"Error occours trying to find TinyUrl code: {code}");
                return Results.NoContent();
            }
        }

        [HttpGet("/info/{id}")]
        public async Task<IResult> GetTinyUrlInfo(string id)
        {
            try
            {
                _logger.LogSimpleMessage($"Trying to get data of tinyUrl with ID={id}");
                var tinyUrl = await _getTinyUrlInfoUseCase.Execute(new() { Id = Guid.Parse(id) });

                return Results.Ok(tinyUrl);
            }
            catch (InvalidDataException)
            {
                _logger.LogSimpleErrorMessage($"Error occours trying to find TinyUrl code: {id}");
                return Results.NoContent();
            }
        }
    }
}
