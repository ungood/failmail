using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Failmail.Web.Indexes;
using Failmail.Web.ViewModels;
using Raven.Client;

namespace Failmail.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDocumentSession session;

        public AdminController(IDocumentSession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            var mostUsedBuckets = session.Query<BucketCountIndex.Result, BucketCountIndex>()
                .OrderByDescending(bucket => bucket.Count)
                .Take(10)
                .ToList();

            var recentlyUpdatedBuckets = session.Query<BucketsRecentlyUpdatedIndex.Result, BucketsRecentlyUpdatedIndex>()
                .OrderByDescending(bucket => bucket.UpdatedDate)
                .Take(10)
                .ToList();

            return View(new AdminIndexViewModel
            {
                MostUsedBuckets = mostUsedBuckets,
                RecentlyUpdatedBuckets = recentlyUpdatedBuckets
            });
        }
    }
}