using Microsoft.AspNetCore.Mvc;//
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    //data notation del controller
    [ApiController]
    [Route("api/[controller]")]//para rutear el controlador, es decir como se va a ver en el swagger, se usa [controller] por si se cambia el nombre del controlador no toca refactorizar todo el codigo
    public class CountriesController : ControllerBase
    {
        //para que el datacontext exista en toda la vida util del controlador se debe crear un campo que va a ser una propiedad de solo lectura
        // del tipo DataContext
        private readonly DataContext _context;

        //inyectar la BD por constructor, es un principio solid,
        public CountriesController(DataContext context)
        {
            _context = context;
        }

        //metodo para consultar t paises
        [HttpGet]
        public async Task<IActionResult> GetAsync()//sin parametros para que traiga todo
        {
            return Ok(await _context.Countries.ToListAsync());
        }

        //metodo pra consultar los paises por id
        [HttpGet("{id}")] // pasamos el parametro por ruta, hay otras 3 formas de pasar el aprametro
        public async Task<IActionResult> GetAsync(int id)//sin parametros para que traiga todo
        {
            var country = await _context.Countries.FindAsync(id);//buscar el pais
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        //metodo para crear paises
        [HttpPost]
        public async Task<IActionResult> PostAsync(Country country)
        {
            _context.Add(country);
            await _context.SaveChangesAsync();//graba los cambios
            return Ok(country);//es una respuesta 200
        }

        //metodo para actualizar paises
        [HttpPut]
        public async Task<IActionResult> PutAsync(Country country)
        {
            _context.Update(country);
            await _context.SaveChangesAsync();//graba los cambios
            return NoContent();// es un ok sin contenido
        }

        //metodo pra eliminar los paises por id
        [HttpDelete("{id}")] // pasamos el parametro por ruta, hay otras 3 formas de pasar el aprametro
        public async Task<IActionResult> DeleteAsync(int id)//sin parametros para que traiga todo
        {
            var country = await _context.Countries.FindAsync(id);//buscar el pais
            if (country == null)
            {
                return NotFound();
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}