using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SecureAPI.Models;

namespace SecureAPI.Controllers
{
    public class HomeController : Controller
    {

        public List<MyData> userData = new List<MyData>()
        {

            new MyData {Id = 120, Name = "Linda", Username = "Linda", Email = "linda@gmail.com", Phone = "9853578989", Website="https://linda.com"},

             new MyData {Id = 121, Name = "Rob", Username = "Rob", Email = "rob@gmail.com", Phone = "9853985989", Website="https://rob.com"},

             new MyData {Id = 122, Name = "John", Username = "Linda", Email = "john@gmail.com", Phone = "41853578989", Website="https://john.com"},

             new MyData {Id = 123, Name = "Mathew", Username = "Mathew", Email = "mathew@gmail.com", Phone = "6473578989", Website="https://mathew.com"},

             new MyData {Id = 124, Name = "Margarette", Username = "Margarette", Email = "margerette@gmail.com", Phone = "6433578989", Website="https://margerette.com"},

             new MyData {Id = 125, Name = "Mark", Username = "Mark", Email = "mark@gmail.com", Phone = "7553578989", Website="https://mark.com"},

             new MyData {Id = 126, Name = "Benjamin", Username = "Benjamin", Email = "benjamin@gmail.com", Phone = "7653578989", Website="https://benjamin.com"},

             new MyData {Id = 127, Name = "Amanda", Username = "Linda", Email = "amanda@gmail.com", Phone = "4535780978", Website="https://amanda.com"},

             new MyData {Id = 128, Name = "Elizibeth", Username = "Elizibeth", Email = "elizibeth@gmail.com", Phone = "9853532189", Website="https://elizibethlinda.com"},
        };

        public IActionResult Index()
        {
            return View();
        }

        // GET api/userData
        [Authorize]
        [HttpGet]
        [Route("api/userData")]
        public IEnumerable<MyData> Get()
        {
            return userData;
        }

        // GET api/userData/name
        [HttpGet]
        [Route("api/userData/{id}")]
        public MyData Get(int id)
        {

            return userData.Find(x => x.Id == id);
        }



        public IActionResult Authenticate()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"bala"),
                new Claim("granny", "cookie")
            };

            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);

            var key = new SymmetricSecurityKey(secretBytes);

            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(900),
                signingCredentials
                ); 

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { access_token = tokenJson });
        }
    }
}
