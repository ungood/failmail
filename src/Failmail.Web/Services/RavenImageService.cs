using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Raven.Client;
using Raven.Json.Linq;

namespace Failmail.Web.Services
{
    public class RavenImageService : IImageService
    {
        private readonly IDocumentStore store;

        public RavenImageService(IDocumentStore store)
        {
            this.store = store;
        }

        public void StoreImage(string imageBucket, string fileKey, Stream imageStream)
        {
            store.DatabaseCommands.PutAttachment(fileKey, null, imageStream, new RavenJObject());
        }
    }
}