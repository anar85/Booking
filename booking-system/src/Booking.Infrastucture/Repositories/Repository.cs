using Booking.Application.Interfaces.Repositories;
using Booking.Domain.Entities.Common;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class Repository<T, TId> : IRepository<T, TId> where T : BaseEntity<TId>
    {
        private readonly BookingDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(BookingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public virtual IQueryable<T> Table => _dbSet;
        public virtual IQueryable<T> TableNoTracking => _dbSet.AsNoTracking();

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = new CancellationToken())
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public  T Add(T entity)
        {
             _dbSet.Add(entity);
             _context.SaveChanges();
            return entity;
        }

        public async Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entity in entities)
                await _dbSet.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = new CancellationToken())
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = new CancellationToken())
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task Commit(CancellationToken cancellationToken = new CancellationToken())
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
