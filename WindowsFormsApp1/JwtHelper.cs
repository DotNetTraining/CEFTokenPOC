using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace WindowsFormsApp1
{
    
    public static class JwtHelper
    {
        private static readonly string SecretKey = "your_super_secret_key_which_is_long";
        public static string GenerateToken(string username)
        {
            Console.WriteLine($"Generating Token for: {username}");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "App",
                audience: "App",
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine($"Generated Token: {jwt}");
            return jwt;
        }
    }

}
