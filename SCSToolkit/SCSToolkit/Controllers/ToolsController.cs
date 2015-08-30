using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCSToolkit.Controllers
{
    public class ToolsController : Controller
    {
        // GET: Tools
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GuidGenerator()
        {
            return View();
        }

        public ActionResult DateCalculator()
        {
            return View();
        }

        public ActionResult WordCount()
        {
            return View();
        }
    }
}