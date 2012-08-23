using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Failmail.Core.Model;
using ImageResizer.Configuration;
using ImageResizer.Plugins;
using ImageResizer.Util;
using Raven.Client;

namespace Failmail.Core
{
    public class RavenImageResizerPlugin : IPlugin, IVirtualImageProvider, IQuerystringPlugin
    {
        private readonly IDocumentStore store;
        private readonly string route;

        public RavenImageResizerPlugin(IDocumentStore store, string route)
        {
            this.store = store;
            this.route = route;
        }

        public IPlugin Install(Config c)
        {
            c.Plugins.add_plugin(this);
            return this;
        }

        public bool Uninstall(Config c)
        {
            c.Plugins.Uninstall(this);
            return true;
        }

        public IEnumerable<string> GetSupportedQuerystringKeys()
        {
            return new string[] {"tag", "seed"};
        }

        public bool FileExists(string virtualPath, NameValueCollection queryString)
        {
            var bucket = queryString["bucket"];
            var tag = queryString["tag"];
            var seed = queryString["seed"];

            if(string.IsNullOrWhiteSpace(bucket))
                return false;

            if(string.IsNullOrWhiteSpace(tag))
                tag = null;

            if(string.IsNullOrWhiteSpace("seed"))
                seed = Guid.NewGuid().ToString();

            //using(var session = store.OpenSession())
            //{
            //    session.Query<Image>()
            //        .Where(img => img.
            //}
            return false;
        }

        public IVirtualFile GetFile(string virtualPath, NameValueCollection queryString)
        {
            throw new NotImplementedException();
        }

        public class RavenVirtualFile : IVirtualFile
        {
            public Stream Open()
            {
                throw new NotImplementedException();
            }

            public string VirtualPath
            {
                get { throw new NotImplementedException(); }
            }
        }
    }
}
