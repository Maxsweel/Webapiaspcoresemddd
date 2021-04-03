using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WapiCatalogoEstudo.Domains
{
    public class Categoria
    {
        /*Função de tratamento de relação categoria/produtos*/
        public Categoria()
        {
            Produtos = new Collection<Produtos>();
        }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
       
        //define que uma categoria pode ter varios produtos
        public ICollection<Produtos> Produtos { get; set; }
    }
}
