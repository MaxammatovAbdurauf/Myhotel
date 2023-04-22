using Mapster;
using MyhotelApi.Database.ConcreteTypeRepositories;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;
using MyhotelApi.Services.IServices;

namespace MyhotelApi.Services;

[Scoped]
public class ReviewService : IReviewService
{
    private readonly IUnitOfWork unitOfWork;

    public ReviewService (UnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Guid> AddReviewAsync(CreateReviewDto createReviewDto)
    {
        var reviewId = Guid.NewGuid();
        var review   = createReviewDto.Adapt<Review>();
        review.Id    = reviewId;

        await unitOfWork.reviewRepository.AddAsync(review);

        return reviewId;
    }

    public async ValueTask<ReviewView> DeleteReviewAsync(Guid reviewId)
    {
        var review = await unitOfWork.reviewRepository.GetAsync(reviewId);  
        var deletedReview = await unitOfWork.reviewRepository.RemoveAsync(review);

        return deletedReview.Adapt<ReviewView>();
    }

    public async ValueTask<ReviewView> GetReviewByIdAsync(Guid reviewId)
    {
        var review = await unitOfWork.reviewRepository.GetAsync(reviewId);
        return review.Adapt<ReviewView>();
    }

    public async ValueTask<ICollection<ReviewView>> GetReviewsAsync(ReviewFilterDto? reviewFilterDto = null)
    {
        var reviews = await unitOfWork.reviewRepository.GetAllAsync();
        return reviews.Select(r => r.Adapt<ReviewView>()).ToList();
    }

    public async ValueTask<ReviewView> UpdateReviewAsync(UpdateReviewDto updateReviewDto)
    {
        var review = updateReviewDto.Adapt<Review>();
        var updatedReview = await unitOfWork.reviewRepository.UpdateAsync(review);

        return updatedReview.Adapt<ReviewView>();
    }
}