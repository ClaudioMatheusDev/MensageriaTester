var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProdutoAdicionadoConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("admin");
            h.Password("password");
        });

        cfg.ReceiveEndpoint("produto-adicionado-queue", e =>
        {
            e.ConfigureConsumer<ProdutoAdicionadoConsumer>(context);
        });
    });
});

app.Run();

