using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Ex3.Controllers
{
    public class DefaultController : Controller
    {
        private static MainModel mainModel = new MainModel();

        // GET: Hello
        public ActionResult Index() => View();

        private IModel LocalModel
        {
            set => Session["LocalModel"] = value;
            get => (IModel) Session["LocalModel"];
        }

        [HttpGet]
        public ActionResult Display(string ip, int port, int timePerSec)
        {
            LocalModel = mainModel.AddConnectionModel(ip, port);
            ViewBag.timePerSec = timePerSec;
            return View("Displayer");
        }

        [HttpGet]
        public ActionResult DisplaySaved(string path, int timePerSec)
        {
            LocalModel = mainModel.AddFileModel(path);
            ViewBag.timePerSec = timePerSec;
            return View("Displayer");
        }

        [HttpGet]
        public ActionResult Save(string ip, int port, int timePerSec, int seconds, string path)
        {
            LocalModel = mainModel.AddSaveModel(path, ip, port, timePerSec * seconds);
            ViewBag.timePerSec = timePerSec;
            return View("Displayer");
        }

        [HttpPost]
        public string GetData()
        {
            FlightData data = LocalModel?.GetNextFlightData();

            if (data == null)
            {
                // free memory pointer
                LocalModel = null;
                return "NaN,NaN";
            }

            return $"{data.Lat},{data.Lon}";
        }

    }
}