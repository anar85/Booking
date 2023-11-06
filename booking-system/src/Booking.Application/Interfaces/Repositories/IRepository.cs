namespace Booking.Application.Interfaces.Repositories
{
    public interface IRepository<T, TId> where T : class
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = new CancellationToken());
        T Add(T entity);
        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = new CancellationToken());
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = new CancellationToken());
        Task DeleteAsync(T entity, CancellationToken cancellationToken = new CancellationToken());
        Task Commit(CancellationToken cancellationToken = new CancellationToken());
    }
}
