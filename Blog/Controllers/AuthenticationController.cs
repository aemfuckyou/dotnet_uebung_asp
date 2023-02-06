using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Controllers;


[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public class AuthenticationRequestBody
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    private class BlogUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public BlogUser(int userId, string username, string firstName, string lastName){
            UserId = userId;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(AuthenticationRequestBody body)
    {
        var user = ValidateUserCredentials(body.UserName, body.Password);

        if(user == null) return Unauthorized();

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
        var signingCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claimsforToken = new List<Claim>();
        claimsforToken.Add(new Claim("sub", user.UserId.ToString()));
        claimsforToken.Add(new Claim("given_name", user.FirstName));
        claimsforToken.Add(new Claim("family_name", user.LastName));

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claimsforToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCred);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Ok(token);
    }

    private BlogUser ValidateUserCredentials(string? userName, string? password)
    {
        return new BlogUser(
            1,
            userName ?? "",
            "Marion",
            "Neuhauser");
    }
}