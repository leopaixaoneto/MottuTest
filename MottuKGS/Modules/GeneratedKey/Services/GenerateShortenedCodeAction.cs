using MottuKGS.Database;
using Microsoft.EntityFrameworkCore;

namespace MottuKGS.Modules.GeneratedKey.Services
{
    /*
        Due to the simplicity of the project, I chose to maintain a very simplistic and optimistic code generation.

        I know that this code has problems regarding longevity and code generation performance.

        The correct way would be to generate codes by ranges of Id's with the division of a table of used and unused codes.
     */
    public class GenerateShortenedCodeAction
    {
        public const int ShortenedLinkCharsCount = 7;
        private const string CharDictionary = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly DatabaseContext _context;

        public GenerateShortenedCodeAction(DatabaseContext context)
        {
            _context = context;
        }

        private readonly Random _random = new();

        public async Task<string> Execute()
        {
            var codeChars = new char[ShortenedLinkCharsCount];

            while (true)
            {
                for (int i = 0; i < ShortenedLinkCharsCount; i++)
                {
                    int randomIndex = _random.Next(CharDictionary.Length - 1);

                    codeChars[i] = CharDictionary[randomIndex];
                }

                var shortenedCode = new string(codeChars);

                if (!await _context.GeneratedKeys.AnyAsync(t => t.Key == shortenedCode).ConfigureAwait(false))
                {
                    return shortenedCode;
                }
            }
        }
    }
}
