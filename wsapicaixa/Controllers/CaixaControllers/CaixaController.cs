using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using wsapicaixa.DTOs.CaixaDTOs;
using wsapicaixa.Models.CaixaModel;
using wsapicaixa.Repository;

namespace wsapicaixa.Controllers.CaixaControllers;

[Route("api/[controller]")]
[ApiController]
public class CaixaController : ControllerBase
{

    private readonly IUnitOfWork uof;
    private readonly IMapper _mapper;

    public CaixaController(IUnitOfWork context, IMapper mapper)
    {
        uof = context;
        _mapper = mapper;   
    }

    [HttpGet]

    public ActionResult<IEnumerable<CaixaDTO>> Get()
    {
        try
        {
            var caixas =  uof.CaixaRepository.GetAll().ToList(); 

            if (caixas is null)
            {
                return NotFound("Não existe caixas cadastrados");
            }

            var caixasDto = _mapper.Map<List<CaixaDTO>>(caixas);

            return caixasDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }

    }

    [HttpGet("{id}", Name = "ListOneCaixa")]
    public ActionResult<CaixaDTO> Get(string id)
    {

        try
        {
            var caixa = uof.CaixaRepository.GetById(_caixa => _caixa.Id == id);
          

            if (caixa is null)
            {
                return NotFound("Caixa Não Encontrado");
            }

            var caixaDto = _mapper.Map<CaixaDTO>(caixa);

            return caixaDto;
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }



    }

    [HttpPost]
    public ActionResult Post([FromBody] CaixaCreateDTO caixaCreateDto)
    {

        try
        {
            var caixa = _mapper.Map<Caixa>(caixaCreateDto);

            if (caixa is null)
            {
                return BadRequest();
            }

            Guid id = Guid.NewGuid();
            string idAsString = id.ToString();

            caixa.Id = idAsString;

            uof.CaixaRepository.Add(caixa);
            uof.Commit();

            var caixaCreateDTO = _mapper.Map<CaixaCreateDTO>(caixa);

            return new CreatedAtRouteResult("ListOneCaixa", new { id = caixa.Id }, caixaCreateDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }


    }

    [HttpPut("{id}")]
    public ActionResult Put(string id,[FromBody] CaixaDTO caixaDto)
    {

        try
        {

            if (id != caixaDto.Id)
            {
                return BadRequest("ID Não correspondente ao dado a ser alterado");
            }

            var caixa = _mapper.Map<Caixa>(caixaDto);

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

    public ActionResult<CaixaDTO> Delete(string id)
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

            var caixaDTO = _mapper.Map<CaixaDTO>(caixa);

            return Ok(caixaDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }

    }


}
