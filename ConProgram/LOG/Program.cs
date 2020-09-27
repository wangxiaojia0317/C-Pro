using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


//注意在assembly添加的监视
namespace LOG
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                Log.d("asdhi", "asdhi");
               // s.Append($"sakjfdo**{i}\n");
            }
            Log.d("asdhi", s.ToString());
            Console.Read();
        }
        public class Log
        {
            private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

            /// <summary>
            /// Log a message with the log4net.Core.Level.Debug level.
            /// </summary>
            /// <param name="tag">The tag of log.</param>
            /// <param name="message">The message to log.</param>
            public static void d(string tag, string message)
            {
                if (log.IsDebugEnabled)
                {
                    log.Debug(tag + ":" + message);
                  //  Trace.WriteLine(message);
                }
            }
            /// <summary>
            /// Log a message with the log4net.Core.Level.Error level.
            /// </summary>
            /// <param name="tag">The tag of log.</param>
            /// <param name="message">The message to log.</param>
            public static void e(string tag, string message)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error(tag + ":" + message);
                }
            }
            /// <summary>
            /// Log a message with the log4net.Core.Level.Fatal level.
            /// </summary>
            /// <param name="tag">The tag of log.</param>
            /// <param name="message">The message to log.</param>
            public static void f(string tag, string message)
            {
                if (log.IsFatalEnabled)
                {
                    log.Fatal(tag + ":" + message);
                }
            }
            /// <summary>
            /// Log a message with the log4net.Core.Level.Info level.
            /// </summary>
            /// <param name="tag">The tag of log.</param>
            /// <param name="message">The message to log.</param>
            public static void i(string tag, string message)
            {
                if (log.IsInfoEnabled)
                {
                    log.Info(tag + ":" + message);
                }
            }
            /// <summary>
            /// Log a message with the log4net.Core.Level.Warn level.
            /// </summary>
            /// <param name="tag">The tag of log.</param>
            /// <param name="message">The message to log.</param>
            public static void w(string tag, string message)
            {
                if (log.IsWarnEnabled)
                {
                    log.Warn(tag + ":" + message);
                }
            }
        }
    }
}
