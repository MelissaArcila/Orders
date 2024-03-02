using System.Net;

namespace Orders.FrontEnd.Repositories
{
    public class HttpResponseWrapper<T> //clase generica, es una clase de envoltorios de respuestas, se va a usar para todas las peticiones,
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; }// el ? indica que puede ser null
        public bool Error { get; }// que sea solo get indica que es solo lectura
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string?> GetErrorMessageasync()
        {
            if (!Error)
            {
                return null;
            }

            var statusCode = HttpResponseMessage.StatusCode;//var significa que no estamos tipando, se tipa con la asignacion
            if (statusCode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }

            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Tienes que estar loggeado para esta operacion";
            }

            if (statusCode == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tienes permisos para ejecutar esta operación";
            }

            return "Error Inesperado";
        }
    }
}