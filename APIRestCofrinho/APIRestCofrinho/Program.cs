using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using PrjCofrinho.Server.Classes;

var builder = WebApplication.CreateBuilder(args);

#region Serviços que rodarão no pipeline
// Controle da lista de pessoas
builder.Services.AddSingleton<IPessoas>(new PessoasService());

// Controle da lista de categorias
builder.Services.AddSingleton<ICategorias>(new CategoriasService());

// Controle da lista de transações
builder.Services.AddSingleton<ITransacoes>(new TransacoesService());
#endregion

var app = builder.Build();

#region Mapeamoentos  
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
app.MapPut("/pessoas", (IPessoas servico, Pessoas pessoa) =>
{
    // Mandar a atualizar a tabela
    servico.UpdatePessoa(pessoa);

    // Retornar que houve sucesso, e a atualização foi feita
    return TypedResults.Ok();
});

// Apagar uma pessoa pelo seu ID
app.MapDelete("/pessoas/{id}", (IPessoas servico, int id) =>
{
    // Mandar apagar o item
    servico.DeletePessoaById(id);

    // Informar que o item foi mesmo apagado
    return TypedResults.Ok();
});
#endregion

    #region Mapeamento do CRUD de Categorias
// Pegar todas as categorias
app.MapGet("/categorias", (ICategorias servico) => servico.GetCategorias());
// Pegar uma categoria especifica pelo seu ID
app.MapGet("/categorias/{id}", Results<Ok<Categorias>, NoContent> (ICategorias servico, int id) =>
{
    // Mandar a busca para a tabela
    var alvo = servico.GetCategoriaById(id);

    // Ver se a busca retornou algo
    return alvo != null
        // Se sim, retornar status positivo e o objeto
        ? TypedResults.Ok(alvo)
        // se não, só voltar status negativo
        : TypedResults.NoContent();
});

// Criar um registro de categoria
app.MapPost("/categorias", (ICategorias servico, Categorias categoria) =>
{
    // Mandar criar o novo registro
    servico.CreateCategoria(categoria);

    // Retornar que houve sucesso, e o registro que foi feito
    return TypedResults.Ok(categoria);
});

// Atualizar um registro da tabela
app.MapPut("/categorias", (ICategorias servico, Categorias categoria) =>
{
    // Mandar a atualizar a tabela
   servico.UpdateCategoria(categoria);

    // Retornar que houve sucesso, e a atualização foi feita
    return TypedResults.Ok();
});

// Apagar uma categoria pelo seu ID
app.MapDelete("/categorias/{id}", (ICategorias servico, int id) =>
{
    // Mandar apagar o item
    servico.DeleteCategoriaById(id);

    // Informar que o item foi mesmo apagado
    return TypedResults.Ok();
});
#endregion

    #region Mapeamento do CRUD de Transações
// Pegar todas as transações
app.MapGet("/transacoes", (ITransacoes servico) => servico.GetTransacoes());
// Pegar uma transação especifica pelo seu ID
app.MapGet("/transacoes/{id}", Results<Ok<Transacoes>, NoContent> (ITransacoes servico, int id) =>
{
    // Mandar a busca para a tabela
    var alvo = servico.GetTransacaoById(id);

    // Ver se a busca retornou algo
    return alvo != null
        // Se sim, retornar status positivo e o objeto
        ? TypedResults.Ok(alvo)
        // se não, só voltar status negativo
        : TypedResults.NoContent();
});

// Criar um registro de transação
app.MapPost("/transacoes", (ITransacoes servico, Transacoes transacao) =>
{
    // Mandar criar o novo registro
    servico.CreateTransacao(transacao);

    // Retornar que houve sucesso, e o registro que foi feito
    return TypedResults.Ok(transacao);
});

// Atualizar um registro da tabela
app.MapPut("/transacoes", (ITransacoes servico, Transacoes transacao) =>
{
    // Mandar a atualizar a tabela
    servico.UpdateTransacao(transacao);

    // Retornar que houve sucesso, e a atualização foi feita
    return TypedResults.Ok();
});

// Apagar uma transação pelo seu ID
app.MapDelete("/transacoes/{id}", (ITransacoes servico, int id) =>
{
    // Mandar apagar o item
    servico.DeleteTransacaoById(id);

    // Informar que o item foi mesmo apagado
    return TypedResults.Ok();
});
#endregion
#endregion

#region Middlewares
// Middleware do próprio .NET, muda o URL de http para https
app.UseHttpsRedirection(); 
// Redirecionamentos que podem ocorrer, por erro de digitação
// O mesmo redirecionador serve para todos, não precisa criar variações, só regras para um
app.UseRewriter(new RewriteOptions()
    // 'pessoa' --> 'pesssoas'
    .AddRedirect("pessoa/(.*)", "pessoas/$1")
    
    // 'categoria' --> 'categorias'
    .AddRedirect("categoria/(.*)", "categorias/$1")
    
    // URLs com caracteres latinos ("ç", "ã" e "õ") não podem ser lidos e nem usados para redirecionar
    // 'transacao' --> 'transacoes'
    .AddRedirect("transacao/(.*)", "transacoes/$1")
);
#endregion

app.Run();