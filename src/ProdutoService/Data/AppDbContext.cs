using Microsoft.EntityFrameworkCore;
using ProdutoService.Models;

namespace ProdutoService.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Produto> Produtos { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Produto>().HasData(
			new Produto { Id = 1, Nome = "Notebook", Descricao = "Notebook Dell i7", Preco = 5000, Estoque = 10 },
			new Produto { Id = 2, Nome = "Mouse", Descricao = "Mouse sem fio", Preco = 120, Estoque = 50 }
		);
	}
}