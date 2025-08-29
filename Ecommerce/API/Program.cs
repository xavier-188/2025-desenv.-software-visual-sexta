Console.Clear();
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Funcionalidades - Requisições
//Endereço URL
//Método HTTP
app.MapGet("/", () => "Minha API");
app.MapGet("/funcionalidade", () => "Segunda funcionalidade");
app.MapPost("/funcionalidade", () => "Funcionalidade com Post");

app.Run();
