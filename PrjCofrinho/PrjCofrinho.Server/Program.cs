using PrjCofrinho.Server.Classes;

var builder = WebApplication.CreateBuilder(args);

#region Configuração dos serviços rodados no backend
// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Adicionar as funções personalizadas como serviços
builder.Services.AddSingleton<IPessoas>(new PessoasService());
#endregion

var app = builder.Build();

/*
#region Mapeamentos do CRUD de Pessoas
// Pegar todas as pessoas
app.MapGet("/pessoas", (IPessoas servico) => servico.GetPessoas());
// Pegar uma pessoa pelo seu ID
app.MapGet("/pessoas/{id}", (int id, IPessoas servico) => servico.GetPessoaById(id));

// Criar uma nova pessoa
app.MapPost("/pessoas", (Pessoas pessoa, IPessoas servico) => servico.CreatePessoa(pessoa));

// Atualizar uma pessoa
app.MapPut("/pessoas", (Pessoas pessoa, IPessoas servico) => servico.UpdatePessoa(pessoa));

// Remover uma pessoa
app.MapDelete("/pessoas", (Pessoas pessoa, IPessoas servico) => servico.DeletePessoa(pessoa));
// Remover uma pessoa pelo seu ID
app.MapDelete("/pessoas/{id}", (int id, IPessoas servico) => servico.DeletePessoaById(id));
#endregion
*/
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
