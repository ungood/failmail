using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Failmail.Core.Model
{
    public class ImageCache
    {
        // /bucket/{name}
        // /image/{bucket}/[tag]/?seed={seed}

        public string Id { get; set; }

        public string Bucket { get; set; }
        public string Tag { get; set; }
        public string Seed { get; set; }

        public string AttachmentKey { get; set; }

        public static string CreateId(string bucket, string tag, string seed)
        {
            return string.Format("cache/{0}/{1}-{2}", bucket, tag, seed);
        }
    }
}
