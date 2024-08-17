using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO.User;
using CompanyApp.Mapper.MapperService;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly CompanyDbContext _dbContext;
    private readonly AppMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserController(CompanyDbContext dbContext, AppMapper mapper, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost("SignUp")]
    public ActionResult Register(RegisterUserDto registerUser) {

        var user = _dbContext.Users.FirstOrDefault(_ => _.Username == registerUser.Username || _.Email == registerUser.Email);
        if (user != null) {
            return BadRequest("User already exists");
        }

        var hasRequiredChars = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*]).{8,}");
        if (!hasRequiredChars.IsMatch(registerUser.Password)) {
            return BadRequest("Password must be at least 8 characters and should contain the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
        registerUser.Password = hashedPassword;

        var newUser = _mapper.Map<RegisterUserDto, User>(registerUser);

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
    }

    [HttpPost("SignIn")]
    public ActionResult Login(LoginUserDto loginUser) {

        var user = _dbContext.Users.FirstOrDefault(_ => _.Username == loginUser.Username);
        if (user == null) {
            return NotFound();
        }
            
        if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password)) {
            return BadRequest("Wrong Password");
        }

        var jwt = GenerateJwtToken(user);
        return Ok(new {jwt});
    }

    [HttpGet("allUsers")]
    public ActionResult<IEnumerable<User>> GetUsers() {

        var users = _dbContext.Users.ToList();

        return Ok(users);
    }

    [HttpGet("getUser/{id}")]
    public ActionResult GetUserById(int id) {

        var user = _dbContext.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecretKey"] ?? String.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}