using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace wwwCommon
{
    public class ConfigFileHepler
    {

        private readonly IConfiguration configuration;
        public ConfigFileHepler(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string GetConfigStr(string filePath,string fileName)
        {
            var builder = new ConfigurationBuilder().SetBasePath(filePath).AddJsonFile(fileName);

            return "";
        }
    }
}
