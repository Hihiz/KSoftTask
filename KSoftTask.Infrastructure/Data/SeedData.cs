using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Infrastructure.Data
{
    public static class SeedData
    {
        public static async void Initialize(IApplicationDbContext db)
        {
            if (db.Books.Any())
            {
                return;
            }

            await db.Books.AddRangeAsync(
                new Book
                {
                    Id = 1,
                    Title = "Book1"
                },
                new Book
                {
                    Id = 2,
                    Title = "Book2"
                },
                new Book
                {
                    Id = 3,
                    Title = "Book3"
                });

            if (db.Authors.Any())
            {
                return;
            }

            await db.Authors.AddRangeAsync(
                new Author
                {
                    Id = 1,
                    Name = "Author1"
                },
                new Author
                {
                    Id = 2,
                    Name = "Author2"
                }, new Author
                {
                    Id = 3,
                    Name = "Author3"
                });

            if (db.Publishers.Any())
            {
                return;
            }

            await db.Publishers.AddRangeAsync(
                new Publisher
                {
                    Id = 1,
                    Title = "Publisher1"
                },
                new Publisher
                {
                    Id = 2,
                    Title = "Publisher2"
                },
                new Publisher
                {
                    Id = 3,
                    Title = "Publisher3"
                });

            if (db.AuthorBooks.Any())
            {
                return;
            }

            await db.AuthorBooks.AddRangeAsync(
                new AuthorBook
                {
                    Id = 1,
                    AuthorId = 1,
                    BookId = 1
                },
                new AuthorBook
                {
                    Id = 2,
                    AuthorId = 2,
                    BookId = 1
                },
                new AuthorBook
                {
                    Id = 3,
                    AuthorId = 3,
                    BookId = 2
                });

            if (db.PublisherBooks.Any())
            {
                return;
            }

            await db.PublisherBooks.AddRangeAsync(
                new PublisherBook
                {
                    Id = 1,
                    PublisherId = 1,
                    BookId = 1
                },
                new PublisherBook
                {
                    Id = 2,
                    PublisherId = 1,
                    BookId = 2
                },
                new PublisherBook
                {
                    Id = 3,
                    PublisherId = 3,
                    BookId = 3
                });

            await db.SaveChangesAsync(new CancellationToken());
        }
    }
}
