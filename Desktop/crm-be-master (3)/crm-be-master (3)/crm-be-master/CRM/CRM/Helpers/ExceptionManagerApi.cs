using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using log4net;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.Logging;
using log4net.Appender;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using CRM.Service;

namespace CRM.Helpers
{
    public static class Logger
    {

        private static readonly string LOG_CONFIG_FILE = @"log4net.config";

        private static readonly log4net.ILog _log = GetLogger(typeof(Logger));

        public static log4net.ILog GetLogger(Type type)
        {
            return log4net.LogManager.GetLogger(type);
        }

        public static void Error(object message,Exception e)
        {
            SetLog4NetConfiguration();
            //_log.Debug(message +", method is : "+ new StackTrace().GetFrame(1).GetMethod().Name+", "+ NameOfCallingClass());
            _log.Error(String.Format(" On class: {0} with method: {1}", NameOfCallingClass(), new StackTrace().GetFrame(1).GetMethod().Name), e);
        }

        public static string NameOfCallingClass()
        {
            string fullName;
            Type declaringType;
            int skipFrames = 2;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                fullName = declaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return fullName;
        }


        private static void SetLog4NetConfiguration()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(LOG_CONFIG_FILE));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }
    }
}
