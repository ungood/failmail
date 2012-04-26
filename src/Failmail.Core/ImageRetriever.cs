using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Failmail.Core.Model;
using Raven.Abstractions.Data;
using Raven.Client;

namespace Failmail.Core
{
    public class ImageRetriever
    {
        private readonly IDocumentStore store;

        public ImageRetriever(IDocumentStore store)
        {
            this.store = store;
        }

        public Stream GetImage(string bucket, string tag, string seed)
        {
            if(string.IsNullOrWhiteSpace(seed))
                return GetRandomImage(bucket, tag);



            using(var session = store.OpenSession())
            {
                var cache = session.Load<ImageCache>("");
                var attachment = session.Advanced.DatabaseCommands.GetAttachment(cache.AttachmentKey);
                attachment.Data();
            }

            return null;
        }

        public Stream GetRandomImage(string bucket, string tag)
        {
            throw new NotImplementedException();
        }
    }
}
