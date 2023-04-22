using Microsoft.AspNetCore.Mvc;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Views;
using MyhotelApi.Services;
using MyhotelApi.Services.IServices;

namespace MyhotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService reviewService;

    public ReviewController (ReviewService reviewService)
    {
        this.reviewService = reviewService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> AddReviewAsync(CreateReviewDto createReviewDto)
    {
        var reviewId = await reviewService.AddReviewAsync(createReviewDto);
        return Ok(reviewId);
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetReviewByIdAsync(Guid reviewId)
    {
        var review = await reviewService.GetReviewByIdAsync(reviewId);
        return Ok(review);
    }

    [HttpGet("all")]
    public async ValueTask<IActionResult> GetReviewsAsync(ReviewFilterDto? reviewFilterDto = null)
    {
       var reviews = await reviewService.GetReviewsAsync(reviewFilterDto);
       return Ok(reviews);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateReviewAsync(UpdateReviewDto updateReviewDto)
    {
        var updatedReview = await reviewService.UpdateReviewAsync(updateReviewDto);
        return Ok(updatedReview);
    }

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteReviewAsync(Guid reviewId)
    {
        var deletedReview = await reviewService.DeleteReviewAsync(reviewId);
        return Ok(deletedReview);
    }
}