using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Ponta.Dominio.Entidades;
using Ponta.Infra.Mapeamento;

namespace Ponta.Infra.Contexto
{
    public class PostgrSqlContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tarefas> Tarefa { get; set; }
        public PostgrSqlContext(DbContextOptions<PostgrSqlContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(new UsuarioMap().Configure);
            modelBuilder.Entity<Tarefas>(new TarefasMap().Configure);
        }
    }
}
