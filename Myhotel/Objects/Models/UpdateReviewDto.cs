using Myhotel.Objects.Enums;

namespace Myhotel.Objects.Models;

public class UpdateReviewDto
{
    public Guid Id { get; set; }
    public string? Comment { get; set; } // Text of the review
    public EReviewStatus? Status { get; set; }
    public decimal? Rating { get; set; } // Rating given by the user (out of 5)
}