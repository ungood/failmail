using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Failmail.Web.App_Start
{
    public static class AppSettings
    {
        private static readonly Dictionary<string, string> Secrets = new Dictionary<string, string>(); 

        public static void FillSecrets()
        {
            var assembly = typeof(AppSettings).Assembly;
            var stream = assembly.GetManifestResourceStream("Failmail.Web.secret.txt");
            if (stream == null)
                return;

            using(var reader = new StreamReader(stream))
            {
                var lines = reader.ReadToEnd().Split('\n');
                foreach(var line in lines)
                {
                    var split = line.Split('=');
                    if(split.Length == 2)
                    {
                        Secrets.Add(split[0].Trim(), split[1].Trim());
                    }
                }
            }
        }
        public static string Get(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if(value == "SECRET")
            {
                if(!Secrets.TryGetValue(key, out value))
                    value = "MISSING_SECRET";
            }

            return value;
        }
    }
}