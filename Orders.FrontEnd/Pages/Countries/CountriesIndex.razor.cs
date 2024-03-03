using Microsoft.AspNetCore.Components;
using Orders.FrontEnd.Repositories;
using Orders.Shared.Entities;

namespace Orders.FrontEnd.Pages.Countries
{
    public partial class CountriesIndex
    {
        [Inject] IRepository Repository { get; set; } = null!;
    public List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()//es el metodo que corre cuando la pagina carga, aca se pone lo que queremos que haga cuando se inicia la pagina
        {
            await base.OnInitializedAsync();
            var responseHttp = await Repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHttp.Response;
        }
    }
}
