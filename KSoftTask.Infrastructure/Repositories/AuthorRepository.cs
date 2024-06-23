using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KSoftTask.Infrastructure.Repositories
{
    public class AuthorRepository : IBaseRepository<Author>
    {
        private readonly IApplicationDbContext _db;

        public AuthorRepository(IApplicationDbContext db) => _db = db;

        public async Task<List<Author>> GetAll(CancellationToken cancellationToken = default) => await _db.Authors
            .AsNoTracking()
            .Include(a => a.AuthorBooks).ThenInclude(a => a.Book)
            .OrderBy(a => a.Id)
            .ToListAsync(cancellationToken);

        public async Task<Author> GetById(int id, CancellationToken cancellationToken = default) => await _db.Authors
            .AsNoTracking()
            .Include(a => a.AuthorBooks).ThenInclude(a => a.Book)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        public async Task<Author> Create(Author entity, CancellationToken cancellationToken = default)
        {
            await _db.Authors.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<Author> Update(Author entity, CancellationToken cancellationToken = default)
        {
            _db.Authors.Update(entity);

            return entity;
        }

        public async Task Delete(Author entity) => _db.Authors.Remove(entity);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _db.SaveChangesAsync(cancellationToken);
    }
}
