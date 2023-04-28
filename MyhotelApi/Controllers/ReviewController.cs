using Microsoft.AspNetCore.Mvc;
using MyhotelApi.Helpers.Exceptions;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Options;
using MyhotelApi.Services;
using MyhotelApi.Services.IServices;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace MyhotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Role(RoleType.All)]
public class ReviewController : ControllerBase
{
    private readonly IReviewService reviewService;
    private readonly IJwtService jwtService;

    public ReviewController(ReviewService reviewService, IJwtService jwtService)
    {
        this.reviewService = reviewService;
        this.jwtService = jwtService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> AddReviewAsync(CreateReviewDto createReviewDto)
    {
        var userId = (await CheckTokenData(HttpContext.Request.Headers.Authorization)).Item1;
        createReviewDto.UserId = userId;

        var newReview = await reviewService.AddReviewAsync(createReviewDto);

        return Ok(newReview);
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetReviewByIdAsync(Guid reviewId)
    {
        var review = await reviewService.GetReviewByIdAsync(reviewId);
        return Ok(review);
    }

    [HttpGet("all")]
    public async ValueTask<IActionResult> GetReviewsAsync(Guid houseId, [FromQuery] ReviewFilterDto? reviewFilterDto = null)
    {
        var reviews = await reviewService.GetReviewsAsync(houseId, reviewFilterDto);
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

    private async Task<Tuple<Guid, string>> CheckTokenData(string token, Guid? reviewId = null)
    {
        var principal = jwtService.GetPrincipal(token);
        var userId = Guid.Parse(principal!.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var role = principal!.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;

        if (reviewId != null)
        {
            var review = await reviewService.GetReviewByIdAsync(reviewId!.Value);

            if (review.UserId != userId)
            {
                throw new BadRequestException("you have no access");
            }
        }

        return new Tuple<Guid, string>(userId, role);
    }
}