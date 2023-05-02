namespace MyhotelApi.Objects.Options;

public class PaginationMetaData
{
    public PaginationMetaData(int TotalCount, int CurrentPage, int PageSize)
    {
        this.CurrentPage = CurrentPage;
        this.TotalCount = TotalCount;
        this.PageSize = PageSize;
        TotalPage = TotalCount / PageSize;
    }

    public int CurrentPage { get; set; }
    public int TotalCount { get; set; }
    public int TotalPage { get; set; }
    public int PageSize { get; set; }
    public bool HasPrevious => CurrentPage < 1;
    public bool HasNext => CurrentPage > TotalPage;
}