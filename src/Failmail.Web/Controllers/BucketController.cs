using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Failmail.Web.Tasks;
using Failmail.Web.ViewModels;

namespace Failmail.Web.Controllers
{
    public class BucketController : Controller
    {
        public ActionResult Index(string bucket)
        {
            return View(new BucketViewModel
            {
                BucketName = bucket
            });
        }

        public ActionResult Upload(string bucket)
        {
            return View(new BucketViewModel
            {
                BucketName = bucket
            });
        }

        [HttpPost]
        public ActionResult Upload(string bucket, BucketViewModel model, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength < 1)
            {
                ModelState.AddModelError("File", "You must upload an image file.");
                return View(model);
            }

            var task = new UploadImageTask(bucket, file.InputStream);
            TaskExecutor.ExecuteTask(task);

            return RedirectToAction("Index");
        }
    }
}