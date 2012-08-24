using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;

namespace Failmail.Web.Services
{
    public class AmazonImageService : IImageService
    {
        private readonly static Dictionary<ImageFormat, string> imageFormatMap = new Dictionary<ImageFormat, string>
        {
            {ImageFormat.Jpeg, "jpeg"},   
            {ImageFormat.Png, "png"},
            {ImageFormat.Gif, "gif"},
        }; 

        private readonly string amazonBucket;
        private readonly AmazonS3Client client;

        public AmazonImageService(string accessKey, string secretKey, string amazonBucket)
        {
            this.amazonBucket = amazonBucket;
            client = new AmazonS3Client(accessKey, 
                secretKey,
                new AmazonS3Config
                {
                    CommunicationProtocol = Protocol.HTTP
                });
        }

        public string StoreImage(string imageBucket, string imageExtension, Stream imageStream)
        {
            var fileKey = string.Format("{0}/{1}.{2}", imageBucket, Guid.NewGuid(), imageExtension);

            var request = new PutObjectRequest()
                .WithContentType("image/" + imageExtension)
                .WithBucketName(amazonBucket)
                .WithCannedACL(S3CannedACL.PublicRead)
                .WithKey(fileKey);
            request.InputStream = imageStream;

            client.PutObject(request);
            
            return fileKey;
        }

        public string GetExtension(Stream imageStream)
        {
            try
            {
                using(var bitmap = new Bitmap(imageStream))
                {
                    string extension;
                    imageFormatMap.TryGetValue(bitmap.RawFormat, out extension);
                    return extension;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}