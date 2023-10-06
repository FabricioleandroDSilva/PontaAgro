using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ponta.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ponta.Dominio.Enumeradores;

namespace Ponta.Infra.Mapeamento
{
    public class TarefasMap : IEntityTypeConfiguration<Tarefas>
    {
        public void Configure(EntityTypeBuilder<Tarefas> builder)
        {
            builder.ToTable("tarefas", "tarefas");

            builder.HasKey(prop => prop.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd()
                .HasColumnName("id_tarefa");
            builder.HasIndex(prop => new { prop.Id });

            builder.Property(prop => prop.Titulo)
                .IsRequired()
                .HasColumnName("titulo")
                .HasColumnType("varchar(80)");

            builder.Property(prop => prop.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar(200)");

            builder.Property(prop => prop.Data)
                .IsRequired()
                .HasColumnName("data");

            builder.Property(prop => prop.Status)
           .IsRequired()
           .HasColumnName("status")
           .HasColumnType("varchar(2)")
           .HasConversion(
               v => v.RetornarDescricao(), 
               v => ExtensaoEnumerador.RetornarValorDescricao<Status>(v)
           );

            builder.Property(prop => prop.IdUsuario)
                .HasColumnName("id_usuario");

            builder.HasOne(prop => prop.Usuario)
                .WithMany()
                .HasForeignKey(prop => prop.IdUsuario)
                .HasConstraintName("fk_usuario")
                .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
