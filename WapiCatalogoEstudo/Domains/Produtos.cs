﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WapiCatalogoEstudo.Domains
{
    [Table("Produtos")]
    public class Produtos
    {
        [Key]
        public int ProdutoId { get; set; }
        
        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Descrição { get; set; }
        
        [Required]
        public decimal Preco { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string ImagemUrl { get; set; }
        
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        
        //Define a chave estrangeira na tabela produtos
        public Categoria Categoria { get; set; }
        public int CategoriaID { get; set; }

    }
}
