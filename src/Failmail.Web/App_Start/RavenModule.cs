using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Failmail.Core.Indexes;
using Failmail.Web.Filters;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Raven.Database.Server;

namespace Failmail.Web.App_Start
{
    public class RavenModule : MvcModule
    {
        public override void Load()
        {
            var store = new DocumentStore
            {
                Url = ConfigurationManager.AppSettings["RAVEN_URL"]
            };
            store.Initialize();
            IndexCreation.CreateIndexes(typeof(TagCloudIndex).Assembly, store);

            Bind<IDocumentStore>()
                .ToConstant(store);

            Bind<IDocumentSession>()
                .ToMethod(ctx => store.OpenSession())
                .InRequestScope();


            this.BindFilter<RavenSessionFilter>(FilterScope.Global, null);
        }
    }
}