using MyhotelApi.Objects.Enums;
using MyhotelApi.Objects.Options;

namespace MyhotelApi.Objects.Models;

public class ReviewFilterDto : PaginationParams
{
    public DateTime? createdDate { get; set; }
    public EReviewStatus? Status { get; set; }
    public decimal? Rating { get; set; } // Rating given by the user (out of 5)
}