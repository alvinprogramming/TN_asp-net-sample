using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sample.Business.IServices.Admin;
using Sample.Controllers.Admin;
using Sample.Data.DTO;
using Sample.Data.DTO.Admin;
using Sample.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;


        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;

            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO credentials)
        {
            #region edits



            //var response = _userService.Search(credentials.Username);

            //var data = _dbContext.UserInfos.Where(x => x.EmailAddress == credentials.Username).Select(x => new UserDTO(x)).ToList();

            //var data = await _userService.PreLogin(credentials.Username);

            //data[0].User

            //what if in database, 
            // how to get email, where only using firstname and password
            // 

            //UserDTO updateuserdto = new UserDTO()
            //{
            //    Id = data[0].Id,
            //    FirstName = data[0].FirstName,
            //    MiddleName = data[0].MiddleName,
            //    LastName = data[0].LastName,
            //    Age = data[0].Age,
            //    EmailAddress = data[0].EmailAddress,
            //    Address = data[0].Address,
            //    CreatedBy = data[0].CreatedBy,
            //    CreatedDate = data[0].CreatedDate,
            //    IsEnabled = data[0].IsEnabled
            //};

            //if (data.Username == null)
            //{

            //    data.Username = credentials.Username;

            //    //_dbContext.SaveChanges();

            //    UserDTO datacast = data[0];

            //    _dbContext.Entry(datacast).State = EntityState.Modified;


            //    bool hasChanges = _dbContext.ChangeTracker.HasChanges(); // should be true
            //    int updates = _dbContext.SaveChanges();                  // should be > 0

            //    //var updateuser = _dbContext.UserInfos.Sin https://learn.microsoft.com/en-us/ef/core/saving/basic

            //    // todo: figure out update of data using ef6
            //}

            //if (data[0].Password == "")
            //{

            //}


            //if (data.Count == 0)
            //{
            //    return Unauthorized();
            //}

            //if(data[0].EmailAddress != credentials.Username || data[0].LastName != credentials.Password)
            //{
            //    return Unauthorized();
            //}

            //return Ok(data);

            //data[0].FirstName = credentials.Username;
            //data[0].LastName = credentials.Password;



            //var sample = new LoginDTO()
            //{
            //    Username = credentials.Username,
            //    Password = credentials.Password,
            //};

            ////Validate credentials here(this is just an example)
            //if (credentials.Username != "admin" || credentials.Password != "password")
            //{
            //    return Unauthorized();
            //} //remove this, insert 


            //Console.WriteLine(credentials);
            //Console.WriteLine(sample);


            #endregion

            var data = await _userService.PreLogin(credentials.Username, credentials.Password);

            if (data == null) 
            {
                return Unauthorized();
            }

            //Get JWT settings from appsettings.json
            var jwtSettings = _configuration.GetSection("Jwt"); // gets value from settings.json

            // Create JWT token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, credentials.Username),
                new Claim(JwtRegisteredClaimNames.Jti, data.Id.ToString()) // change into guid ni username
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])); //gets secret key, jwt, from settings.json
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims, // geths who logged in
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpirationInMinutes"])),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}
