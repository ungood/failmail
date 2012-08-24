using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Failmail.Web.Filters;
using Failmail.Web.Indexes;
using Failmail.Web.Services;
using Failmail.Web.Tasks;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Failmail.Web.App_Start
{
    public class FailMailModule : MvcModule
    {
        public override void Load()
        {
            BindRaven();
            BindAmazon();

            this.BindFilter<RavenSessionFilter>(FilterScope.Global, null);
        }

        private void BindRaven()
        {
            var store = new DocumentStore
            {
                Url = ConfigurationManager.AppSettings["Raven/Url"],
                ApiKey = ConfigurationManager.AppSettings["Raven/ApiKey"]
            };
            store.Initialize();
            IndexCreation.CreateIndexes(typeof(BucketCountIndex).Assembly, store);

            TaskExecutor.DocumentStore = store;

            Bind<IDocumentStore>()
                .ToConstant(store);

            Bind<IDocumentSession>()
                .ToMethod(ctx => store.OpenSession())
                .InRequestScope();
        }

        private void BindAmazon()
        {
            var accessKey = ConfigurationManager.AppSettings["Amazon/AccessKey"];
            var secretKey = ConfigurationManager.AppSettings["Amazon/SecretKey"];
            var bucketName = ConfigurationManager.AppSettings["Amazon/S3BucketName"];

            var imageService = new AmazonImageService(accessKey, secretKey, bucketName);
            Bind<IImageService>().ToConstant(imageService);
        }
    }
}