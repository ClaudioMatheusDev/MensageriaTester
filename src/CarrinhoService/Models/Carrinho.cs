namespace CarrinhoService.Models;

public class Carrinho
{
	public int Id { get; set; }
	public string UsuarioId { get; set; } 
	public List<ItemCarrinho> Itens { get; set; } = new();
}
