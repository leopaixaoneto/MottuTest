using MassTransit;
using MottuShared.Contracts.Requests;
using MottuShared.Contracts.Responses;

namespace MottuApi.Modules.TinyURL.Services
{
    public class GetGeneratedKeyAction
    {
        private readonly IRequestClient<GetGeneratedKeyRequest> _client;

        public GetGeneratedKeyAction(IRequestClient<GetGeneratedKeyRequest> client)
        {
            _client = client;
        }

        public async Task<string> Execute()
        {
            var response = await _client
                .GetResponse<GetGeneratedKeyResponse>(
                    new GetGeneratedKeyRequest
                    {
                       NumberOfKeys = 1,
                    }
                );

            return response.Message.GeneratedKeys.FirstOrDefault() ?? "";
        }
    }
}
