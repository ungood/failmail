using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Failmail.Web.Filters;
using Failmail.Web.Indexes;
using Failmail.Web.Tasks;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Failmail.Web.App_Start
{
    public class RavenModule : MvcModule
    {
        public override void Load()
        {
            var store = CreateDocumentStore();

            TaskExecutor.DocumentStore = store;

            Bind<IDocumentStore>()
                .ToConstant(store);

            Bind<IDocumentSession>()
                .ToMethod(ctx => store.OpenSession())
                .InRequestScope();
            
            this.BindFilter<RavenSessionFilter>(FilterScope.Global, null);
        }

        private IDocumentStore CreateDocumentStore()
        {
            var store = new DocumentStore
            {
                ConnectionStringName = ConfigurationManager.AppSettings["RavenConnectionStringName"]
            };
            store.Initialize();
            IndexCreation.CreateIndexes(typeof(BucketCountIndex).Assembly, store);

            return store;
        }
    }
}