var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapDefaultEndpoints();

app.UseFileServer();

app.Run();

// Classes do banco de dados "traduzidas" para o backend
public record Categorias(int Id_Categoria, String Descricao, int Finalidade);
public record Transacoes(int Id_Transacao, String Descricao, int Valor, bool Tipo, int Id_Categoria, int Id_Pessoa);
