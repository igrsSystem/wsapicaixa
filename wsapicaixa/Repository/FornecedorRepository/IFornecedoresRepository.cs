using Microsoft.AspNetCore.Mvc;
using wsapicaixa.Models.FornecedorModel;

namespace wsapicaixa.Repository.FornecedorRepository;

public interface IFornecedoresRepository: IRepository<Fornecedor>
{
    IEnumerable<Fornecedor> GetAll();
    ActionResult<Fornecedor> GetCpf(string Cpf);
}
