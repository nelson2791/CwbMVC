using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CwbMVC.Models;

namespace CwbMVC.Controllers
{
    public class WeatherForecastsController : Controller
    {
        private CWBsysEntities1 db = new CWBsysEntities1();

        // GET: WeatherForecasts
        public ActionResult Index()
        {
            return View(db.WeatherForecast.ToList());
        }

        // GET: WeatherForecasts/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeatherForecast weatherForecast = db.WeatherForecast.Find(id);
            if (weatherForecast == null)
            {
                return HttpNotFound();
            }
            return View(weatherForecast);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
