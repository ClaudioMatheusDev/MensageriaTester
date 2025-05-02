using System;

public class ProdutoAdicionadoConsumer : IConsumer<ProdutoAdicionadoEvent>
{
    public async Task Consume(ConsumeContext<ProdutoAdicionadoEvent> context)
    {
        Console.WriteLine($"Produto recebido: {context.Message.Nome} (R${context.Message.Preco})");
        // Lógica para atualizar o carrinho...
    }
}