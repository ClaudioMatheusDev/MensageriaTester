using System;

public class ProdutoAdicionadoEvent
{
    public int ProdutoId { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}