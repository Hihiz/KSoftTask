using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KSoftTask.Infrastructure.Repositories
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly IApplicationDbContext _db;

        public BookRepository(IApplicationDbContext db) => _db = db;

        public async Task<List<Book>> GetAll(CancellationToken cancellationToken = default) => await _db.Books
            .AsNoTracking()
            .Include(b => b.AuthorBooks).ThenInclude(b => b.Author)
            .Include(b => b.PublisherBooks).ThenInclude(b => b.Publisher)
            .OrderBy(b => b.Id)
            .ToListAsync(cancellationToken);

        public async Task<Book> GetById(int id, CancellationToken cancellationToken = default) => await _db.Books
            .AsNoTracking()
            .Include(b => b.AuthorBooks).ThenInclude(b => b.Author)
            .Include(b => b.PublisherBooks).ThenInclude(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        public async Task<Book> Create(Book entity, CancellationToken cancellationToken = default)
        {
            await _db.Books.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<Book> Update(Book entity, CancellationToken cancellationToken = default)
        {
            _db.Books.Update(entity);

            return entity;
        }

        public async Task Delete(Book entity) => _db.Books.Remove(entity);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _db.SaveChangesAsync(cancellationToken);
    }
}
