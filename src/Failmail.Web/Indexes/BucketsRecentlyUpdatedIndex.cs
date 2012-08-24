using System;
using System.Collections.Generic;
using System.Linq;
using Failmail.Web.Models;
using Raven.Client.Indexes;

namespace Failmail.Web.Indexes
{
    public class BucketsRecentlyUpdatedIndex : AbstractIndexCreationTask<Image, BucketsRecentlyUpdatedIndex.Result>
    {
        public class Result
        {
            public string Name { get; set; }
            public DateTimeOffset UpdatedDate { get; set; }
        }


        public BucketsRecentlyUpdatedIndex()
        {
            Map = images => from image in images
                select new Result
                {
                    Name = image.Bucket,
                    UpdatedDate = image.CreatedDate
                };

            Reduce = results => from result in results
                group result by result.Name
                into g
                select new Result
                {
                    Name = g.Key,
                    UpdatedDate = g.Max(x => (DateTimeOffset)x.UpdatedDate)
                };
        }
    }
}