using MyhotelApi.Objects.Enums;

namespace MyhotelApi.Objects.Views;

public class ReviewView
{
    public Guid? HouseId { get; set; } // Id of house which the review is written for
    public Guid? UserId { get; set; } // Id of the user who wrote the review
    public DateTime? createdDate { get; set; }
    public EReviewStatus? Status { get; set; }
    public string? Comment { get; set; } // Text of the review
    public decimal Rating { get; set; } // Rating given by the user (out of 5)
}