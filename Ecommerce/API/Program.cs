using API.Model;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

Console.Clear();

var builder = WebApplication.CreateBuilder(args);

//Adicionar o serviço de banco de dados na aplicação.
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

List<Produto> produtos = new List<Produto>();
// {
//     new Produto { Nome = "Notebook", Quantidade = 10, Preco = 3500.00 },
//     new Produto { Nome = "Smartphone", Quantidade = 25, Preco = 2200.00 },
//     new Produto { Nome = "Fone de Ouvido", Quantidade = 50, Preco = 199.90 },
//     new Produto { Nome = "Monitor", Quantidade = 15, Preco = 899.99 },
//     new Produto { Nome = "Teclado Mecânico", Quantidade = 20, Preco = 350.00 }
// };

//Funcionalidades
//Requisições
// - Endereço/URL
// - Método HTTP
// - Dados: Rota(URL) e corpo

//Respostas
//- Código de Status
//

//Métodos HTTP
// GET: Recupera dados de um servidor sem modificar o recurso.
// POST: Envia dados ao servidor para criar um novo recurso.
// PUT: Atualiza completamente um recurso existente no servidor.
// PATCH: Atualiza parcialmente um recurso existente.
// DELETE: Remove um recurso do servidor.

app.MapGet("/", () => "API de Produtos");

//Listar produtos
app.MapGet("/api/produto/listar", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.Produtos.Any())
    {
        return Results.Ok(ctx.Produtos.ToList());
    }
    return Results.BadRequest("Lista vazia!");
});

//Cadastrar Produto
app.MapPost("/api/produto/cadastrar", ([FromBody] Produto produto, [FromServices] AppDbContext ctx) =>
{
    Produto? resultado =
      ctx.Produtos.FirstOrDefault(p => p.Nome == produto.Nome);
    if (resultado is not null)
    {
        return Results.Conflict("Produto já existente!");
    }
    ctx.Produtos.Add(produto);
    ctx.SaveChanges();
    return Results.Created("", produto);
});

// //Buscar Produto
// app.MapGet("/api/produto/buscar/{nome}", ([FromRoute] string nome, [FromServices] AppDbContext ctx) =>
// {
//     // //Expressão Lambda
//     // Produto? produtoBuscado = ctx.Produtos.FirstOrDefault(p => p.Nome == nome);

//     // if (produtoBuscado == null)
//     // {
//     //     return Results.NotFound("Produto não encontrado");
//     // }
//     // return Results.Ok(produtoBuscado);
// });

//Buscar Produto pelo Id
app.MapGet("/api/produto/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    //Expressão Lambda
    Produto? produtoBuscado = ctx.Produtos.Find(id); //Find busca a chave primária.

    if (produtoBuscado == null)
    {
        return Results.NotFound("Produto não encontrado");
    }
    return Results.Ok(produtoBuscado);
});

//Remover Produto
app.MapDelete("/api/produto/remover/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    //Expressão Lambda
    Produto? produtoBuscado = ctx.Produtos.Find(id);

    if (produtoBuscado == null)
    {
        return Results.NotFound("Produto não encontrado");
    }

    produtos.Remove(produtoBuscado);
    ctx.SaveChanges();
    return Results.Ok("Produto Removido!");
});


//Alterar um produto pelo id
app.MapPatch("/api/produto/alterar/{id}", (
[FromRoute] string id,
[FromBody] Produto produtoAlterado,
[FromServices] AppDbContext ctx) =>
{
    //Expressão Lambda
    Produto? produtoBuscado = ctx.Produtos.Find(id);

    if (produtoBuscado == null)
    {
        return Results.NotFound("Produto não encontrado");
    }

    produtoBuscado.Nome = produtoAlterado.Nome;
    produtoBuscado.Quantidade = produtoAlterado.Quantidade;
    produtoBuscado.Preco = produtoAlterado.Preco;
    ctx.Produtos.Update(produtoBuscado);
    ctx.SaveChanges();


    return Results.Ok("Produto alterado com sucesso!");

});


app.Run();


