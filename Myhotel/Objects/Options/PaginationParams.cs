namespace Myhotel.Objects.Options;

public class PaginationParams
{
    private const int MaxPageSize = 100;
    private const int MinPageNumber = 1;

    private int PageSize = 50;
    private int CurrentPage = 1;

    public int Size
    {
        get
        {
            if (PageSize > MaxPageSize)
            {
                PageSize = MaxPageSize;
            }
            else if (PageSize <= 0)
            {
                PageSize = 1;
            }

            return PageSize;
        }
        set
        {
            PageSize = value;
        }
    }

    public int Page
    {
        get
        {
            if (CurrentPage < MinPageNumber) CurrentPage = MinPageNumber;
            return CurrentPage;
        }
        set
        {
            CurrentPage = value;
        }
    }
}