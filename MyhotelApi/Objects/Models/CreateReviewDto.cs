namespace MyhotelApi.Objects.Models;

public class CreateReviewDto
{
    public Guid? HouseId { get; set; } // Id of house which the review is written for
    public string? Comment { get; set; } // Text of the review
    public decimal? Rating { get; set; } // Rating given by the user (out of 5)
}