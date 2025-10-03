using System;

namespace API.Model;

public class Categoria
{

    //Primary Key
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }

    public DateTime CriadoEm { get; set; }

}
