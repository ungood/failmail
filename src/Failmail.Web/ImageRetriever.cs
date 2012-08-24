using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Failmail.Web.Models;
using Raven.Client;

namespace Failmail.Web
{
    public class ImageRetriever
    {
        private readonly IDocumentStore store;

        public ImageRetriever(IDocumentStore store)
        {
            this.store = store;
        }

        public Stream GetImage(string bucket, string seed)
        {
            using(var session = store.OpenSession())
            {
                if (string.IsNullOrWhiteSpace(seed))
                {
                    var image = GetRandomImage(session, bucket);
                    return GetAttachment(session, image.AttachmentKey);
                }

                var cached = GetCachedImage(session, bucket, seed);
                if(cached != null)
                {
                    return GetAttachment(session, cached.AttachmentKey);
                }

                var randomImage = GetRandomImage(session, bucket);
                SaveCachedImage(session, randomImage, bucket, seed);
                return GetAttachment(session, randomImage.AttachmentKey);
            }
        }

        private CachedImage GetCachedImage(IDocumentSession session, string bucket, string seed)
        {
            var id = CachedImage.CreateId(bucket, seed);
            return session.Load<CachedImage>(id);
        }

        private Image GetRandomImage(IDocumentSession session, string tag)
        {
            return session.Query<Image>()
                .Customize(q => q.RandomOrdering())
                .FirstOrDefault(image => image.Bucket == tag);
        }

        private Stream GetAttachment(IDocumentSession session, string attachmentKey)
        {
            var attachment = session.Advanced.DocumentStore.DatabaseCommands.GetAttachment(attachmentKey);
            return attachment.Data();
        }

        private void SaveCachedImage(IDocumentSession session, Image image, string bucket, string seed)
        {
            var cachedImage = new CachedImage
            {
                Id = CachedImage.CreateId(bucket, seed),
                Bucket = bucket,
                Seed = seed,
                AttachmentKey = image.AttachmentKey
            };
            session.Store(cachedImage);
        }
    }
}
