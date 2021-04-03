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

        [HttpGet]
        public ActionResult<IEnumerable<Produtos>> Get()
        {
            return _context.Produtos.AsNoTracking().ToList();
        }

        //metodo de listagem em detalhe
        [HttpGet("{id}")]
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

    }
}
