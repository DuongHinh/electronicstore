using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Web.Core
{
    public static class ServiceFactory
    {
        public static TService Get<TService>()
        {
            if (HttpContext.Current != null)
            {
                var key = string.Concat("factory-", typeof(TService).Name);
                if (!HttpContext.Current.Items.Contains(key))
                {
                    var resolvedService = DependencyResolver.Current.GetService<TService>();
                    HttpContext.Current.Items.Add(key, resolvedService);
                }
                return (TService)HttpContext.Current.Items[key];
            }
            return DependencyResolver.Current.GetService<TService>();
        }
    }
}