using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wsapicaixa.Models.AreasCompanyModel;


[Table("tbl_area_companys")]
public class AreaCompany
{
    [Key]
    [StringLength(36)]
    public string? Id { get; set; }
    [Required]
    public int Codigo_Area { get; set; }
    [Required]
    [StringLength(255)]
    public string? Descricao { get; set; }

    public DateTime Dh_criacao { get; set; }
}
