using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wsapicaixa.Context;
using wsapicaixa.Models;
using wsapicaixa.Models.FornecedorModel;
using wsapicaixa.Repository;

namespace wsapicaixa.Controllers.FornecedorControllers;

[Route("api/[controller]")]
[ApiController]
public class FornecedorController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork uof;
    public FornecedorController(IUnitOfWork context)
    {
        uof = context;
    }

    [HttpGet]
    public  ActionResult<IEnumerable<Fornecedor>> Get()
    {
        try
        {
            var fornecedores =  uof.FornecedorRepository.GetAll().ToList();

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


    [HttpGet("id", Name = "ListOneFornecedor")]
    public ActionResult<Fornecedor> Get(string id) 
    {

        try
        {
            var fornecedor =  uof.FornecedorRepository.GetById(f => f.Id == id);

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

            var verificaFornecedor = uof.FornecedorRepository.GetCpf(fornecedor.Cpf);

            if (verificaFornecedor.Value is not null)
            {
                return BadRequest($"Forncedor com esse CFP={fornecedor.Cpf} já cadastrado...");
            }

            Guid id = Guid.NewGuid();
            string idAsString = id.ToString();

            fornecedor.Id = idAsString;

            uof.FornecedorRepository.Add(fornecedor);
            uof.Commit();

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

             var verificaFornecedor =  uof.FornecedorRepository.GetCpf(fornecedor.Cpf);

            if (verificaFornecedor.Value is not null && (verificaFornecedor.Value.Id != id))
            {
                return BadRequest("Forncedor com esse CPF já cadastrado...");
            } 
            
            uof.FornecedorRepository.Update(fornecedor);
         
            uof.Commit();

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

            var fornecedor = uof.FornecedorRepository.GetById(c => c.Id == id);

            if (fornecedor is null)
            {
                return NotFound("Fornecedor não encontrado para deletar");
            }

            uof.FornecedorRepository.Delete(fornecedor);
            uof.Commit();

            return Ok(fornecedor);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }

    }


}
