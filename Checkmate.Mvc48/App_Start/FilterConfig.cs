﻿using System.Web;
using System.Web.Mvc;

namespace Checkmate.Mvc48
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
