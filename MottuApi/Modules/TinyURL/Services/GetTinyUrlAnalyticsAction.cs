using MassTransit;
using MottuShared.Contracts.Requests;

namespace MottuApi.Modules.TinyURL.Services
{
    public class GetTinyUrlAnalyticsAction
    {
        private readonly IRequestClient<GetAnalyticsRequest> _client;

        public GetTinyUrlAnalyticsAction(IRequestClient<GetAnalyticsRequest> client)
        {
            _client = client;
        }

        public async Task<GetAnalyticsResponse> Execute(Guid id)
        {
            var response = await _client
                .GetResponse<GetAnalyticsResponse>(
                    new GetAnalyticsRequest
                    {
                        Id = id
                    }
                );

            return response.Message;
        }
    }
}
