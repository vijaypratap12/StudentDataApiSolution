using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.Model;

namespace StudentAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ConnectionDbContext db;
          private readonly Login login;
        private readonly JwtAuthenticationManager jwtAuthentication;
        public UserController(ConnectionDbContext _db, Login login, JwtAuthenticationManager _jwtAuthentication)
        {
            db = _db;
            this.login = login;
            this.jwtAuthentication = _jwtAuthentication;
        }



        [HttpPost]
        [Route("registration")]
        public void SignupDetails(SignupProperties signupProperties)
        {
            db.signupDetails.Add(signupProperties);
            db.SaveChanges();
           
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult LoginDetails(LoginProperties loginProperties)
        {
            //Result result = new Result();
            //if (login.LoginUser(loginProperties.Email, loginProperties.Password))
            //{
            //    result.result = true;
            //    result.message = "Login Success";
            //}
            //else
            //{
            //    result.result = false;
            //    result.message = "Login Failed";
            //}

            //return result;

           // var jwtAuthenticationManager = new JwtAuthenticationManager();
            var authResult = jwtAuthentication.Authentication(loginProperties.Email, loginProperties.Password);
            if (authResult == null)
            {
                return Unauthorized();
            }
            else
                return Ok(authResult);
        }
    }
}
