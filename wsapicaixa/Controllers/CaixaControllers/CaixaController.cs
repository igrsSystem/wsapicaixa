using Microsoft.AspNetCore.Mvc;
using wsapicaixa.Models.CaixaModel;
using wsapicaixa.Repository;

namespace wsapicaixa.Controllers.CaixaControllers;

[Route("api/[controller]")]
[ApiController]
public class CaixaController : ControllerBase
{

    private readonly IUnitOfWork uof;

    public CaixaController(IUnitOfWork context)
    {
        uof = context;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Caixa>> Get()
    {
        try
        {
            var caixas =  uof.CaixaRepository.GetAll().ToList();

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
    public ActionResult<Caixa> Get(string id)
    {

        try
        {
            var caixa = uof.CaixaRepository.GetById(_caixa => _caixa.Id == id);

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
    public ActionResult Post([FromBody] Caixa caixa)
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

            uof.CaixaRepository.Add(caixa);
            uof.Commit();

            return new CreatedAtRouteResult("ListOneCaixa", new { id = caixa.Id }, caixa);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }


    }

    [HttpPut("{id}")]
    public ActionResult Put(string id,[FromBody] Caixa caixa)
    {

        try
        {
            if (id != caixa.Id)
            {
                return BadRequest("ID Não correspondente ao dado a ser alterado");
            }

            uof.CaixaRepository.Update(caixa);

            uof.Commit();

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
            var caixa = uof.CaixaRepository.GetById(_caixa => _caixa.Id == id);

            if (caixa is null)
            {
                return NotFound("Caixa não encontrado para deletar");
            }

            uof.CaixaRepository.Delete(caixa);
            uof.Commit();

            return Ok(caixa);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }

    }


}
