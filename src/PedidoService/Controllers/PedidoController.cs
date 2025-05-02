using Microsoft.AspNetCore.Mvc;
using PedidoService.Data;
using PedidoService.Models;

namespace PedidoService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
	private readonly IPedidoRepository _repo;

	public PedidoController(IPedidoRepository repo)
	{
		_repo = repo;
	}

	[HttpPost]
	public async Task<IActionResult> Create(Pedido pedido)
	{
		await _repo.Create(pedido);
		return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var pedido = await _repo.GetById(id);
		return pedido == null ? NotFound() : Ok(pedido);
	}
}