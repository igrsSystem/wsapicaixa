using wsapicaixa.Repository.CaixaRepository;
using wsapicaixa.Repository.FornecedorRepository;

namespace wsapicaixa.Repository;

public interface IUnitOfWork
{
    ICaixasRepository CaixaRepository { get; }
    IFornecedoresRepository FornecedorRepository { get; }

    void Commit();
}
