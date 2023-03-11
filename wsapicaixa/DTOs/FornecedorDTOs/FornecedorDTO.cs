using System.ComponentModel.DataAnnotations;

namespace wsapicaixa.DTOs.FornecedorDTOs;

public class FornecedorDTO
{
    public string? Id { get; set; }
    public string? Nome_Fornecedor { get; set; }
    public string? Cpf { get; set; }
    public int Banco { get; set; }
    public string? Agencia { get; set; }
}
