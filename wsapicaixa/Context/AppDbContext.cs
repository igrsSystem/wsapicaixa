using Microsoft.EntityFrameworkCore;
using wsapicaixa.Models.AreasCompanyModel;
using wsapicaixa.Models.CaixaModel;
using wsapicaixa.Models.DocumentoModel;
using wsapicaixa.Models.DocumentosItensModel;
using wsapicaixa.Models.FornecedorModel;

namespace wsapicaixa.Context;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

	public DbSet<Caixa>? Caixas { get; set; }
    public DbSet<Fornecedor>? Fornecedores { get; set; }
	public DbSet<AreaCompany>? AreaCompanys { get; set; }
    public DbSet<Documento>? Documentos { get; set; }
    public DbSet<DocumentosItens>? DocumentosItens { get; set; }
}
