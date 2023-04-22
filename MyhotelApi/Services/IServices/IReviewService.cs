using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;

namespace MyhotelApi.Services.IServices;

public interface IReviewService
{
    ValueTask<Guid> AddReviewAsync(CreateReviewDto createReviewDto);
    ValueTask<ReviewView> GetReviewByIdAsync(Guid reviewId);
    ValueTask<ICollection<ReviewView>> GetReviewsAsync(ReviewFilterDto? reviewFilterDto = null);
    ValueTask<ReviewView> UpdateReviewAsync(UpdateReviewDto updateReviewDto);
    ValueTask<ReviewView> DeleteReviewAsync(Guid reviewId);
}