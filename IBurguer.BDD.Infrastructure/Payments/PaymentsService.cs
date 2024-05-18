using IBurguer.BDD.Model.Payments;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace IBurguer.BDD.Infrastructure.Payments
{
    public class PaymentsService : IPaymentsService
    {
        private readonly HttpClient _httpClient;
        private readonly PaymentsServiceConiguration _config;

        private readonly string _basePath = "/iburguer-payments/api/payments";

        public PaymentsService(HttpClient httpClient, IOptions<PaymentsServiceConiguration> options)
        {
            _httpClient = httpClient;
            _config = options.Value;

            _httpClient.BaseAddress = new Uri(_config.BaseUrl);
        }

        public async Task ConfirmPayment(Guid paymentId)
        {
            var response = await _httpClient.PatchAsync($"{_basePath}/{paymentId}/confirmed", null);

            response.EnsureSuccessStatusCode();
        }

        public async Task<GenerateQRCodeResponse> GenerateQRCode(Guid orderId, decimal Amount)
        {
            var content = new StringContent(JsonConvert.SerializeObject(
                new { orderId = orderId, amount = Amount }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_basePath}", content);

            response.EnsureSuccessStatusCode();

            var stringContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GenerateQRCodeResponse>(stringContent);

            return result;
        }
    }
}
