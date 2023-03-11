using System.ComponentModel.DataAnnotations;

namespace wsapicaixa.DTOs.CaixaDTOs;

public class CaixaDTO
{
    public string? Id { get; set; }
    public int Numero_caixa { get; set; }
    public string? Descricao { get; set; }
}
