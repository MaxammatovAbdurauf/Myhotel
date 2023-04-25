using Microsoft.EntityFrameworkCore;
using MyhotelApi.Objects.Options;
using Newtonsoft.Json;

namespace Myhotel.Services;

public static class PagedListService
{
    public static async Task<IEnumerable<T>> ToPagedListAsync<T>(this IQueryable<T> source, PaginationParams? pageParams)
    {
        // to prevent null exception, then CurrentPage and PageSize get default values:
        pageParams ??= new PaginationParams();

        // write pagination parameters (CurrentPage, PageSize, TotalPage) into header:
        HttpContextHelper.AddResponceHeader("X-pagination",
            JsonConvert.SerializeObject(new PaginationMetaData(source.Count(), pageParams.Page, pageParams.Size)));

        var pagedList = await source.Skip(pageParams.Size * (pageParams.Page - 1)).Take(pageParams.Size).ToListAsync();

        return pagedList;
    }

    public static IEnumerable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams? pageParams)
    {
        // to prevent null exception, then CurrentPage and PageSize get default values:
        pageParams ??= new PaginationParams(); // to prevent null exception, then pageParams get default values:

        // write pagination parameters (CurrentPage, PageSize, TotalPage) into header:
        HttpContextHelper.AddResponceHeader("X-pagination",
            JsonConvert.SerializeObject(new PaginationMetaData(source.Count(), pageParams.Page, pageParams.Size)));

        var pagedList = source.Skip(pageParams.Size * (pageParams.Page - 1)).Take(pageParams.Size).ToList();

        return pagedList;
    }
}

public class HttpContextHelper
{
    public static IHttpContextAccessor? httpContextAccessor;

    public static HttpContext Current => httpContextAccessor.HttpContext;

    public static void AddResponceHeader(string key, string value)
    {
        if (Current.Response.Headers.Keys.Contains(key) == true)
        {
            Current.Response.Headers.Remove(key);
        }
        Current.Response.Headers.Add("Access-Control-Expose-Headers", key);
        Current.Response.Headers.Add(key, value);
    }
}