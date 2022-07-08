using Microsoft.AspNetCore.Mvc;
using StudentDataApi.Data;
using StudentDataApi.Logics;
using StudentDataApi.Models;
using System.Net;
using Microsoft.Extensions.Configuration;
namespace StudentDataApi.Controllers
{
 
    public class StudentController : Controller
    {
       
        private readonly HttpHelper httpHelper;
        private IConfiguration configuration { get; }
        ConnectionDbContext db;
        public StudentController(HttpHelper _httpHelper, IConfiguration _configuration, ConnectionDbContext _db)
        {
            httpHelper = _httpHelper;
            configuration = _configuration;
            db = _db;
        }


        public ActionResult Index()
        {
            return View();
        }

        HttpClient httpClient = new HttpClient();
        public List<Student> student { get; set; }
        [HttpGet]
        public ActionResult GetStudent()
        {
            string url = configuration.GetValue<string>("MyApi:DefaultApi");
            httpClient.BaseAddress = new Uri(url);
            var response = httpClient.GetAsync("Values");
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Student>>();
                display.Wait();
                student = display.Result;
            }
            return View(student);
        }
        
        public ActionResult AddStudent()
        {
            return View();
        }


       
        [HttpPost]
        public ActionResult AddStudent(IFormCollection fc)
        {
            string url = configuration.GetValue<string>("MyApi:DefaultApi");
            Student student = new Student();
            student.FullName = fc["FullName"];
            student.Email = fc["Email"];
            student.Address = fc["Address"];
            if(httpHelper.PostData(student, url))
            {
                return RedirectToAction("GetStudent");   
            }
            else
            {
                return View();
            }
        }
        public ActionResult EditStudent()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditStudent(int? id)
        {
            string url = configuration.GetValue<string>("MyApi:DefaultApi");
            List<Student> ss = new List<Student>();
            httpClient.BaseAddress = new Uri(url + id);

            var response = httpClient.GetAsync("Values");
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<Student>>();
                readTask.Wait();
                ss = readTask.Result;
                return View(ss.FirstOrDefault(op => op.Id == id));
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditStudent(IFormCollection fc)
        {
            Student student = new Student();
            student.FullName = fc["FullName"];
            student.Email = fc["Email"];
            student.Address = fc["Address"];
            string url = configuration.GetValue<string>("MyApi:DefaultApi");
            if (httpHelper.PutData(student, $"{url}/{fc["Id"]}"))
            {
                return RedirectToAction("GetStudent");
            }
            else
            {
                return View();
            }
        }
        public ActionResult DeleteStudent()
        {
            return View();
        }


        [HttpGet]
        public ActionResult DeleteStudent(int? id)
        {
            List<Student> ss = new List<Student>();
            string url = configuration.GetValue<string>("MyApi:DefaultApi");
            httpClient.BaseAddress = new Uri(url + id);

            var response = httpClient.GetAsync("Values");
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<Student>>();
                readTask.Wait();
                ss = readTask.Result;
                return View(ss.FirstOrDefault(op => op.Id == id));
            }
            return View();
        }


       [HttpPost]
        public ActionResult DeleteStudent(IFormCollection fc)
        {
            string url = configuration.GetValue<string>("MyApi:DefaultApi");
            if (httpHelper.DeleteData($"{url}/{fc["Id"]}"))
            {
                return RedirectToAction("GetStudent");
            }
            else
            {
                return View();
            }

        }
    }
}
