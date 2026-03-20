using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Rewrite;
using PrjCofrinho.Server.Classes;

var builder = WebApplication.CreateBuilder(args);

#region Serviços que rodarão no pipeline
// Controle da lista de pessoas
builder.Services.AddSingleton<IPessoas>(new PessoasService());
#endregion

var app = builder.Build();

#region Mapeamento do CRUD de Pessoas
// Pegar todas as pessoas
app.MapGet("/pessoas", (IPessoas servico) => servico.GetPessoas());
// Pegar uma pessoa especifica pelo seu ID
app.MapGet("/pessoas/{id}", Results<Ok<Pessoas>, NoContent> (IPessoas servico, int id) =>
{
    // Mandar a busca para a tabela
    var alvo = servico.GetPessoaById(id);

    // Ver se a busca retornou algo
    return alvo != null
        // Se sim, retornar status positivo e o objeto
        ? TypedResults.Ok(alvo)
        // se não, só voltar status negativo
        : TypedResults.NoContent();
});

// Criar um registro de pessoa
app.MapPost("/pessoas", (IPessoas servico, Pessoas pessoa) => 
{
    // Mandar criar o novo registro
    servico.CreatePessoa(pessoa);

    // Retornar que houve sucesso, e o registro que foi feito
    return TypedResults.Ok(pessoa);
});

// Atualizar um registro da tabela
app.MapPut("/pessoas", Results<Ok<Pessoas>, NoContent> (IPessoas servico, Pessoas pessoa) =>
{
    // Mandar a atualizar a tabela
    var alvo = servico.UpdatePessoa(pessoa);

    // Ver se o item estava na tabela mesmo
    return alvo != null
        // Se sim, retornar status positivo e o objeto
        ? TypedResults.Ok(alvo)
        // se não, só voltar status negativo
        : TypedResults.NoContent();
});

// Apagar uma pessoa pelo seu ID
app.MapDelete("/pessoas/{id}", (IPessoas servico, int id) =>
{
    servico.DeletePessoaById(id);

    return TypedResults.Ok();
});
#endregion

#region Middlewares
// Middleware do próprio .NET, muda o URL de http para https
app.UseHttpsRedirection();

// Redireciona os URL de 'pessoa' para 'pesssoas'
app.UseRewriter(new RewriteOptions().AddRedirect("pessoa/(.*)", "pessoas/$1"));
#endregion

app.Run();