using System;
using System.Collections.Generic;
using System.Linq;

namespace Failmail.Web.Models
{
    public class CachedImage
    {
        public string Id { get; set; }
        
        public string Bucket { get; set; }
        
        public string Seed { get; set; }
        
        public string AttachmentKey { get; set; }

        public static string CreateId(string tag, string seed)
        {
            return string.Format("cache/{0}/{1}-{2}", tag, seed);
        }
    }
}
