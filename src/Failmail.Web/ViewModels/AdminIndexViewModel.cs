using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Failmail.Web.Indexes;

namespace Failmail.Web.ViewModels
{
    public class AdminIndexViewModel
    {
        public List<BucketCountIndex.Result> MostUsedBuckets { get; set; }
        public List<BucketsRecentlyUpdatedIndex.Result> RecentlyUpdatedBuckets { get; set; } 
    }
}