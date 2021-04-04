using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WapiCatalogoEstudo.Context;
using WapiCatalogoEstudo.Domains;

namespace WapiCatalogoEstudo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext contexto)
        {
            _context = contexto;
        }

        //METODO DE LISTAGEM
        [HttpGet]
        public ActionResult<IEnumerable<Produtos>> Get()
        {
            return _context.Produtos.AsNoTracking().ToList();
        }


        //METODO DE LISTAGEM EM DETALHE
        [HttpGet("{id}",Name ="ObterProduto")]
        public ActionResult<Produtos> Get(int id)
        {
            var produto = _context.Produtos.AsNoTracking()//.AsNoTracking só é chamando se o comando for somente parar listar
                .FirstOrDefault(p => p.ProdutoId == id);
            if(produto == null)
            {
                return NotFound();

            }
            return produto;
        }


        //METODO DE INSERÇÃO
        [HttpPost]
        public ActionResult Post([FromBody]Produtos produtos)
        {
            /* if(IModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }*/

            _context.Produtos.Add(produtos);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto",
                new { id = produtos.ProdutoId }, produtos);
        }

        //METODO DE EDIÇÃO
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] Produtos produtos)
        {
            if(id != produtos.ProdutoId)
            { 
                return BadRequest(); 
            }
            _context.Entry(produtos).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }


        //METODO DE EXCLUSÃO
        [HttpDelete("{id}")]
        public ActionResult<Produtos> Delete(int id)
        {
            var produtos = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            //var produtos = _context.Produtos.Find(id);
            if(produtos == null)
            {
                return BadRequest();
            }
            _context.Produtos.Remove(produtos);
            _context.SaveChanges();
            return Ok();
        }

    }
}
