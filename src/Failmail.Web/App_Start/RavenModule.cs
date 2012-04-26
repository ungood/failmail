using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Ninject.Web.Common;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace Failmail.Web.App_Start
{
    public class RavenModule : NinjectModule
    {
        public override void Load()
        {
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
            var store = new EmbeddableDocumentStore
            {
                DataDirectory = "App_Data",
                UseEmbeddedHttpServer = true,
            };
            store.Initialize();

            Bind<IDocumentStore>()
                .ToConstant(store);

            Bind<IDocumentSession>()
                .ToMethod(ctx => store.OpenSession())
                .InRequestScope();

        }
    }
}