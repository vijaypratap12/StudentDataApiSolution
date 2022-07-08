using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.Model;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ConnectionDbContext db;
        public ValuesController(ConnectionDbContext _db)
        {
            this.db = _db;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetData()
        {
            return Ok(db.studentDetail.ToList());
        }

        [HttpPost]
        public void AddStudent(Student addstudent)
        {
            
            db.studentDetail.Add(addstudent);
            db.SaveChanges();
    
        }


        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetSingleData([FromRoute] int id)
        {
             var getsinglestudent = db.studentDetail.Find(id);
            return Ok(getsinglestudent);
        }


        [HttpPut]
        [Route("{id:int}")]
        public void UpdateContacts([FromRoute] int id, Student updateContactsRequest)
        {
            var student = db.studentDetail.Find(id);
            if (student != null)
            {
                student.FullName = updateContactsRequest.FullName;
                student.Email = updateContactsRequest.Email;
                student.Address = updateContactsRequest.Address;
                db.SaveChanges();
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void DeleteContacts([FromRoute] int id)
        {
            var student = db.studentDetail.Find(id);
            if (student != null)
            {
                db.studentDetail.Remove(student);
                db.SaveChanges();
            }

        }
    }
}
