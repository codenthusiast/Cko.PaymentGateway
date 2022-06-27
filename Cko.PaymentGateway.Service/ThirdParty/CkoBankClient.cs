using Cko.PaymentGateway.Core.Models;
using Cko.PaymentGateway.Service.DTOs;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cko.PaymentGateway.Service.ThirdParty
{
    public class CkoBankClient : IAcquiringBankClient
    {
        private readonly HttpClient _httpClient;

        public CkoBankClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CardPaymentReponseDTO> ProcessCardTransaction(CardPaymenRequestDTO request)
        {
            var json = JsonSerializer.Serialize(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/payment/process", stringContent);
            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CardPaymentReponseDTO>(responseString);
            }
            throw new HttpRequestException(response.ReasonPhrase);
        }
    }
}