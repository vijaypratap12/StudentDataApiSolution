using Microsoft.AspNetCore.Mvc;
using StudentDataApi.Logics;
using StudentDataApi.Models;

namespace StudentDataApi.Controllers
{
    public class UserController : Controller
    {
        private readonly UserHelper userHelper;
        public UserController(UserHelper _userHelper)
        {
            userHelper = _userHelper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(IFormCollection fc)
        {
            SignupProperties signup = new SignupProperties();
            signup.FullName = fc["FullName"];
            signup.Email = fc["Email"];
            signup.Password = fc["Password"];
            signup.ConfirmPassword = fc["ConfirmPassword"];
            if(userHelper.RegisterUser(signup, "https://localhost:7174/api/User/registration"))
             return RedirectToAction("Login");
            else
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(IFormCollection fc)
        {
            LoginProperties loginProperties = new LoginProperties();
            loginProperties.Email = fc["Email"];
            loginProperties.Password = fc["Password"];
            if (userHelper.LoginUser(loginProperties, "https://localhost:7174/api/User/Login"))
            {
                return RedirectToAction("GetStudent", "Student");
            }
            return View();
        }
    }
}
