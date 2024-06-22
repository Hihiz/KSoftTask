using KSoftTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KSoftTask.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Publisher> Publishers { get; set; }
        DbSet<AuthorBook> AuthorBooks { get; set; }
        DbSet<PublisherBook> PublisherBooks { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
