using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace GC5.Application.Extensions
{ 
   public class SingleLogger
    {

        public static ILoggerFactory Factory;
        private static SingleLogger instance;
        private static Object objectToLock = new Object();
        public bool IsLocalizationToRefresh { get; set; }

        public ILogger<SingleLogger> Logger { get; private set; }

        private SingleLogger()
        {

        }

        public static SingleLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (objectToLock)
                    {
                        if (instance == null)
                        {
                            var logger = Factory.CreateLogger<SingleLogger>();
                            instance = new SingleLogger();
                            instance.Logger = logger;
                        }
                    }
                }
                return instance;
            }
        }
    }
}

