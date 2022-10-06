using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        using var context = new BlogDataContext();

        // context.Users.Add(new User
        // {
        //     Bio = "9x Microsoft MVP",
        //     Email = "andre@balta.io",
        //     Image = "https://balta.io",
        //     Name = "Andre Baltieri",
        //     PasswordHash = "1234",
        //     Slug = "andre-baltieri"
        // });

        var user = context.Users.FirstOrDefault(); //busca o usuario no banco
        
        var post = new Post
        {
            Author = user,
            Body = "Meu artigo",
            Category = new Category
            {
                Name = "BackEnd",
                Slug = "backend"
            },
            CreateDate = System.DateTime.Now,
            Slug = "meu artigo",
            Summary = "neste artigo vamos conferir...",
            Title = "Meu artigo"
        };

        context.Posts.Add(post);
        context.SaveChanges();
    }
}