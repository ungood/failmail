using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Failmail.Web.Models;
using Raven.Json.Linq;

namespace Failmail.Web.Tasks
{
    public class UploadImageTask : BackgroundTask
    {
        private readonly string bucketName;
        private readonly Stream fileStream;

        public UploadImageTask(string bucketName, Stream fileStream)
        {
            this.bucketName = bucketName;
            this.fileStream = fileStream;
        }

        public override void Execute()
        {
            var attachmentKey = Guid.NewGuid().ToString();

            DatabaseCommands.PutAttachment(attachmentKey, null, fileStream, new RavenJObject());

            var image = new Image(bucketName, attachmentKey);
            DocumentSession.Store(image);
        }
    }
}