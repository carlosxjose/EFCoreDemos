﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace savedchanges_interception_auditing;

public class Program
{
    public static async Task Main()
    {
        await CreateDatabases();

        // Insert, update, and delete some entities

        using (var context = new BlogsContext())
        {
            context.Add(
                new Blog
                {
                    Name = "EF Blog",
                    Posts = { new Post { Title = "EF Core 3.1!" }, new Post { Title = "EF Core 5.0!" } }
                });

            await context.SaveChangesAsync();
        }

        using (var context = new BlogsContext())
        {
            var blog = context.Blogs.Include(e => e.Posts).Single();

            blog.Name = "EF Core Blog";
            context.Remove(blog.Posts.First());
            blog.Posts.Add(new Post { Title = "EF Core 6.0!" });

            context.SaveChanges();
        }

        // Do an insert that will fail

        using (var context = new BlogsContext())
        {
            try
            {
                context.Add(new Post { Id = 3, Title = "EF Core 3.1!" });

                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
            }
        }

        using (var context =
               new AuditContext(
                   @"Server=(localdb)\mssqllocaldb;Database=Demo5.Audit;Trusted_Connection=True;ConnectRetryCount=0"))
        {
            foreach (var audit in context.SaveChangesAudits.Include(e => e.Entities).ToList())
            {
                Console.WriteLine(
                    $"Audit {audit.AuditId} from {audit.StartTime} to {audit.EndTime} was{(audit.Succeeded ? "" : " not")} successful.");

                foreach (var entity in audit.Entities)
                {
                    Console.WriteLine($"  {entity.AuditMessage}");
                }

                if (!audit.Succeeded)
                {
                    Console.WriteLine($"  Error: {audit.ErrorMessage}");
                }
            }
        }
    }

    private static async Task CreateDatabases()
    {
        using (var context =
               new AuditContext(
                   @"Server=(localdb)\mssqllocaldb;Database=Demo5.Audit;Trusted_Connection=True;ConnectRetryCount=0"))
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        using (var context = new BlogsContext())
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}