using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Failmail.Web.Services
{
    public interface IImageService
    {
        void StoreImage(string imageBucket, string fileKey, Stream imageStream);
    }
}