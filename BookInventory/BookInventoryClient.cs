using BookInventory.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace BookInventory
{
    internal class BookInventoryClient : IBookInventoryClient
    {
        //todo: move to configuration
        private readonly string _url = "https://localhost/";
        private readonly int _port = 44394;

        private readonly HttpClient _client;
        public BookInventoryClient()
        {
            _client = new HttpClient();
        }

        public async Task<T> Get<T>(string action, Dictionary<string, string> query = null)
            where T : class
        {
            var dictFormUrlEncoded = query != null && query.Any()
                ? new FormUrlEncodedContent(query)
                : null;

            var builder = new UriBuilder(_url + action);
            builder.Port = _port;
            builder.Query = dictFormUrlEncoded == null
                ? string.Empty
                : await dictFormUrlEncoded.ReadAsStringAsync();
            string url = builder.ToString();

            using (HttpResponseMessage response = await _client.GetAsync(new Uri(url)))
            {
                using (HttpContent content = response.Content)
                {
                    if (content == null || !response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Response code: {response.StatusCode} for {response.Headers.Date}");
                    }

                    var json = await content.ReadAsStringAsync();
                    var vm = JsonConvert.DeserializeObject<T>(json);

                    return vm;
                }
            }

        }

        public async Task Post(string action, Dictionary<string, string> request)
        {
            var builder = new UriBuilder(_url + action);
            builder.Port = _port;
            string url = builder.ToString();

            using (HttpResponseMessage response = await _client.PostAsync(new Uri(url), new FormUrlEncodedContent(request)))
            {
                using (HttpContent content = response.Content)
                {
                    if (content == null || !response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Response code: {response.StatusCode} for {response.Headers.Date}");
                    }
                }
            }
        }


        public async Task Put(string action, Dictionary<string, string> request)
        {
            var builder = new UriBuilder(_url + action);
            builder.Port = _port;
            string url = builder.ToString();

            var requestJson = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json");

            using (HttpResponseMessage response = await _client.PutAsync(new Uri(url), new FormUrlEncodedContent(request)))
            {
                using (HttpContent content = response.Content)
                {
                    if (content == null || !response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Response code: {response.StatusCode} for {response.Headers.Date}");
                    }
                }
            }
        }

        public async Task Delete(string action)
        {
            var builder = new UriBuilder(_url + action);
            builder.Port = _port;
            string url = builder.ToString();

            using (HttpResponseMessage response = await _client.DeleteAsync(new Uri(url)))
            {
                using (HttpContent content = response.Content)
                {
                    if (content == null || !response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Response code: {response.StatusCode} for {response.Headers.Date}");
                    }

                    var json = await content.ReadAsStringAsync();
                }
            }
        }
    }
}
