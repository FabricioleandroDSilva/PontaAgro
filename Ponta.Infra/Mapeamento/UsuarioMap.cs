using Ponta.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Ponta.Infra.Mapeamento
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
			builder.ToTable("usuario", "usuario");

			builder.HasKey(prop => prop.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd()
				.HasColumnName("id_usuario");
			builder.HasIndex(prop => new { prop.Id });

			builder.Property(prop => prop.Nome)
				.IsRequired()
				.HasColumnName("nm_usuario")
				.HasColumnType("varchar(100)");

			builder.Property(prop => prop.Login)
				.IsRequired()
				.HasColumnName("nm_login")
				.HasColumnType("varchar(25)");

			builder.Property(prop => prop.Senha)
				.IsRequired()
				.HasColumnName("nm_senha")
				.HasColumnType("varchar(30)");

		}
    }
}
