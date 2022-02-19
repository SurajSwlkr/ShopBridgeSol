using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopBridgeSol.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeSol.Application_start
{
    public class JwtTokanAuth
    {

        private static readonly Lazy<JwtTokanAuth> _lazy =
         new Lazy<JwtTokanAuth>(() => new JwtTokanAuth());
        private JwtTokanAuth()
        {

            Console.WriteLine("Instance created");
        }
        public static JwtTokanAuth Instance
        {
            get
            {
                return _lazy.Value;
            }
        }
        private IConfigurationRoot _config;


        private IConfigurationRoot Config
        {
            get
            {
                if (_config == null)
                    _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                return _config;
            }
        }
        public string GenerateJSONWebToken(Login userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var TokanTime = (1440 - (int)DateTime.Now.TimeOfDay.TotalMinutes);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.mail),
                new Claim("DateOfJoing", userInfo.DateOfJoing.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(TokanTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
     
}
