using _3amStoreAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _3amStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration configuration;
        public AuthController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            this.configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserLoginModel userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = GenerateToken(user);
                var userToken = new UserModel
                {
                    user_id = user.user_id,
                    email = user.email,
                    fullname = user.fullname,
                    address = user.address,
                    phone_number = user.phone_number,
                    role = user.role

                };
                return Ok(new { token, userToken });
            }

            return NotFound("user not found");
        }

        private UserModel Authenticate(UserLoginModel userLogin)
        {
            var listUser = _databaseContext.Users.ToList();
            if (listUser != null && listUser.Count > 0)
            {
                var currentUser = listUser.FirstOrDefault(u => u.email.ToLower() == userLogin.email && u.password == userLogin.password);
                return currentUser;
            }
            return null;
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials
                (securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Fullname",user.fullname),
                new Claim("Email",user.email),
                new Claim(ClaimTypes.Role,user.role)
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
