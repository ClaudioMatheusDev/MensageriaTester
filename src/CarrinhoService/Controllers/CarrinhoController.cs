using Microsoft.AspNetCore.Mvc;
using CarrinhoService.Data;
using CarrinhoService.Models;

namespace CarrinhoService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarrinhoController : ControllerBase
{
    private readonly ICarrinhoRepository _repo;

    public CarrinhoController(ICarrinhoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> GetByUsuario(string usuarioId)
    {
        var carrinho = await _repo.GetByUsuario(usuarioId);
        return carrinho == null ? NotFound() : Ok(carrinho);
    }

    [HttpPost("{usuarioId}/itens")]
    public async Task<IActionResult> AddItem(string usuarioId, ItemCarrinho item)
    {
        await _repo.AddItem(usuarioId, item);
        return Ok();
    }

    [HttpDelete("{usuarioId}/itens/{itemId}")]
    public async Task<IActionResult> RemoveItem(string usuarioId, int itemId)
    {
        await _repo.RemoveItem(usuarioId, itemId);
        return NoContent();
    }
}