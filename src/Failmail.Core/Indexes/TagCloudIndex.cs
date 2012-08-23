using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Failmail.Core.Model;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Failmail.Core.Indexes
{
    public class TagCloudIndex : AbstractIndexCreationTask<Image, TagCloud>
    {
        public TagCloudIndex()
        {
            Map = images => from image in images
                from tag in image.Tags
                select new TagCloud
                {
                    Name = tag,
                    Count = 1
                };

            Reduce = results => from result in results
                group result by result.Name
                into g
                select new TagCloud
                {
                    Name = g.Key,
                    Count = g.Sum(x => x.Count)
                };

            Sort(result => result.Count, SortOptions.Int);
        }
    }
}
