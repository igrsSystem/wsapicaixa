namespace wsapicaixa.DTOs.FornecedorDTOs;

public class FornecedorCreateDTO
{
    public string? Nome_Fornecedor { get; set; }
    public string? Cpf { get; set; }
    public int Banco { get; set; }
    public string? Agencia { get; set; }
}
