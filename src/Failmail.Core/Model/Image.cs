using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Failmail.Core.Model
{
    public class Image
    {
        public string Id { get; set; }

        public string AttachmentKey { get; set; }

        public IList<string> Tags { get; set; }

        public Image()
        {
            Tags = new List<string>();
        }

        public Image(string attachmentKey) : this()
        {
            Id = "image/" + attachmentKey;
            AttachmentKey = attachmentKey;
        }
    }
}
