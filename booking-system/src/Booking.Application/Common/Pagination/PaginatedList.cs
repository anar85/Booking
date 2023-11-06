using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Common.Pagination
{
    public class PaginatedList<T>
    {
        private QueryStringParameters _pagingOpt;
        public int CurrentPage { get; private set; }
        public int TotalPages => (TotalCount / PageSize) + 1;
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public List<T> Items { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PaginatedList() { }
        public PaginatedList(QueryStringParameters queryStringParameters) => _pagingOpt = queryStringParameters;

        public async Task CreateAsync(IQueryable<T> source)
        {
            PageSize = _pagingOpt.PageSize;
            CurrentPage = _pagingOpt.PageNumber;
            TotalCount = await source.CountAsync();
            Items = await source.Skip((_pagingOpt.PageNumber - 1) * _pagingOpt.PageSize)
                                .Take(_pagingOpt.PageSize)
                                .ToListAsync();
        }
    }
}
