using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OctoPerf.PetStore.Automation.Framework.Data
{
    public static partial class Data
    {
        public static class Config
        {
            private static IConfigurationRoot _configuration;
            private static IConfigurationRoot Configuration
            {
                get
                {
                    if (_configuration == null)
                    {
                        var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile($"AppSettings.json");
                        _configuration = builder.Build();
                    }
                    return _configuration;
                }
            }

            public static string GetConfigData(string key)
            {
                return Configuration[$"appSettings:{key}"];
            }

            public static string GetUserFirstName()
            {
                return GetConfigData("TestUser:FirstName");
            }

            public static string GetUserLastName()
            {
                return GetConfigData("TestUser:LastName");
            }

            public static string GetUserId()
            {
                return GetConfigData("TestUser:UserName");
            }

            public static string GetUserPassword()
            {
                return GetConfigData("TestUser:Password");
            }
        }
    }
}
