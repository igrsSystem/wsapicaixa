using AutoMapper;
using wsapicaixa.DTOs.CaixaDTOs;
using wsapicaixa.DTOs.FornecedorDTOs;
using wsapicaixa.Models.CaixaModel;
using wsapicaixa.Models.FornecedorModel;

namespace wsapicaixa.DTOs.Mappings;

public class MappingProfile:Profile
{
	public MappingProfile()
	{
		CreateMap<Caixa, CaixaDTO>().ReverseMap();
        CreateMap<Caixa, CaixaCreateDTO>().ReverseMap();

        CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();
    }
}
