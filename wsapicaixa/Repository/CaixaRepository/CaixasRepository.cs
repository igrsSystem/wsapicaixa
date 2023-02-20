using wsapicaixa.Context;
using wsapicaixa.Models.CaixaModel;
using wsapicaixa.Repository;

namespace wsapicaixa.Repository.CaixaRepository;

public class CaixasRepository : Repository<Caixa>, ICaixasRepository
{

    public CaixasRepository(AppDbContext context) : base(context)
    {
    }
    public IEnumerable<Caixa> GetAll()
    {
        return Get().OrderBy(caixa => caixa.Numero_caixa).ToList();
    }
}
