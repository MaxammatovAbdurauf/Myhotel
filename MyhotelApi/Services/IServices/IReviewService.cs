using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;

namespace MyhotelApi.Services.IServices;

public interface IReviewService
{
    ValueTask<Guid> AddReviewAsync(CreateReviewDto createReviewDto);
    ValueTask<ReviewView> GetReviewByIdAsync(Guid reviewId);
    ValueTask<List<ReviewView>> GetReviewsAsync(Guid houseId,ReviewFilterDto? reviewFilterDto = null);
    ValueTask<ReviewView> UpdateReviewAsync(UpdateReviewDto updateReviewDto);
    ValueTask<ReviewView> DeleteReviewAsync(Guid reviewId, bool deleteFromDataBase = false);
}