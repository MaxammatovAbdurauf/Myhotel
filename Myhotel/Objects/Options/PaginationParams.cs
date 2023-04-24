namespace Myhotel.Objects.Options;

public class PaginationParams
{
    private const int MaxPageSize = 100;
    private const int MinPageNumber = 1;

    private int PageSize = 24;
    private int PageNumber = 1;

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
            if (PageNumber < MinPageNumber) PageNumber = MinPageNumber;
            return PageNumber;
        }
        set
        {
            PageNumber = value;
        }
    }
}