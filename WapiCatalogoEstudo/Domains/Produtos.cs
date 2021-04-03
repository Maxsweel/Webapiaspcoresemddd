using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WapiCatalogoEstudo.Domains
{
    public class Produtos
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public decimal Preco { get; set; }
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        
        //Define a chave estrangeira na tabela produtos
        public Categoria Categoria { get; set; }
        public int CategoriaID { get; set; }

    }
}
