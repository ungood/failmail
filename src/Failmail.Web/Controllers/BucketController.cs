using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Failmail.Web.Services;
using Failmail.Web.Tasks;
using Failmail.Web.ViewModels;

namespace Failmail.Web.Controllers
{
    public class BucketController : Controller
    {
        private readonly UploadImageTaskFactory uploadImageTaskFactory;
        
        public BucketController(UploadImageTaskFactory uploadImageTaskFactory)
        {
            this.uploadImageTaskFactory = uploadImageTaskFactory;
        }

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
            try
            {
                uploadImageTaskFactory.StartNew(bucket, file);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("file", ex.Message);
                return View(model);
            }
            
            return RedirectToAction("Index");
        }
    }
}