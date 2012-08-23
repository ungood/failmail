using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Failmail.Core.Indexes;
using Failmail.Core.Model;
using Failmail.Web.Models;
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
            var tagClouds = session.Query<TagCloud, TagCloudIndex>()
                .OrderByDescending(tag => tag.Count)
                .Take(64)
                .ToList();

            return View(new HomeModel
            {
                TagClouds = tagClouds
            });
        }
    }
}
