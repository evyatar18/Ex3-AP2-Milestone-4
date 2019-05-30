﻿using Ex3.Models;
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

        // GET: Hello
        public ActionResult Index()
        {
            return View("Hello");
        }

        private IModel LocalModel
        {
            set => Session["LocalModel"] = value;
            get => (IModel) Session["LocalModel"];
        }

        [HttpGet]
        public ActionResult display(string ip, int port, int timePerSec)
        {
            LocalModel = mainModel.AddConnectionModel(ip, port);
            ViewBag.timePerSec = timePerSec;
            return View("Displayer");
        }

        //[HttpGet]
        //public ActionResult display(string path, int timePerSec)
        //{
        //    this.localModel = mainModel.AddFileModel(path);
        //    ViewBag.timePerSec = timePerSec;
        //    return View();
        //}

        [HttpGet]
        public ActionResult save(string ip, int port, int timePerSec, int seconds, string path)
        {
            LocalModel = mainModel.AddSaveModel(path, ip, port);
            ViewBag.timePerSec = timePerSec;
            ViewBag.seconds = seconds;
            return View();
        }

        [HttpPost]
        public string GetData()
        {
            FlightData data = LocalModel?.GetNextFlightData();
            return data == null ? "NaN,NaN" : $"{data.Lat},{data.Lon}";
        }

        public ActionResult Hello()
        {
            return View();
        }
    }
}