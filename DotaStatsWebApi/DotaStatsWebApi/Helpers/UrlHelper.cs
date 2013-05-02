using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaStatsWebApi.Helpers
{
    public class UrlHelper
    {
        public static string GetImageUrlBase()
        {
            return "https://raw.github.com/richhildebrand/DotaWebApi/master/DotaStatsWebApi/DotaStatsWebApi/Content/";
        }
    }
}