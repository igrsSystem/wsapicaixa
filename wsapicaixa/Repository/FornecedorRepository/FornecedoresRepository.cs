using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wsapicaixa.Context;
using wsapicaixa.Models.FornecedorModel;

namespace wsapicaixa.Repository.FornecedorRepository;

public class FornecedoresRepository: Repository<Fornecedor>,IFornecedoresRepository
{
    public FornecedoresRepository(AppDbContext context) : base(context)
    { 
    }

    public IEnumerable<Fornecedor> GetAll()
    {
        return Get().OrderBy(fornecedor => fornecedor.Banco).ToList();
       
    }

    public ActionResult<Fornecedor> GetCpf(string Cpf)
    {
    
        var fornecedor =  _context.Fornecedores.AsNoTracking().FirstOrDefault(findFornecedor => findFornecedor.Cpf == Cpf);
        
        return fornecedor;
      
    }
}
