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

        public Stream GetImage(string tag, string seed)
        {
            using(var session = store.OpenSession())
            {
                if (string.IsNullOrWhiteSpace(seed))
                {
                    var image = GetRandomImage(session, tag);
                    return GetAttachment(session, image.AttachmentKey);
                }

                var cached = GetCachedImage(session, tag, seed);
                if(cached != null)
                {
                    return GetAttachment(session, cached.AttachmentKey);
                }

                var randomImage = GetRandomImage(session, tag);
                SaveCachedImage(session, randomImage, tag, seed);
                return GetAttachment(session, randomImage.AttachmentKey);
            }
        }

        private CachedImage GetCachedImage(IDocumentSession session, string tag, string seed)
        {
            var id = CachedImage.CreateId(tag, seed);
            return session.Load<CachedImage>(id);
        }

        private Image GetRandomImage(IDocumentSession session, string tag)
        {
            return session.Query<Image>()
                .Customize(q => q.RandomOrdering())
                .FirstOrDefault(image => image.Tags.Contains(tag));
        }

        private Stream GetAttachment(IDocumentSession session, string attachmentKey)
        {
            var attachment = session.Advanced.DatabaseCommands.GetAttachment(attachmentKey);
            return attachment.Data();
        }

        private void SaveCachedImage(IDocumentSession session, Image image, string tag, string seed)
        {
            var cachedImage = new CachedImage
            {
                Id = CachedImage.CreateId(tag, seed),
                Tag = tag,
                Seed = seed,
                AttachmentKey = image.AttachmentKey
            };
            session.Store(cachedImage);
        }
    }
}
