using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wsapicaixa.Context;
using wsapicaixa.Models;
using wsapicaixa.Models.FornecedorModel;


namespace wsapicaixa.Controllers.FornecedorControllers;

[Route("api/[controller]")]
[ApiController]
public class FornecedorController : ControllerBase
{
    private readonly AppDbContext _context;

    public FornecedorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fornecedor>>> Get()
    {
        try
        {
            var fornecedores = await _context.Fornecedores.ToListAsync();

            if (fornecedores is null)
            {
                return NotFound("Não existe fornecedores cadastrados");
            }

            return fornecedores;
        }
        catch (Exception)
        {
           return StatusCode(StatusCodes.Status500InternalServerError,"Ocorreu um erro no processamento");
        }
      
    }


    [HttpGet("id")]
    public async Task<ActionResult<Fornecedor>> Get(string id) 
    {

        try
        {
            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Id == id);

            if (fornecedor is null)
            {
                return NotFound("Não existe fornecedores cadastrados");
            }
            return fornecedor;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }
        
    }

    [HttpPost]

    public ActionResult Post(Fornecedor fornecedor)
    {

        try
        {
            if (fornecedor is null)
            {
                return BadRequest();
            }

            var verificaFornecedor = _context.Fornecedores.FirstOrDefault(f => f.Cpf == fornecedor.Cpf);

            if (verificaFornecedor is not null)
            {
                return BadRequest($"Forncedor com esse CFP={fornecedor.Cpf} já cadastrado...");
            }

            Guid id = Guid.NewGuid();
            string idAsString = id.ToString();

            fornecedor.Id = idAsString;

            _context.Fornecedores.Add(fornecedor);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ListOneFornecedor", new { id = fornecedor.Id }, fornecedor);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }


    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, Fornecedor fornecedor)
    {

        try
        {

            if (id != fornecedor.Id)
            {
                return BadRequest("ID Não correspondente ao dado a ser alterado");
            }

            /*
             * var verificaFornecedor = _context.Fornecedores.FirstOrDefault(f => f.Cpf == fornecedor.Cpf);

            if (verificaFornecedor is not null)
            {
                return BadRequest("Forncedor com esse CFP já cadastrado...");
            } 
            */

            _context.Entry(fornecedor).State = EntityState.Modified;

            _context.SaveChanges();

            return Ok(fornecedor);

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
            var fornecedor = _context.Fornecedores.FirstOrDefault(c => c.Id == id);

            if (fornecedor is null)
            {
                return NotFound("Caixa não encontrado para deletar");
            }

            _context.Fornecedores.Remove(fornecedor);
            _context.SaveChanges();

            return Ok(fornecedor);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }

    }


}
