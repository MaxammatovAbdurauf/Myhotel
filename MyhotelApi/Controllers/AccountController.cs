using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MyhotelApi.Objects.Models;
using MyhotelApi.Objects.Options;
using MyhotelApi.Services;
using MyhotelApi.Services.IServices;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Security.Claims;
using Role = MyhotelApi.Helpers.Exceptions.RoleAttribute;

namespace MyhotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService userService;
    private readonly IEmailService emailService;
    private readonly IDatabase cacheDb;
    private readonly IJwtService jwtService;
    private const int ExpiryCacheTimeInMinutes = 10;

    public AccountController(AccountService userService,
                             EmailService emailService,
                             IConnectionMultiplexer multiplexer,
                             JwtService jwtService)
    {
        this.userService = userService;
        this.emailService = emailService;
        this.jwtService = jwtService;
        cacheDb = multiplexer.GetDatabase();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInUserDto signInUserDto)
    {
        /*var confirmationNumber = await CheckAndSendEmail(signInUserDto);
        var currentUserData = JsonConvert.SerializeObject(signInUserDto);
        await cacheDb.StringSetAsync(confirmationNumber, currentUserData, TimeSpan.FromMinutes(ExpiryCacheTimeInMinutes));
        
        return Ok(confirmationNumber);*/
        var user = await userService.AddUserAsync(signInUserDto);

        var token = jwtService.GenerateToken(user.Id.ToString(), user.Role.ToString());
        return Ok(token);
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveUser(string confirmationNumber)
    {
        if (!cacheDb.KeyExists(confirmationNumber)) return BadRequest("key is incorrect");

        var userCacheData = await cacheDb.StringGetAsync(new RedisKey(confirmationNumber));
        var cachedUser = JsonConvert.DeserializeObject<SignInUserDto>(userCacheData.ToString());
        var user = await userService.AddUserAsync(cachedUser);

        var token = jwtService.GenerateToken(user.Id.ToString(), user.Role.ToString());
        return Ok(token);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpUserDto signUpUserDto)
    {
        var user = await userService.CheckEmailExistAsync(signUpUserDto.Email!);

        if (user == null) return BadRequest("user not found");
        if (user.Password != signUpUserDto.Password) return BadRequest("password is incorrect");

        var token = jwtService.GenerateToken(user.Id.ToString(), user.Role!);
        return Ok(token);
    }

    [HttpGet("all")]
    [Role(RoleType.Admin)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet]
    [Role(RoleType.All)]
    public async Task<IActionResult> GetUserById()
    {
        var userId = CheckTokenData(HttpContext.Request.Headers[HeaderNames.Authorization]).Item1;
        var user = await userService.GetUserByIdAsync(userId);
        return Ok(user);
    }

    [HttpPut]
    [Role(RoleType.All)]
    public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
    {
        var userId = CheckTokenData(HttpContext.Request.Headers[HeaderNames.Authorization]).Item1;
        var updatedUser = await userService.UpdateUserAsync(userId, updateUserDto);
        return Ok(updatedUser);
    }

    [HttpDelete]
    [Role(RoleType.All)]
    public async Task<IActionResult> DeleteUser(Guid? userId = null)
    {
        var userData = CheckTokenData(HttpContext.Request.Headers[HeaderNames.Authorization]);

        if (userData.Item2 == RoleType.Admin || userData.Item2 == RoleType.Creator) await userService.DeleteUserAsync(userId!.Value, fullyDelete: true);
        else await userService.DeleteUserAsync(userData.Item1);

        return Ok();
    }

    private Tuple<Guid, string> CheckTokenData(string token)
    {
        var principal = jwtService.GetPrincipal(token);
        var userId = Guid.Parse(principal!.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var role = principal!.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;
        return new Tuple<Guid, string>(userId, role);
    }

    private async Task<string> CheckAndSendEmail(SignInUserDto signInUserDto)
    {
        var user = await userService.CheckEmailExistAsync(signInUserDto.Email!);
        if (user != null) throw new Exception("this email is registred before");

        var confirmationNumber = new Random().Next(100000, 1000000).ToString();
        var emailReceiver = new string[] { signInUserDto.Email! };
       // emailService.SendEmail(emailReceiver, confirmationNumber);
        return confirmationNumber;
    }
}