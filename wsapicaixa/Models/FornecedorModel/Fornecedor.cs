using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wsapicaixa.Models.FornecedorModel;
[Table("tbl_fornecedor")]
public class Fornecedor
{
    [Key]
    public string? Id { get; set; }

    [Required]
    [StringLength(255)]
    public string? Nome_Fornecedor { get; set; }
    
    [Required]
    [StringLength(11)]
    public string? Cpf { get; set; }
   
    [Required]
    public int Banco { get; set; }

    [Required]
    [StringLength(5)]
    public string? Agencia { get; set; }

}
