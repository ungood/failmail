using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Failmail.Web.Models;
using Failmail.Web.Services;
using Raven.Client;

namespace Failmail.Web.Tasks
{
    public class UploadImageTaskFactory
    {
        private readonly IDocumentStore store;
        private readonly IImageService imageService;

        public UploadImageTaskFactory(IDocumentStore store, IImageService imageService)
        {
            this.store = store;
            this.imageService = imageService;
        }

        public void StartNew(string bucketName, HttpPostedFileBase file)
        {
            if(file == null || file.ContentLength < 1)
                throw new Exception("No file was uploaded.");

            var extension = imageService.GetExtension(file.InputStream);
            if(extension == null)
                throw new Exception("The uploaded file is not an image type we support.");
            
            Task.Factory.StartNew(() => Execute(bucketName, extension, file.InputStream))
                .ContinueWith(task => Debug.Write("error occured " + task.Exception.Message), TaskContinuationOptions.OnlyOnFaulted);
        }

        private void Execute(string bucketName, string extension, Stream imageStream)
        {
            var fileKey = imageService.StoreImage(bucketName, extension, imageStream);

            using (var session = store.OpenSession())
            {
                var image = new Image(bucketName, fileKey);
                session.Store(image);
                session.SaveChanges();
            }
        }
    }
}