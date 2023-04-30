using Mapster;
using Myhotel.Services;
using MyhotelApi.Database.ConcreteTypeRepositories;
using MyhotelApi.Database.IRepositories;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Helpers.Exceptions;
using MyhotelApi.Objects.Entities;
using MyhotelApi.Objects.Enums;
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

    public async ValueTask<ReviewView> AddReviewAsync(Guid userId, CreateReviewDto createReviewDto)
    {
        var currentHouse = await unitOfWork.houseRepository.GetAsync(createReviewDto.HouseId);
        if (currentHouse is null) throw new NotFoundException<House>();

        var reviewId = Guid.NewGuid();
        var review   = createReviewDto.Adapt<Review>();

        review.Id    = reviewId;
        review.createdDate = DateTime.UtcNow;
        review.Status = EReviewStatus.created;
        review.UserId = userId;

        var newReview = await unitOfWork.reviewRepository.AddAsync(review);

        if (newReview is null) throw new BadRequestException("some error with this review");   
        else
        return newReview.Adapt<ReviewView>();
    }

    public async ValueTask<ReviewView> GetReviewByIdAsync(Guid reviewId)
    {
        var review = await unitOfWork.reviewRepository.GetAsync(reviewId);

        if (review is null) throw new NotFoundException<Review>();
        else  
            return review.Adapt<ReviewView>();
    }

    public async ValueTask<List<ReviewView>> GetReviewsAsync(Guid houseId, ReviewFilterDto? reviewFilterDto = null)
    {
        var query = unitOfWork.reviewRepository.GetAll();
        query = query.Where(re => re.HouseId == houseId);

        if (reviewFilterDto != null)
        {
            if (reviewFilterDto.createdDate != null)
                query = query.Where(re => re.createdDate == reviewFilterDto.createdDate);

            if (reviewFilterDto.Rating == null) 
                query = query.Where(re => re.Rating == reviewFilterDto.Rating);

            if (reviewFilterDto.Status != null)
            {
                query = reviewFilterDto.Status switch
                {
                    EReviewStatus.created => query.Where(re => re.Status == EReviewStatus.created),
                    EReviewStatus.inactive => query.Where(re => re.Status == EReviewStatus.inactive),
                    EReviewStatus.active => query.Where(re => re.Status == EReviewStatus.active),
                    EReviewStatus.deleted => query.Where(re => re.Status == EReviewStatus.deleted),
                };
            }
        }

        var reviews = await query.ToPagedListAsync(reviewFilterDto);

        return reviews.Select(r => r.Adapt<ReviewView>()).ToList();
    }

    public async ValueTask<ReviewView> UpdateReviewAsync(UpdateReviewDto updateReviewDto)
    {
        var review = await unitOfWork.reviewRepository.GetAsync(updateReviewDto.Id);

        if (review is null) throw new NotFoundException<Review>();
        else
        {
            if (updateReviewDto.Comment != null) review.Comment = updateReviewDto.Comment;
            if (updateReviewDto.Rating != null) review.Rating = updateReviewDto.Rating;

            if (updateReviewDto.Status != null &&
                updateReviewDto.Status != EReviewStatus.created &&
                updateReviewDto.Status != EReviewStatus.deleted) review.Comment = updateReviewDto.Comment;

            var updatedReview = await unitOfWork.reviewRepository.UpdateAsync(review);

            return updatedReview!.Adapt<ReviewView>();
        }
    }

    public async ValueTask<ReviewView> DeleteReviewAsync(Guid reviewId, bool deleteFromDataBase = false)
    {
        var review = await unitOfWork.reviewRepository.GetAsync(reviewId);

        if (review is null) throw new NotFoundException<Review>();
        else
        {
            if (deleteFromDataBase)
            {
                var deletedReview = await unitOfWork.reviewRepository.RemoveAsync(review);
                return deletedReview!.Adapt<ReviewView>();
            }
            else
            {
                review.Status = EReviewStatus.deleted;
                var updatedReview = await unitOfWork.reviewRepository.UpdateAsync(review);

                return updatedReview!.Adapt<ReviewView>();
            }
        }  
    }
}