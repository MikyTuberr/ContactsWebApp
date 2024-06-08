using ContactsWebApp.DTO;
using ContactsWebApp.Models;
using ContactsWebApp.Repositories;
using ContactsWebApp.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IAuthRepository _authRepository;

    public AuthController(
        IAuthService authService,
        IAuthRepository authRepository
        )
    {
        _authService = authService;
        _authRepository = authRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        // Validates the incoming registration data
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        // Ensure that required fields are not null
        if(!_authService.IsRegisterModelValid(model)) { return BadRequest("Some data is null."); }

        // Check if user with the same email already exists
        if(await _authRepository.IsEmailUsedAsync(model.Email)) { return BadRequest("Email is already used."); }

        // Create AppUser model
        var newUser = new AppUser()
        {
            Email = model.Email,
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        // Attempt to create a new user
        if (!await _authRepository.CreateUserAsync(newUser, model.Password)) { return StatusCode(500, new { message = "Failed to create user." }); }

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {

        // Check if the incoming model is valid
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        // Ensure that required fields are not null
        if (!_authService.IsLoginModelValid(model)) { return BadRequest("Some data is null."); }

        // Attempt to sign in the user
        var user = await _authRepository.SignInAsync(model.Email, model.Password);
        if (user != null)
        {
            var userRoles = await _authRepository.GetUserRolesAsync(user);
            var token = _authService.GenerateJwtToken(user, userRoles);
            return Ok(new { token });
        }

        // If login fails, return Unauthorized status
        return Unauthorized();
    }
}
