using MassTransit;
using Microsoft.EntityFrameworkCore;
using PedidoService.Data;
using PedidoService.Models;
using PedidoService.Sagas;

var builder = WebApplication.CreateBuilder(args);

// Configuração do SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do RabbitMQ com MassTransit
builder.Services.AddMassTransit(x =>
{
    // Registrar Saga
    x.AddSaga<ProcessamentoPedidoSaga>()
     .InMemoryRepository(); // Pode substituir por Entity Framework depois

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("admin");
            h.Password("password");
        });
    });
});

// Repositório
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

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