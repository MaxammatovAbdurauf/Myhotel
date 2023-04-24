using Myhotel.Objects.Options;
using Myhotel.Services.IServices;

namespace Myhotel.Services;

public static class PagedListService
{
    public static async Task<IEnumerable<T>> ToPagedListAsync<T> (this IQueryable<T> source, PaginationParams? paginationParams)
    {
        paginationParams ??= new PaginationParams();
        HttpContextHelper.AddResponceHeader();
        return;
    }
}