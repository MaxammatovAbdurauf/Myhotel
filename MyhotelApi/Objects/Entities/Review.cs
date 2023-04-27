using MyhotelApi.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyhotelApi.Objects.Entities;

public class Review
{
    public Guid Id { get; set; } // Unique identifier for the review
    public Guid? UserId { get; set; } // Id of the user who wrote the review
    public Guid? HouseId { get; set; } // Id of house which the review is written for
    public DateTime? createdDate { get; set; }
    public EReviewStatus? Status { get; set; }
    public string? Comment { get; set; } // Text of the review
    public decimal? Rating { get; set; } // Rating given by the user (out of 5)

    [ForeignKey(nameof(HouseId))]
    public virtual House? House { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }
}