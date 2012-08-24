using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Failmail.Web.Services
{
    public interface IImageService
    {
        string GetExtension(Stream imageStream);
        string StoreImage(string imageBucket, string imageExtension, Stream imageStream);
    }
}