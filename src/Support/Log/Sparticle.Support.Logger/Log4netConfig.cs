using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.Logger
{
    class Log4netConfig
    {
        public static readonly string Domains = ConfigurationManager.AppSettings["Log4netDomains"];
        public static readonly string RootLogDir = ConfigurationManager.AppSettings["Log4netRootDir"];

        public void CreateRespositiesByCode()
        {
            foreach (var domain in Domains.Split(','))
            {
                if (string.IsNullOrEmpty(domain))
                    continue;

                CreateResposityByCode(domain);
            }
        }

        public void CreateRespositiesByXml()
        {
            var basepath = AppDomain.CurrentDomain.BaseDirectory;
            var xmlConfigPath = System.IO.Path.Combine(basepath, "log4net.xml");

            foreach (var domain in Domains.Split(','))
            {
                if (string.IsNullOrEmpty(domain))
                    continue;

                CreateResposityByXml(domain, xmlConfigPath);
            }
        }

        private void CreateResposityByXml(string domain, string xmlConfigPath)
        {
            var xmlConfig = new FileInfo(xmlConfigPath);

            if (!xmlConfig.Exists)
                throw new ConfigurationErrorsException(string.Format("log4net config file [{0}] do not exist", xmlConfigPath));

            var repository = LogManager.CreateRepository(domain);

            XmlConfigurator.Configure(repository, xmlConfig);
        }

        private static readonly Tuple<Level, string>[] levels = {
                            Tuple.Create(Level.Info, Level.Info.Name),
                            Tuple.Create(Level.Warn, Level.Warn.Name),
                            Tuple.Create(Level.Error, Level.Error.Name),
                            Tuple.Create(Level.Notice, "SLOW"),
        };

        private void CreateResposityByCode(string domain)
        {
            var repository = LogManager.CreateRepository(domain);

            foreach (var level in levels)
            {
                var filter = CreateLevelFilter(level.Item1);

                var fileAppender = CreateFileAppender(domain, RootLogDir, level.Item2, filter);

                BasicConfigurator.Configure(repository, fileAppender);
            }
        }

        private FilterSkeleton CreateLevelFilter(Level level)
        {
            var levelFilter = new LevelMatchFilter();

            levelFilter.LevelToMatch = level;
            levelFilter.ActivateOptions();

            return levelFilter;
        }

        private static readonly string layoutFormat = "@Log Begin%newlineThread ID：[%thread]%newline%message%newlineLog End@%newline";
        private static ILayout DefaultLayout = new PatternLayout(layoutFormat);

        private FileAppender CreateFileAppender(string domain, string rootLogDir, string level, FilterSkeleton filter)
        {
            var fileAppender = new RollingFileAppender();
            fileAppender.Name = domain + "_" + level + "_FileAppender";
            fileAppender.File = System.IO.Path.Combine(rootLogDir, "Log_" + domain + "\\" + level + "\\");
            fileAppender.AppendToFile = true;
            fileAppender.RollingStyle = RollingFileAppender.RollingMode.Date;
            fileAppender.DatePattern = "yyyy-MM-dd'.log'";
            fileAppender.StaticLogFileName = false;
            fileAppender.Layout = DefaultLayout;
            fileAppender.AddFilter(filter);
            fileAppender.ActivateOptions();

            return fileAppender;
        }
    }
}
