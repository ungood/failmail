using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;

namespace Failmail.Web.Services
{
    public class AmazonImageService : IImageService
    {
        private readonly string amazonBucket;
        private readonly AmazonS3Client client;

        public AmazonImageService(string accessKey, string secretKey, string amazonBucket)
        {
            this.amazonBucket = amazonBucket;
            client = new AmazonS3Client(accessKey, secretKey);
        }

        public void StoreImage(string imageBucket, string fileKey, Stream imageStream)
        {
            client.PutObject(new PutObjectRequest
            {
                BucketName = amazonBucket,
                FilePath = imageBucket + "/" + fileKey,
                AutoCloseStream = true,
                InputStream = imageStream
            });
        }
    }
}