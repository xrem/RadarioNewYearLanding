using System;
using System.Configuration;
using System.IO;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NewYearLanding.Infrastructure;

namespace NewYearLanding {
    public static class Program {
        public static void Main(string[] args) {
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .UseKestrel((context, options) => {
                        options.Listen(IPAddress.Any, int.Parse(context.Configuration["ListenPort"]));
                        options.AddServerHeader = false;
                    })
                   .Build()
                   .Run();
        }
    }
}
