using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Model;

//AppDbContext representa  o Banco de Dados na aplicação
//1-Criar a herança da classe DbContext
//2-Criar os atributos que vão representar as tabelas do Banco de Dados

//Entity Framework: Code First

public class AppDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Ecommerce.db");
    }




}
