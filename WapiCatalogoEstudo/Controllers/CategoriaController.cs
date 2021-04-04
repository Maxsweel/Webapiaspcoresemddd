using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WapiCatalogoEstudo.Context;
using WapiCatalogoEstudo.Domains;

namespace WapiCatalogoEstudo.Controller
{
    
    [Route("Api/[Controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext contexto)
        {
            _context = contexto;
        }

        //LISTAGEM
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Erro ao obter as categorias no banco de dados");
            }
        }


        //PRODUTOS RELACIONADOS
        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                return _context.Categorias.Include(x => x.Produtos).ToList();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter o produto da categoria no banco de dados");
            }
        }


        //LISTAGEM UNICA
        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking()//.AsNoTracking só é chamando se o comando for somente parar listar
                    .FirstOrDefault(c => c.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound($"A categoria com id= {id} não foi encontrada");

                }
                return categoria;
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter a categoria no banco de dados");
            }
        }

        //metodo de adição
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            try
            {
                /* if(IModelState.IsValid)
                 {
                     return BadRequest(ModelState);
                 }*/

                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categoria.CategoriaId }, categoria);
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao Inserir a categoria no banco de dados");
            }
        }



        //METODO DE EDIÇÃO
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest();
                }
                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao alterar a categoria no banco de dados");
            }
        }


        //METODO DE EXCLUSÃO
        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
                //var produtos = _context.Produtos.Find(id);
                if (categoria == null)
                {
                    return BadRequest();
                }
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return Ok();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir a categoria no banco de dados");
            }
        }



    }

  

}
