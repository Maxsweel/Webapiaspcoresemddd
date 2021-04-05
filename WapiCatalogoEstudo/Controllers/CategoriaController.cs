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
        public async Task <ActionResult<IEnumerable<Categoria>>> Get()
        {
            try
            {
                return await _context.Categorias.AsNoTracking().ToListAsync();
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Erro ao obter as categorias no banco de dados");
            }
        }


        //PRODUTOS RELACIONADOS
        [HttpGet("Produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        {
            try
            {
                return await _context.Categorias.Include(x => x.Produtos).ToListAsync();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter o produto da categoria no banco de dados");
            }
        }




        //LISTAGEM UNICA
        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            try
            {
                var categoria = await _context.Categorias.AsNoTracking()//.AsNoTracking só é chamando se o comando for somente parar listar
                    .FirstOrDefaultAsync(c => c.CategoriaId == id);
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
        public async Task <ActionResult> Post([FromBody] Categoria categoria)
        {
            try
            {

                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);


            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao Inserir a categoria no banco de dados");
            }
        }



        //METODO DE EDIÇÃO
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest($"A categoria com id= {id} não foi encontrada");
                }
                _context.Entry(categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok($"Categoria atualizada com sucesso");
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar a categoria no banco de dados");
            }
        }


        //METODO DE EXCLUSÃO
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            try
            {
                // var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    return BadRequest($"A categoria com id= {id} não foi encontrada");
                }
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                return Ok("A categoria com foi excluida com sucesso");
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir a categoria no banco de dados");
            }
        }



    }

  

}
