using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Failmail.Core;
using Raven.Client;

namespace Failmail.Web.Controllers
{
    public class RandomImageController : Controller
    {
        public RandomImageController(ImageRetriever retriever)
        {
            
        }

        public ActionResult Index(string tag, string seed)
        {
            return new HttpStatusCodeResult(301, "HI");
        }
    }
}