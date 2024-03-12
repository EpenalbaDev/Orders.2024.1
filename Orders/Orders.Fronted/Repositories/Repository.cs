
using System.Text;
using System.Text.Json;

namespace Orders.Fronted.Repositories
{
    public class Repository : IRepository
    {
        //atributo
        //constructor
        //metodos publicos
        //metodos privados

        //readonly son propiedades que se asignan en el constructor
        //Constante se define constante y no cambia

        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)
        {
          var responseHttp = await _httpClient.GetAsync(url);

            if(responseHttp.IsSuccessStatusCode)
            {
                var response = await unserializeAnswer<T>(responseHttp);
                return new HttpResponseWrapper<T>(response,false,responseHttp);
            }

            return new HttpResponseWrapper<T>(default,true,responseHttp);
        }



        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model);
            var messageContext = new StringContent(messageJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContext);
            return new HttpResponseWrapper<object>(null,!responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model);
            var messageContext = new StringContent(messageJson, Encoding.UTF8,"application/json");
            var responseHttp = await _httpClient.PostAsync(url,messageContext);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await unserializeAnswer<TActionResponse>(responseHttp);
                return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp);
        }

        private async Task<T> unserializeAnswer<T>(HttpResponseMessage responseHttp)
        {
            var response = await responseHttp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions)!;
        }
    }
}
