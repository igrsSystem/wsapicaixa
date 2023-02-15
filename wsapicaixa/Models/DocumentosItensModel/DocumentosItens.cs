using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wsapicaixa.Models.DocumentosItensModel;

[Table("tbl_documentos_itens")]
public class DocumentosItens
{
    [Key]
    public string? Id { get; set; }

    [Required]
    public string? Id_documento { get; set; }
    [Required]
    [StringLength(11)]
    public int Cpf_forncedor { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Valor_item_documento { get; set; }
}
