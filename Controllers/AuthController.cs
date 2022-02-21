using DAL.Model;
using E_HousingAutomation.Controllers.DBControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_HousingAutomation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Login login = new Login();
        
        private readonly IConfiguration _configuration;
        private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        
        AuthDBController authDbController = new AuthDBController();
        UserDBController userDBController = new UserDBController();


        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create")]
        public bool loginCreate(APIAuthority _login)
        {
            _login.Password = MD5Hash(_login.Password);
            //admin be true if it is registered in the landlord table or user table
            _login.Admin = (authDbController.FindLandlord(0, _login.Email) == null ? false : true) || (userDBController.FindUserByEmail(0, _login.Email) == null ? false : true) ;

            authDbController.CreateLogin(_login);
            return true;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromHeader] LoginDto request)
        {
            APIAuthority tokenLandlord = new APIAuthority();
            tokenLandlord.Email = request.Email;
            tokenLandlord.Password = MD5Hash(request.Password);

            APIAuthority result = authDbController.GetLogin(tokenLandlord);

            if (result != null)
            {
                string token = CreateToken(login);
                Console.WriteLine(token);
                return Ok(token);

            }
            else
            {
                return BadRequest("Kullanıcı yok ya da şifre hatalı.");
            }

        }

        private string CreateToken(Login login)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public string MD5Hash(string _input)
        {

            byte[] dizi = Encoding.UTF8.GetBytes(_input);
            dizi = md5.ComputeHash(dizi);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
    }
}
