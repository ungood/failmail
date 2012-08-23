using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Failmail.Core.Model;
using Failmail.Web.Models;
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

        public ActionResult Upload()
        {
            return View(new UploadModel());
        }

        [HttpPost]
        public ActionResult Upload(UploadModel model, HttpPostedFileBase file)
        {
            if(file == null || file.ContentLength < 1)
            {
                ModelState.AddModelError("File", "You must upload an image file.");
                return View(model);
            }

            var attachmentKey = Guid.NewGuid().ToString();

            session.Advanced.DatabaseCommands.PutAttachment(attachmentKey, null, file.InputStream, new RavenJObject());
            
            var image = new Image(attachmentKey);
            foreach(var tag in model.Tags.Split(',', ' '))
            {
                image.Tags.Add(tag.Trim());
            }
            session.Store(image);
            
            return View(model);
        }

    }
}
