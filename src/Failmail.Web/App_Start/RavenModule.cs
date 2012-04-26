using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
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
            Bind<IDocumentStore>().ToMethod(ctx =>
            {
                NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
                var documentStore = new EmbeddableDocumentStore
                {
                    DataDirectory = "App_Data",
                    UseEmbeddedHttpServer = true,
                };
                return documentStore.Initialize();
            }).InSingletonScope();

            Bind<IDocumentSession>()
                .ToMethod(ctx => ctx.Kernel.Get<IDocumentStore>().OpenSession())
                .InRequestScope();

        }
    }
}