using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
              //Tabela
            builder.ToTable("Post");

            //Chave Primaria
            builder.HasKey(x=>x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(); 

            //Propriedades
            builder.Property(x => x.LastUpdateDate) //data de ultima atualizacao
                .IsRequired()  // IS NOT NULL
                .HasColumnName("LastUpdateDate") // DATETME NOT NULL DEFAULT(GETDATE())
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETDATE()"); 
                // .HasDefaultValue(DateTime.Now.ToUniversalTime()); //pega a data no .NET

            // Índices
            builder.HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique(); // indice único

            // Relacionamento N P/ 1
            builder.HasOne(x=>x.Author)
                .WithMany(x=>x.Posts)
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.Cascade); // quando exclui um post, tb exclui o Autor

            builder.HasOne(x=>x.Category)
                .WithMany(x=>x.Posts)
                .HasConstraintName("FK_Post_Category")
                .OnDelete(DeleteBehavior.Cascade);

            //RELACIONAMENTO N P/ N
            builder.HasMany(x=>x.Tags)
                .WithMany(x=>x.Posts)
                //criação identidade virtual baseada num dicionario para relacionar Tags x Posts (3 tabela)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag", //nome da tabela
                    post => post.HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostTag_PostId")
                        .OnDelete(DeleteBehavior.Cascade),
                    tag => tag.HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTag_TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}