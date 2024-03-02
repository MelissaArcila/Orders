
using System.Text.Json;
using System.Text;


namespace Orders.FrontEnd.Repositories
{
    public class Repository : IRepository// esta clase no se nombra con la I porque es la que implementa
    {
        //Atributos -> siempre son privados y son diferentes a las propiedades
        private readonly HttpClient _httpClient; //es readonly pq son propiedades que solo se peuden asignar en el constructor
        private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions()//sintaxis de flecha para hacer propiedades de lectura
        {
            PropertyNameCaseInsensitive = true //para que puieda mapear el json sin importar si tiene mayusculas o minusculas
        };
            

        //constructor
        public Repository(HttpClient httpClient)//inyectamos en httpClient que esta conectado con el program.cs del front
        {
            _httpClient = httpClient;// es un campo no una propiedad
        }


        //implementamos los metodos de la interfaz
        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)//metodo para listar
        {
            //throw new NotImplementedException();
            var responseHttp = await _httpClient.GetAsync(url);
            if (responseHttp.IsSuccessStatusCode) //esta respuesta llega en json, por eso hay que deserializar para crearlo en un objeto
            {
                var response = await UnserializeAnswer<T>(responseHttp);
                return new HttpResponseWrapper<T>(response, false, responseHttp);//el false es para indicarle que no hubo error 
            }
            return new HttpResponseWrapper<T>(default, true, responseHttp);//esta es la respuesta que envia cuando hya error
        }


        

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model)// este es post que no devuelve respuesta
        {
            //throw new NotImplementedException();
            var messageJson = JsonSerializer.Serialize(model);
            //codificamos el json en utf-8 (español)
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);//esta es la respuesta que envia cuando hya error

        }

        public async  Task<HttpResponseWrapper<Actionresponse>> postAsync<T, Actionresponse>(string url, T model)//este es el post que devuelve respuesta, el model es lo que llega por el body, que viene en json y hay que serializarlo para pasarlo a string
        {
            //throw new NotImplementedException();
            //serializacion
            var messageJson = JsonSerializer.Serialize(model);
            //codificamos el json en utf-8 (español)
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");
            
            var responseHttp = await _httpClient.PostAsync(url, messageContent);
            if (responseHttp.IsSuccessStatusCode) //esta respuesta llega en json, por eso hay que deserializar para crearlo en un objeto
            {
                var response = await UnserializeAnswer<Actionresponse>(responseHttp);
                return new HttpResponseWrapper<Actionresponse>(response, false, responseHttp);//el false es para indicarle que no hubo error 
            }
            return new HttpResponseWrapper<Actionresponse>(default, true, responseHttp);//esta es la respuesta que envia cuando hya error
        }
        
        
        private async Task<T?> UnserializeAnswer<T>(HttpResponseMessage responseHttp)
        {
            var response = await responseHttp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions);
        }
    }
}
