using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

Console.Clear();
var builder = WebApplication.CreateBuilder(args);
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

app.MapGet("/api/produto/listar", () =>
{
    if (produtos.Count > 0)
    {
        return Results.Ok(produtos);
    }
    return Results.BadRequest("Lista vazia!");
});

app.MapPost("/api/produto/cadastrar", ([FromBody] Produto produto) =>
{
    foreach (Produto produtoCadastrado in produtos)
    {
        if (produtoCadastrado.Nome == produto.Nome)
        {
            return Results.Conflict("Produto já cadastrado!");
        }
    }
    produtos.Add(produto);
    return Results.Created("", produto);
});

app.MapGet("/api/produto/buscar/{nome}", ([FromRoute] string nome) =>
{
    //Expressão Lambda
    Produto? produtoBuscado = produtos.FirstOrDefault(p => p.Nome == nome);

    if (produtoBuscado == null)
    {
        return Results.NotFound("Produto não encontrado");
    }
    return Results.Ok(produtoBuscado);
});

app.Run();
