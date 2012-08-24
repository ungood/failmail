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
            AppSettings.FillSecrets();
            BindRaven();
            BindAmazon();

            this.BindFilter<RavenSessionFilter>(FilterScope.Global, null);
        }

        private void BindRaven()
        {
            var store = new DocumentStore
            {
                Url = AppSettings.Get("Raven/Url"),
                ApiKey = AppSettings.Get("Raven/ApiKey"),
                DefaultDatabase = AppSettings.Get("Raven/Database")
            };
            store.Initialize();
            IndexCreation.CreateIndexes(typeof(BucketCountIndex).Assembly, store);

            Bind<IDocumentStore>()
                .ToConstant(store);

            Bind<IDocumentSession>()
                .ToMethod(ctx => store.OpenSession())
                .InRequestScope();
        }

        private void BindAmazon()
        {
            var accessKey = AppSettings.Get("Amazon/AccessKey");
            var secretKey = AppSettings.Get("Amazon/SecretKey");
            var bucketName = AppSettings.Get("Amazon/S3Bucket");

            var imageService = new AmazonImageService(accessKey, secretKey, bucketName);
            Bind<IImageService>().ToConstant(imageService);
        }
    }
}