
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;

namespace Orders.Backend
{
    public class Program  //este es el main, por aca empieza a correr el programa
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
              
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Inyectar la conexion de la base de datos 
            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));//el name debe ser el mismo que se indique en el string connection

            var app = builder.Build();
            app.UseCors(x => x.AllowAnyMethod()
                  .AllowAnyMethod()
                  .SetIsOriginAllowed(origin => true)
                  .AllowCredentials());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())//en esta linea se controla que cuando estemos en pruebas nos abra swagger
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}