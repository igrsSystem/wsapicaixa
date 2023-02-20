using wsapicaixa.Context;
using wsapicaixa.Repository.CaixaRepository;
using wsapicaixa.Repository.FornecedorRepository;

namespace wsapicaixa.Repository;

public class UnitOfWork: IUnitOfWork
{
    private CaixasRepository _caixaRepo;
    private FornecedoresRepository _fornecedorRepo;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext contexto)
    {
        _context = contexto;
    }

    public ICaixasRepository CaixaRepository
    { 
        get 
        {
            return _caixaRepo = _caixaRepo ?? new CaixasRepository(_context);
        }
    }

    public IFornecedoresRepository FornecedorRepository
    {
        get
        {
            return _fornecedorRepo = _fornecedorRepo ?? new FornecedoresRepository(_context);
        }
    }

    public void Commit()
    {
      _context.SaveChanges();
    }

    public void Dispose()
    { 
      _context.Dispose(); 
    }
}
