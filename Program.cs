using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JoysCakeShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //This will create a server and a request processing pipeline
            CreateHostBuilder(args).Build().Run();
        }

        // CreateDefaultBuilder uses Kestrel webserver by default 
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
