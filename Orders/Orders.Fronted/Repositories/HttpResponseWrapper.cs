
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Orders.Fronted.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string>? GetErrorMessageAsync()
        {
            if (!Error) {
                return null;
            }

            var statusCode = HttpResponseMessage.StatusCode;

            if(statusCode == System.Net.HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado.";
            }

            if (statusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }

            if (statusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return "Tienes que estar logueado para ejecutar esta operación.";
            }

            return "Tienes un error inesperado.";
        }
    }
}
