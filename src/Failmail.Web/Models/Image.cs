using System;
using System.Collections.Generic;
using System.Linq;

namespace Failmail.Web.Models
{
    public class Image
    {
        public string Id { get; set; }

        public string AttachmentKey { get; set; }

        public string Bucket { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public Image() {}

        public Image(string bucket, string attachmentKey)
        {
            Id = "image/" + attachmentKey;
            Bucket = bucket;
            AttachmentKey = attachmentKey;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
