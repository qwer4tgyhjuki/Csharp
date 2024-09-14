using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Models;

namespace WebApplication2.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _iconfiguration;

        public AuthService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        const int KEY_SIZE = 80;
        const int ITERATION_COUNT = 500000;
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        public void SetUserPasswordHash(UserModel user, string password)
        {
            user.Password = HashPassword(password);
        }

        public string HashPassword(string password)
        {
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: ITERATION_COUNT,
                numBytesRequested: 256 / 8
                ));
            return hashedPassword;
        }

        public bool VerifyPassword(UserModel user, string password)
        {
            string hashPassword = user.Password;
            string inputHashPassword = HashPassword(password);

            return (hashPassword == inputHashPassword);
        }

        public TokenModel GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, username),
                    new Claim(JwtRegisteredClaimNames.Aud, _iconfiguration["Jwt:ValidAudience"]),
                    new Claim(JwtRegisteredClaimNames.Iss, _iconfiguration["Jwt:ValidIssuer"])
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateRefreshToken();
            return new TokenModel { AccessToken = tokenHandler.WriteToken(accessToken), RefreshToken = refreshToken };
        }


        public string GenerateRefreshToken()
        {

            var randomNumber = new byte[80];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }


        public Task<UserModel> RegisterUser(UserModel newUser)
        {
            SetUserPasswordHash(newUser, newUser.Password);
            return Task.FromResult(newUser);
        }
    }
}
