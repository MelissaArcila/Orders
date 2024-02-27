using Microsoft.EntityFrameworkCore;
using Orders.Shared.Entities;

namespace Orders.Backend.Data
{
    public class DataContext: Microsoft.EntityFrameworkCore.DbContext
    {
        //constructor
        //hay que pasarle un BDContextoption que es una clase generica Generics, esto hace que nos conectemos a la DB
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        
        //Propiedad Countries tipo DBset tambien es eun generics, countries va a ser la collection/tabla
        //Country esta en otro proyecto, entonces se debe adicionar la referencia a orders.Shared
        public DbSet<Country> Countries { get; set; }


        //paragarantizar que los registros sean unicos se deben crear indices
        //pra esto se hace un override al OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //creamos el indice con una expresion lambda para modificar la entidad Country, se usa una expresion lambda c => c.Name
            //con el isunique controlamos que ese campo sea unico
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
        }


    }
}
