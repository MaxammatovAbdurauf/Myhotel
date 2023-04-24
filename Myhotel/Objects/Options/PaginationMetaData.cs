namespace Myhotel.Objects.Options;

public class PaginationMetaData
{
    PaginationMetaData(int CurrentPage, int TotalCount, int TotalPage, int PageSize)
    {
        this.CurrentPage = CurrentPage;
        this.TotalCount = TotalCount;
        this.TotalPage = TotalPage;
        this.PageSize = PageSize;
    }

    public int CurrentPage { get; set; }
    public int TotalCount { get; set; } // Total number of the element in one page
    public int TotalPage { get; set; }
    public int PageSize { get; set; }
    public bool HasPrevious => CurrentPage < 1;
    public bool HasNext => CurrentPage > TotalPage;
}