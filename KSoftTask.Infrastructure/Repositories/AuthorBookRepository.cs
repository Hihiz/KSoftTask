using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KSoftTask.Infrastructure.Repositories
{
    public class AuthorBookRepository : IBaseRepository<AuthorBook>
    {
        private readonly IApplicationDbContext _db;

        public AuthorBookRepository(IApplicationDbContext db) => _db = db;

        public async Task<List<AuthorBook>> GetAll(CancellationToken cancellationToken = default) => await _db.AuthorBooks
            .AsNoTracking()
            .Include(a => a.Author)
            .Include(a => a.Book)
            .OrderBy(a => a.Id)
            .ToListAsync(cancellationToken);

        public async Task<AuthorBook> GetById(int id, CancellationToken cancellationToken = default) => await _db.AuthorBooks
            .AsNoTracking()
            .Include(a => a.Author)
            .Include(a => a.Book)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public async Task<AuthorBook> Create(AuthorBook entity, CancellationToken cancellationToken = default)
        {
            await _db.AuthorBooks.AddAsync(entity, cancellationToken);

            await _db.AuthorBooks.Entry(entity).Reference(a => a.Author).LoadAsync(cancellationToken);
            await _db.AuthorBooks.Entry(entity).Reference(a => a.Book).LoadAsync(cancellationToken);

            return entity;
        }

        public async Task<AuthorBook> Update(AuthorBook entity, CancellationToken cancellationToken = default)
        {
            _db.AuthorBooks.Update(entity);

            await _db.AuthorBooks.Entry(entity).Reference(a => a.Author).LoadAsync(cancellationToken);
            await _db.AuthorBooks.Entry(entity).Reference(a => a.Book).LoadAsync(cancellationToken);

            return entity;
        }

        public async Task Delete(AuthorBook entity) => _db.AuthorBooks.Remove(entity);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _db.SaveChangesAsync(cancellationToken);
    }
}
