using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using step_buy_server.data;
using step_buy_server.DTO;
using step_buy_server.models.Personal;

namespace step_buy_server.controller;

[Route("api")]
[ApiController]
public class UserController:ControllerBase
{
    private AppDBConfig _context;

    public UserController(AppDBConfig context)
    {
        this._context = context;
    }
    

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDTO user)
    {
        try
        {
            // Check if user already exists (by Email or Phone)
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email || u.Phone == user.Phone);

            if (existingUser != null)
            {
                return Conflict(new { message = "User with this email or phone number already exists." });
            }

            // Create new user
            User newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
            };

            // Hash password
            string hashedPwd = HashPassword(user.Password);

            AuthentiData auth = new AuthentiData()
            {
                UserId = newUser.Id,
                KeyHash = hashedPwd
            };

            Console.WriteLine($"Registering user\n1. {user.Name}\n2. {user.Email}\n3. {user.Phone}");

            // Add user and authentication data to the database
            _context.Users.Add(newUser);
            _context.AuthentiDatas.Add(auth);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { id = newUser.Id }, newUser);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { message = "Database error occurred.", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
        }
    }

    [HttpPost("login")]
    public Task<ActionResult<User>> LogIn(LoginDTO creds)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email.Equals(creds.Username)
                                 || u.Phone.Equals(creds.Username));
        if (user == null)
        {
            return Task.FromResult<ActionResult<User>>(NotFound(new { message = "User Not Found." }));
        }

        var auth =  _context.AuthentiDatas.FirstOrDefault(a => a.UserId == user.Id);
        if (creds.Password != null && HashPassword(creds.Password) == auth?.KeyHash)
        {
            return Task.FromResult<ActionResult<User>>(user);
        }
        else
        {
            return Task.FromResult<ActionResult<User>>(Unauthorized(new { message = "Invalid credentials." }));
        }
    }
    
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create()) // use bycrupt in nextAuth 
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
    
}