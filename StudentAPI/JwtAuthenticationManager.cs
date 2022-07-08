using Microsoft.IdentityModel.Tokens;
using StudentAPI.Data;
using StudentAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentAPI
{
    public class JwtAuthenticationManager
    {
        private readonly Login login;
        public JwtAuthenticationManager(Login _login)
        {
            login = _login;
        }
        public JwtAuthResponse Authentication(string email, string password)
        {
           
            bool res = login.LoginUser(email, password);
            if(!res)
            {
                return null;
            }
            var tokenExpiryTime = DateTime.Now.AddMinutes(Contants.JWT_TOKEN_MIN);
            var jwtSecurityTokenHandle = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(Contants.JWT_KEY);
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", email),
                   // new Claim(ClaimTypes.PrimaryGroupSid, "User Group 01")
                }),
                Expires = tokenExpiryTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature),
            };
            var securityToken = jwtSecurityTokenHandle.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandle.WriteToken(securityToken);

            return new JwtAuthResponse
            {
                token = token,
                Email = email,
                expires_in = (int)tokenExpiryTime.Subtract(DateTime.Now).TotalSeconds,
            };
        }
    }
}
