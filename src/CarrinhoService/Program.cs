using MassTransit;
using Microsoft.EntityFrameworkCore;
using CarrinhoService.Data;
using CarrinhoService.Models;
using CarrinhoService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Configuração do SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do RabbitMQ com MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProdutoAtualizadoConsumer>(); // Registrar Consumer

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("admin");
            h.Password("password");
        });

        // Configurar fila para o Consumer
        cfg.ReceiveEndpoint("produto-atualizado-queue", e =>
        {
            e.ConfigureConsumer<ProdutoAtualizadoConsumer>(context);
        });
    });
});

// Repositório
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migração automática
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();