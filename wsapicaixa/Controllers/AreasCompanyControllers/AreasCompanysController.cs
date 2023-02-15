using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wsapicaixa.Context;
using wsapicaixa.Models;
using wsapicaixa.Models.AreasCompanyModel;
using wsapicaixa.Models.FornecedorModel;

namespace wsapicaixa.Controllers.AreasCompanyControllers;

[Route("api/[controller]")]
[ApiController]
public class AreasCompanysController : ControllerBase
{
    private readonly AppDbContext _context;

    public AreasCompanysController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AreaCompany>>> Get()
    {
        try
        {
            var areasCompanys = await _context.AreaCompanys.AsNoTracking().ToListAsync();

            if (areasCompanys is null) 
            {
                return NotFound("Não Existe Aréas Cadastradas");
            }

            return areasCompanys;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AreaCompany>> Get(string id)
    {
        try
        {
            var areasCompany = await _context.AreaCompanys.AsNoTracking().FirstOrDefaultAsync(ar => ar.Id == id);

            if (areasCompany is null)
            {
                return NotFound("Área não econtrada");
            }
            return areasCompany;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }
    }

    [HttpPost]
    public ActionResult Post(AreaCompany areaCompany) 
    {
        try
        {

            if (areaCompany is null)
            {
                return BadRequest("Gentileza preencher o formulario");
            }

            var verificaAreasCompany =  _context.AreaCompanys.AsNoTracking().FirstOrDefault(ar => ar.Codigo_Area == areaCompany.Codigo_Area);

            if (verificaAreasCompany is not null)
            {
                return BadRequest("Área já cadastrada....");
            }

            Guid id = Guid.NewGuid();
            string idAsString = id.ToString();

            areaCompany.Id = idAsString;

            _context.AreaCompanys.Add(areaCompany);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ListOneCaixa", new { id = areaCompany.Id }, areaCompany);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }
    
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, AreaCompany areaCompany)
    {

        try
        {

            if (id != areaCompany.Id)
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

            _context.Entry(areaCompany).State = EntityState.Modified;

            _context.SaveChanges();

            return Ok(areaCompany);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }

    }
}
