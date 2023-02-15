using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using wsapicaixa.Context;
using wsapicaixa.Models.CaixaModel;

namespace wsapicaixa.Controllers.CaixaControllers;

[Route("api/[controller]")]
[ApiController]
public class CaixaController : ControllerBase
{

    private readonly AppDbContext _context;

    public CaixaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<Caixa>>> Get()
    {
        try
        {
            var caixas = await _context.Caixas.AsNoTracking().ToListAsync();

            if (caixas is null)
            {
                return NotFound("Não existe caixas cadastrados");
            }

            return caixas;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }

    }

    [HttpGet("{id}", Name = "ListOneCaixa")]
    public async Task<ActionResult<Caixa>> Get(string id)
    {

        try
        {
            var caixa = await _context.Caixas.FirstOrDefaultAsync(c => c.Id == id);

            if (caixa is null)
            {
                return NotFound("Caixa Não Encontrado");
            }

            return caixa;
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }



    }

    [HttpPost]
    public ActionResult Post(Caixa caixa)
    {

        try
        {
            if (caixa is null)
            {
                return BadRequest();
            }

            Guid id = Guid.NewGuid();
            string idAsString = id.ToString();

            caixa.Id = idAsString;

            _context.Caixas.Add(caixa);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ListOneCaixa", new { id = caixa.Id }, caixa);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }


    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, Caixa caixa)
    {

        try
        {
            if (id != caixa.Id)
            {
                return BadRequest("ID Não correspondente ao dado a ser alterado");
            }

            _context.Entry(caixa).State = EntityState.Modified;

            _context.SaveChanges();

            return Ok(caixa);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }



    }

    [HttpDelete("{id}")]

    public ActionResult Delete(string id)
    {

        try
        {
            var caixa = _context.Caixas.FirstOrDefault(c => c.Id == id);

            if (caixa is null)
            {
                return NotFound("Caixa não encontrado para deletar");
            }

            _context.Caixas.Remove(caixa);
            _context.SaveChanges();

            return Ok(caixa);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }

    }


}
