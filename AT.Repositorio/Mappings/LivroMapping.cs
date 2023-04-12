

using AT.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AT.Repositorio.Mappings
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Titulo).IsRequired();
            builder.Property(x => x.Isbn).IsRequired();
            builder.Property(x => x.Ano).IsRequired();

            builder.HasMany(x => x.Autores).WithMany(x => x.Livros);
            
        }
    }
}
