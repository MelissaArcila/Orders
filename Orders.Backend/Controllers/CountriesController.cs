using Microsoft.AspNetCore.Mvc;
using Orders.Backend.Data;

namespace Orders.Backend.Controllers
{
    //data notation del controller
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController:ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }
        //--------------------------------------- continuar acá

    }
}
