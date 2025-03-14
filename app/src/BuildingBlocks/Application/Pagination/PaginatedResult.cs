using Microsoft.EntityFrameworkCore;

namespace Application.Pagination;

public class PaginatedResult<TEntity> : List<TEntity>
    where TEntity : class
{
	public PaginatedResult(IEnumerable<TEntity> items, int count, int pageNumber, int pageSize)
	{
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;
        AddRange(items);
    }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public static async Task<PaginatedResult<TEntity>> CreateAsync(IQueryable<TEntity> source, int pageNumber,
    int pageSize, CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PaginatedResult<TEntity>(items, count, pageNumber, pageSize);
    }
}
