using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KSoftTask.Infrastructure.Repositories
{
    public class PublisherRepository : IBaseRepository<Publisher>
    {
        private readonly IApplicationDbContext _db;

        public PublisherRepository(IApplicationDbContext db) => _db = db;

        public async Task<List<Publisher>> GetAll(CancellationToken cancellationToken = default) => await _db.Publishers
            .AsNoTracking()
            .Include(p => p.PublisherBooks).ThenInclude(p => p.Book)
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);

        public async Task<Publisher> GetById(int id, CancellationToken cancellationToken = default) => await _db.Publishers
            .AsNoTracking()
            .Include(p => p.PublisherBooks).ThenInclude(p => p.Book)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<Publisher> Create(Publisher entity, CancellationToken cancellationToken = default)
        {
            await _db.Publishers.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<Publisher> Update(Publisher entity, CancellationToken cancellationToken = default)
        {
            _db.Publishers.Update(entity);

            return entity;
        }

        public async Task Delete(Publisher entity) => _db.Publishers.Remove(entity);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _db.SaveChangesAsync(cancellationToken);
    }
}
