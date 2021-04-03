using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WapiCatalogoEstudo.Domains
{
    [Table("Categorias")]//Não é necessario pois o framework já irá criar com este nome a tabela porem reforça
    public class Categoria
    {
        /*Função de tratamento de relação categoria/produtos*/
        public Categoria()
        {
            Produtos = new Collection<Produtos>();
        }
        [Key]//Não é necessario pois o framework já entende como chava primaria
        public int CategoriaId { get; set; }
        [Required]
        [MaxLength(80)]

        public string Nome { get; set; }
        [Required]
        [MaxLength(200)]

        public string ImagemUrl { get; set; }
       


        //define que uma categoria pode ter varios produtos
        public ICollection<Produtos> Produtos { get; set; }
    }
}
