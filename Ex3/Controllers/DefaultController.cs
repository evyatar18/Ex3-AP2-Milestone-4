using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Ex3.Controllers
{
    public class DefaultController : Controller
    {
        private static MainModel mainModel = new MainModel();
        private IModel localModel = null;

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult display(string ip, int port, int timePerSec)
        {
            this.localModel = mainModel.AddConnectionModel(ip, port);
            ViewBag.timePerSec = timePerSec;
            return View();
        }

        [HttpGet]
        public ActionResult display(string path, int timePerSec)
        {
            this.localModel = mainModel.AddFileModel(path);
            ViewBag.timePerSec = timePerSec;
            return View();
        }

        [HttpGet]
        public ActionResult save(string ip, int port, int timePerSec, int seconds, string path)
        {
            this.localModel = mainModel.AddSaveModel(path, ip, port);
            ViewBag.timePerSec = timePerSec;
            ViewBag.seconds = seconds;
            return View();
        }

        [HttpPost]
        public string GetData()
        {
            FlightData data = localModel.GetNextFlightData();
            return $"{data.Lat},{data.Lon}";
        }
    }
}