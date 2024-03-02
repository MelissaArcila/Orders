using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Orders.FrontEnd;
using Orders.FrontEnd.Repositories;

namespace Orders.FrontEnd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //en C# hay 3 formas de inyectar, scope es una de ellas 
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7265//") });//la url es la que provee los serviicos de backend al frontend
            builder.Services.AddScoped<IRepository, Repository>(); //aca inyectamos la implementacion

            await builder.Build().RunAsync();
        }
    }
}