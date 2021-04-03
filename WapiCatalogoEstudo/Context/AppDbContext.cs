using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WapiCatalogoEstudo.Domains;

namespace WapiCatalogoEstudo.Context
{
    public class AppDbContext : DbContext
    {
        //Definição de Contexto
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        //Mapeamento de entidades
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
    }
}
