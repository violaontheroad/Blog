using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
             //Tabela
            builder.ToTable("Category");

            //Chave Primaria
            builder.HasKey(x=>x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(); 

            //Propriedades
            builder.Property(x=>x.Name)
                .IsRequired()  // IS NOT NULL
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x=>x.Slug)
                .IsRequired()  // IS NOT NULL
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            // Índices
            builder.HasIndex(x => x.Slug, "IX_Category_Slug")
                .IsUnique(); // indice único


        }
    }
}