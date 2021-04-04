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
            return _context.Categorias.AsNoTracking().ToList();
        }


        //LISTAGEM UNICA
        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.AsNoTracking()//.AsNoTracking só é chamando se o comando for somente parar listar
                .FirstOrDefault(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound();

            }
            return categoria;
        }

        //metodo de adição
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            /* if(IModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }*/

            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }



        //METODO DE EDIÇÃO
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }


        //METODO DE EXCLUSÃO
        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
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
        }



    }

  

}
