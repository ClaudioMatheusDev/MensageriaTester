using ProdutoService.Models;

namespace ProdutoService.Data;

public interface IProdutoRepository
{
    Task<List<Produto>> GetAll();
    Task<Produto?> GetById(int id);
    Task Create(Produto produto);
    Task Update(Produto produto);
    Task Delete(int id);
}