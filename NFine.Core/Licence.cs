using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Web;


namespace NFine.Code
{
    public sealed class Licence
    {
        public static bool IsLicence(string key)
        {
            throw new NotImplementedException();
            string host = "";// HttpContext.Current.Request.Host.Host.ToLower();
            if (host.Equals("localhost"))
                return true;
            string licence = Configs.GetValue("LicenceKey");
            if (licence != null && licence == EncryptProvider.Md5(key, Internal.MD5Length.L32))
                return true;

            return false;
        }
        public static string GetLicence()
        {
            var licence = Configs.GetValue("LicenceKey");
            if (string.IsNullOrEmpty(licence))
            {
                licence = Common.GuId();
                Configs.SetValue("LicenceKey", licence);
            }
            return EncryptProvider.Md5(licence, Internal.MD5Length.L32);
        }
    }
}
