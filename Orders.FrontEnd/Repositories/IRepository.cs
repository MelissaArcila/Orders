namespace Orders.FrontEnd.Repositories
{
    public interface IRepository// se usa una interfaz para poder mockiar 
    {
        //Metodos iniciales
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);

        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model);

        Task<HttpResponseWrapper<Actionresponse>> postAsync<T, Actionresponse>(string url, T model);// hay dos post porque hay unos que devuelven NoContent
    }
}
