using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace EverlightTestAutomation.FunctionLibrary
{
    public class ConfigurationDriver
    {
        public string Browser => Environment.GetEnvironmentVariable("Test_Browser");
    }
}
