using Microsoft.AspNetCore.Http;
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
            try
            {
                return _context.Produtos.AsNoTracking().ToList();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter os produtos no banco de dados");
            }
        }


        //METODO DE LISTAGEM EM DETALHE
        [HttpGet("{id}",Name ="ObterProduto")]
        public ActionResult<Produtos> Get(int id)
        {
            try
            {
                var produto = _context.Produtos.AsNoTracking()//.AsNoTracking só é chamando se o comando for somente parar listar
                    .FirstOrDefault(p => p.ProdutoId == id);
                if (produto == null)
                {
                    return NotFound();

                }
                return produto;
            }catch(Exception)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter o produto no banco de dados"); 
            }
        }


        //METODO DE INSERÇÃO
        [HttpPost]
        public ActionResult Post([FromBody]Produtos produtos)
        {
            try
            {
                /* if(IModelState.IsValid)
                 {
                     return BadRequest(ModelState);
                 }*/

                _context.Produtos.Add(produtos);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterProduto",
                    new { id = produtos.ProdutoId }, produtos);
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao inserir o produto no banco de dados");
            }
        }

        //METODO DE EDIÇÃO
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] Produtos produtos)
        {
            try
            {
                if (id != produtos.ProdutoId)
                {
                    return BadRequest();
                }
                _context.Entry(produtos).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao alterar o produto no banco de dados");
            }
        }


        //METODO DE EXCLUSÃO
        [HttpDelete("{id}")]
        public ActionResult<Produtos> Delete(int id)
        {
            try
            {
                var produtos = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                //var produtos = _context.Produtos.Find(id);
                if (produtos == null)
                {
                    return BadRequest();
                }
                _context.Produtos.Remove(produtos);
                _context.SaveChanges();
                return Ok();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir o produto no banco de dados");
            }
        }

    }
}
