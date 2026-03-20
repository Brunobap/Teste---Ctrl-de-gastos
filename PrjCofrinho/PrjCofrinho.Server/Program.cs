using Microsoft.AspNetCore.Http.HttpResults;
using PrjCofrinho.Server.Classes;

var builder = WebApplication.CreateBuilder(args);

#region Configuração dos serviços rodados no backend
// Adicionar as funções personalizadas como serviços
builder.Services.AddSingleton<IPessoas>(new PessoasService());

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
//builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
#endregion

var app = builder.Build();

#region Mapeamentos do CRUD de Pessoas
// Pegar todas as pessoas
app.MapGet("/pessoas", (IPessoas servico) => servico.GetPessoas());
// Pegar uma pessoa pelo seu ID
app.MapGet("/pessoas/{id}", Results<Ok<Pessoas>, NotFound> (int id, IPessoas servico) => {
    // Chamar a busca pela pessoa
    var alvo = servico.GetPessoaById(id);

    // Voltar se foi possível acha-la
    if (alvo != null) return TypedResults.Ok(alvo);
    else return TypedResults.NotFound();
});

// Criar uma nova pessoa
app.MapPost("/pessoas", (Pessoas pessoa, IPessoas servico) => {
    // Mandar gravar no banco de dados
    servico.CreatePessoa(pessoa);

    // Retornar um status positivo
    return TypedResults.Created("/pessoas", pessoa);
});

// Atualizar uma pessoa
app.MapPut("/pessoas", Results<Ok<Pessoas>, NotFound>(Pessoas pessoa, IPessoas servico) => {
    // Mandar atualizar a tabela
    var alvo = servico.UpdatePessoa(pessoa);

    // Retornar se foi possível fazer isso
    if (alvo != null) return TypedResults.Ok(alvo);
    else return TypedResults.NotFound();
});

// Remover uma pessoa
app.MapDelete("/pessoas", (Pessoas pessoa, IPessoas servico) => {
    // Mandar apagar o item
    servico.DeletePessoa(pessoa);

    // Retornar que o item não está lá
    return TypedResults.Ok();
});
// Remover uma pessoa pelo seu ID
app.MapDelete("/pessoas/{id}", (int id, IPessoas servico) => {
    // Mandar achar e apagar o item
    servico.DeletePessoaById(id);

    // Retornar que o item não está lá
    return TypedResults.Ok();
});
#endregion

// Configure the HTTP request pipeline.
//app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

//app.MapDefaultEndpoints();

//app.UseFileServer();

app.Run();

// Classes do banco de dados "traduzidas" para o backend
public record Categorias(int Id_Categoria, String Descricao, int Finalidade);
public record Transacoes(int Id_Transacao, String Descricao, int Valor, bool Tipo, int Id_Categoria, int Id_Pessoa);
