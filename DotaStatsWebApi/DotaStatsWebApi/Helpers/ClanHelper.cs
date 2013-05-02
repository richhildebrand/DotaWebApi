using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaStatsWebApi.Helpers
{
    public class ClanHelper
    {
        public string GenrateClanImageUrl(int clanId)
        {
            var clanImageNumber = clanId % 10;
            return UrlHelper.GetImageUrlBase() + "ClanPortraits/" + "clanImage" + clanImageNumber + ".PNG";
        }
    }
}