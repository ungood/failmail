using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Failmail.Web.Models;
using Failmail.Web.ViewModels;
using Raven.Client;
using Raven.Json.Linq;

namespace Failmail.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IDocumentSession session;

        public ImageController(IDocumentSession session)
        {
            this.session = session;
        }

        

    }
}
