using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Application.Interfaces
{
    public class RequestService : IRequestService
    {
        private readonly IHttpClientFactory clientFactory;

        public RequestService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task Post<T>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize<T>(data);

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new StringContent(dataJson, Encoding.UTF8, "application/json");

            var httpClient = clientFactory.CreateClient();

            var response = await httpClient.SendAsync(request);
        }
    }
}