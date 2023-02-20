using wsapicaixa.Models.CaixaModel;

namespace wsapicaixa.Repository.CaixaRepository;

public interface ICaixasRepository:IRepository<Caixa>
{
    IEnumerable<Caixa> GetAll();
}
