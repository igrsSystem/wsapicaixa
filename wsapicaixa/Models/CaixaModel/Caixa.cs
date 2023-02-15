using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wsapicaixa.Models.CaixaModel;

[Table("tbl_caixa")]
public class Caixa
{
    [Key]
    [StringLength(36)]
    public string? Id { get; set; }
    [Required]
    public int Numero_caixa { get; set; }
    [Required]
    [StringLength(255)]
    public string? Descricao { get; set; }
}
