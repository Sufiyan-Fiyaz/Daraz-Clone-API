using Daraz_Clone.Data;
using Daraz_Clone.DTOs;
using Daraz_Clone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    //  Register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        string role = request.Role.ToLower();

        if (role != "customer" && role != "seller")
            return BadRequest("Invalid role. Only 'customer' or 'seller' are allowed.");

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
            return BadRequest("Email already exists");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password), //  BCrypt hashing
            Phone = request.Phone,
           
            Role = role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "User registered successfully", user.Id, user.Role });
    }

    //  Login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) //  BCrypt verification
            return Unauthorized("Invalid credentials.");

        return Ok(new UserResponse
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Phone = user.Phone,
       
            Role = user.Role,
            CreatedAt = user.CreatedAt
        });
    }

    //  Get user by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        return Ok(new UserResponse
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Phone = user.Phone,
            
            Role = user.Role,
            CreatedAt = user.CreatedAt
        });
    }
}
