using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Raven.Client;

namespace Failmail.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentSession session;

        public HomeController(IDocumentSession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
