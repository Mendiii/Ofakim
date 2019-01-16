using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.DL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            return DbManagement.GetAllUsers();
        }

        // POST api/values
        public void Post(User user)
        {
            try
            {
                DbManagement.InsertNewUser(user);
            }
            catch (Exception)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Occurred"));
            }

        }
    }
}
