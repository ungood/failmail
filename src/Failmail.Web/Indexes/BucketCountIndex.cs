using System;
using System.Collections.Generic;
using System.Linq;
using Failmail.Web.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Failmail.Web.Indexes
{
    public class BucketCountIndex : AbstractIndexCreationTask<Image, BucketCountIndex.Result>
    {
        public class Result
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }

        public BucketCountIndex()
        {
            Map = images => from image in images
                select new Result
                {
                    Name = image.Bucket,
                    Count = 1
                };

            Reduce = results => from result in results
                group result by result.Name
                into g
                select new Result
                {
                    Name = g.Key,
                    Count = g.Sum(x => x.Count)
                };

            Sort(result => result.Count, SortOptions.Int);
        }
    }
}
