using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wsapicaixa.Context;
using wsapicaixa.Models.DocumentoModel;

namespace wsapicaixa.Controllers.DocumentoControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documento>>> Get()
        {
            try
            {
                var documentos = await _context.Documentos.AsNoTracking().ToListAsync();

                if (documentos is null)
                {
                    return NotFound("Dados não econtrados...");
                }

                return documentos;


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
            }
        }
    }
}
