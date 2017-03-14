using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TrainingCompany.Controllers
{
    public class CoursesController : ApiController
    {
        [HttpGet]
        public IEnumerable<course> AllCourses()
        {
            return courses;
        }
        [HttpGet]
        public HttpResponseMessage Get(int id) {
            HttpResponseMessage msg = null;
            var ret = (from c in courses
                       where c.id == id
                       select c
                        ).FirstOrDefault();
            if (ret == null)
            {

                msg = Request.CreateErrorResponse(HttpStatusCode.Found, "Course not found");
            }
            else {
                msg = Request.CreateResponse<course>(HttpStatusCode.OK, ret);
            }
            return msg;
        }

        public HttpResponseMessage Post([FromBody]course c) {
            c.id = courses.Count();
            courses.Add(c);
            var msg = Request.CreateResponse(HttpStatusCode.Created);
            msg.Headers.Location = new Uri(Request.RequestUri + c.id.ToString());
            return msg;
        }

        public void Put(int id, [FromBody]course c)
        {
            var ret = (from cc in courses
                       where cc.id == id
                       select cc
                         ).FirstOrDefault();
            ret.title = c.title;
        }

        public void Delete(int id) {
            var del = (from c in courses
                       where c.id == id
                       select c).FirstOrDefault();

            courses.Remove(del);
                    
        }
        static List<course> courses = InitCourse();
        private static List<course> InitCourse()
        {
            var ret = new List<course>();
            ret.Add(new course { id=0, title ="course 1"});
            ret.Add(new course { id = 1, title = "course 2"});

            return ret;
        }
    }
    public class course
    {
        public int id;
        public string title;


    }

}
