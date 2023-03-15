using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using wsapicaixa.DTOs.CaixaDTOs;
using wsapicaixa.DTOs.FornecedorDTOs;
using wsapicaixa.Models.CaixaModel;
using wsapicaixa.Models.FornecedorModel;
using wsapicaixa.Repository;
using wsapicaixa.Services;

namespace wsapicaixa.Controllers.FornecedorControllers;

[Route("api/[controller]")]
[ApiController]
public class FornecedorController : ControllerBase
{

    private readonly IUnitOfWork uof;
    private readonly IMapper _mapper;
    private readonly IMessageProducer _messagePublisher;
    public FornecedorController(IUnitOfWork context, IMapper mapper, IMessageProducer messagePublisher)
    {
        uof = context;
        _mapper = mapper;
        _messagePublisher = messagePublisher;   
    }

    [HttpGet]
    public  ActionResult<IEnumerable<FornecedorDTO>> Get()
    {
        try
        {
            var fornecedores =  uof.FornecedorRepository.GetAll().ToList();

            if (fornecedores is null)
            {
                return NotFound("Não existe fornecedores cadastrados");
            }

            var fornecedoresDto = _mapper.Map<List<FornecedorDTO>>(fornecedores);

            _messagePublisher.SendMessage(fornecedores);

            return fornecedoresDto;
        }
        catch (Exception)
        {
           return StatusCode(StatusCodes.Status500InternalServerError,"Ocorreu um erro no processamento");
        }
      
    }


    [HttpGet("id", Name = "ListOneFornecedor")]
    public ActionResult<FornecedorDTO> Get(string id) 
    {

        try
        {
            var fornecedor =  uof.FornecedorRepository.GetById(f => f.Id == id);

            if (fornecedor is null)
            {
                return NotFound("Não existe fornecedores cadastrados");
            }

            var fornecedorDto = _mapper.Map<FornecedorDTO>(fornecedor);

            return fornecedorDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }
        
    }

    [HttpPost]

    public ActionResult Post([FromBody] FornecedorCreateDTO fornecedorCreateDto)
    {

        try
        {

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorCreateDto);

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

            var fornecedorCreateDTO = _mapper.Map<FornecedorCreateDTO>(fornecedor);

            _messagePublisher.SendMessage(fornecedor);

            return new CreatedAtRouteResult("ListOneFornecedor", new { id = fornecedor.Id }, fornecedorCreateDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }


    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, FornecedorDTO fornecedorDto)
    {

        try
        {

            if (id != fornecedorDto.Id)
            {
                return BadRequest("ID Não correspondente ao dado a ser alterado");
            }

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);

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

    public ActionResult<FornecedorDTO> Delete(string id)
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

            var fornecedorDTO = _mapper.Map<FornecedorDTO>(fornecedor);

            return Ok(fornecedorDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no processamento");
        }

    }


}
