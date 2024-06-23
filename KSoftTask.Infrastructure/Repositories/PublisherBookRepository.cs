using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KSoftTask.Infrastructure.Repositories
{
    public class PublisherBookRepository : IBaseRepository<PublisherBook>
    {
        private readonly IApplicationDbContext _db;

        public PublisherBookRepository(IApplicationDbContext db) => _db = db;

        public async Task<List<PublisherBook>> GetAll(CancellationToken cancellationToken = default) => await _db.PublisherBooks
            .AsNoTracking()
            .Include(p => p.Publisher)
            .Include(p => p.Book)
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);

        public async Task<PublisherBook> GetById(int id, CancellationToken cancellationToken = default) => await _db.PublisherBooks
            .AsNoTracking()
            .Include(p => p.Publisher)
            .Include(p => p.Book)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        public async Task<PublisherBook> Create(PublisherBook entity, CancellationToken cancellationToken = default)
        {
            await _db.PublisherBooks.AddAsync(entity, cancellationToken);

            await _db.PublisherBooks.Entry(entity).Reference(a => a.Publisher).LoadAsync(cancellationToken);
            await _db.PublisherBooks.Entry(entity).Reference(a => a.Book).LoadAsync(cancellationToken);

            return entity;
        }

        public async Task<PublisherBook> Update(PublisherBook entity, CancellationToken cancellationToken = default)
        {
            _db.PublisherBooks.Update(entity);

            await _db.PublisherBooks.Entry(entity).Reference(a => a.Publisher).LoadAsync(cancellationToken);
            await _db.PublisherBooks.Entry(entity).Reference(a => a.Book).LoadAsync(cancellationToken);

            return entity;
        }

        public async Task Delete(PublisherBook entity) => _db.PublisherBooks.Remove(entity);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _db.SaveChangesAsync(cancellationToken);
    }
}
