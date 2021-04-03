using Microsoft.AspNetCore.Mvc;
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
            return _context.Produtos.ToList();
        }

    }
}
