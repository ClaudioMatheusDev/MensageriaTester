namespace PedidoService.Models;

public class Pedido
{
    public int Id { get; set; }
    public string UsuarioId { get; set; }
    public DateTime DataPedido { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public string Status { get; set; } = "Pendente";
    public List<ItemPedido> Itens { get; set; } = new();
}
