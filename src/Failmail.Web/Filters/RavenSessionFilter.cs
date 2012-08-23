using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;

namespace Failmail.Web.Filters
{
    public class RavenSessionFilter : IActionFilter
    {
        private readonly IDocumentSession session;

        public RavenSessionFilter(IDocumentSession session)
        {
            this.session = session;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            if (filterContext.Exception != null)
                return;

            if (session != null)
                session.SaveChanges();
        }
    }
}