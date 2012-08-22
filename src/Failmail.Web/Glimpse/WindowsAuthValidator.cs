using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Validator;

namespace Failmail.Web.Glimpse
{
    [GlimpseValidator]
    public class WindowsAuthValidator : IGlimpseValidator
    {
        public bool IsValid(HttpContextBase context, LifecycleEvent lifecycleEvent)
        {
            return true;
        }
    }
}