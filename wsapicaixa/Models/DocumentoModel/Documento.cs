using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wsapicaixa.Models.DocumentoModel;

[Table("tbl_documento")]
public class Documento
{
    [Key]
    public string? Id { get; set; }
    public int Numero_caixa { get; set; }
    [Required]
    public int Ano_documento { get; set; }
    [Required]
    public int Numero_documento { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Valor_documento { get; set; }
}
