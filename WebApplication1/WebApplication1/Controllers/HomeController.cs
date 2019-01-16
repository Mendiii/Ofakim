using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DL;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net.Http.Headers;
using System.Net;
using System.IO;

namespace Ofakim.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserRegistration()
        {
            return View();
        }

        public ActionResult UsersList()
        {
            string serviceUrl = string.Format("http://localhost:55555/api/values");
            WebRequest request = WebRequest.Create(serviceUrl);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            string setJson = responseFromServer.Replace("\\", "").TrimStart('"').TrimEnd('"');
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(setJson, (typeof(DataTable)));
            return View(dt);
        }

        public ActionResult NoDataBase()
        {
            return View();
        }
    }
}