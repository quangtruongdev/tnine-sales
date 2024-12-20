﻿using System.Web;
using System.Web.Mvc;

namespace tnine.Web.Host.Infrastructure.Core
{
    public static class ServiceFactory
    {
        public static THelper GetHelper<THelper>()
        {
            if (HttpContext.Current != null)
            {
                var key = string.Concat("factory-.", typeof(THelper).Name);
                if (!HttpContext.Current.Items.Contains(key))
                {
                    var resolvedService = DependencyResolver.Current.GetService<THelper>();
                    HttpContext.Current.Items.Add(key, resolvedService);
                }
                return (THelper)HttpContext.Current.Items[key];
            }
            return DependencyResolver.Current.GetService<THelper>();
        }
    }
}