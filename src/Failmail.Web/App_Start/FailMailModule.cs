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
            var store = BindRaven();
            BindAmazon(store);

            this.BindFilter<RavenSessionFilter>(FilterScope.Global, null);
        }

        private IDocumentStore BindRaven()
        {
            var store = new DocumentStore
            {
                ConnectionStringName = ConfigurationManager.AppSettings["RavenConnectionStringName"],
            };
            store.Initialize();
            IndexCreation.CreateIndexes(typeof(BucketCountIndex).Assembly, store);

            TaskExecutor.DocumentStore = store;

            Bind<IDocumentStore>()
                .ToConstant(store);

            Bind<IDocumentSession>()
                .ToMethod(ctx => store.OpenSession())
                .InRequestScope();

            return store;
        }

        private void BindAmazon(IDocumentStore documentStore)
        {
            var accessKey = ConfigurationManager.AppSettings["AmazonAccessKey"];
            var secretKey = ConfigurationManager.AppSettings["AmazonSecretKey"];

            if(string.IsNullOrWhiteSpace(accessKey) || string.IsNullOrWhiteSpace(secretKey))
            {
                var imageService = new RavenImageService(documentStore);
                Bind<IImageService>().ToConstant(imageService);
            }
            else
            {
                var amazonBucketName = ConfigurationManager.AppSettings["AmazonBucketName"];
                var imageService = new AmazonImageService(accessKey, secretKey, amazonBucketName);
                Bind<IImageService>().ToConstant(imageService);
            }
        }
    }
}