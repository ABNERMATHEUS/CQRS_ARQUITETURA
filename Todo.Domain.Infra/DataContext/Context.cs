using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.Entities;

namespace Todo.Domain.Infra.DataContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            :base(options)
        {
        }

        public DbSet<TodoItem> Todos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("Todo");
            modelBuilder.Entity<TodoItem>().Property(x => x.Id);                   ///PARA ALTERAR O TIPO É NECESSÁRIO INSTALAR OUTRO PACOTE - Microsoft.EntityFrameworkCore.Design e Tools e Relacional
            modelBuilder.Entity<TodoItem>().Property(x => x.User).HasMaxLength(120).HasColumnType("varchar(120)"); //se não tiver varchar, ele vai colocar o padrão - nvarchar
            modelBuilder.Entity<TodoItem>().Property(x => x.Title).HasMaxLength(160).HasColumnType("varchar(160)");
            modelBuilder.Entity<TodoItem>().Property(x => x.Done).HasColumnType("bit");
            modelBuilder.Entity<TodoItem>().Property(x => x.Date);  //Quando o tipo é date, ele já coloca datetime por padrão
            modelBuilder.Entity<TodoItem>().HasIndex(b => b.User);            
        }

      
    }
}
