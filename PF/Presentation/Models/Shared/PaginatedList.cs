using Microsoft.EntityFrameworkCore;

namespace PF.Presentation.Models.Shared
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }

        private PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, PaginationParameters parameters)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize).ToListAsync();
            return new PaginatedList<T>(items, count, parameters.PageNumber, parameters.PageSize);
        }
    }
}
