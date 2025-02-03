using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace APIRestSharpCoreLayer.Utils
{
    public static class AppSettings
    {
        private static readonly IConfigurationRoot Configuration;
        static AppSettings()
        {
            string filepath = Directory.GetCurrentDirectory().Split("TestLayer")[0] + "TestLayer";
            Configuration = new ConfigurationBuilder()
               .SetBasePath(filepath)
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true).Build();
        }
        public static string GetAppSettingData(string key)
        {
            try
            {
                //if (string.IsNullOrEmpty(Configuration[key])) { throw new ArgumentException("Invalid key provided", nameof(key)); }
                //else
                    return Configuration[key];

            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"provided key {key} or data is not in correct format- {ex.Message}");
                throw new Exception($"provided key {key} or data is not in correct format- {ex.Message}");
            }
        }
    }
}
