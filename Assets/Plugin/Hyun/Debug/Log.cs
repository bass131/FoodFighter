using System;
using System.Text;
using UnityEngine;

namespace Hyun.Debug
{
    using Extensions;
    public class Log
    {
        public static bool isDebugBuild
        {
            get
            {
                return UnityEngine.Debug.isDebugBuild;
            }
        }

        public static StringBuilder logMessage = new StringBuilder();
        public static object s_lock = new object();


        public static string CallerName
        {
            get
            {

                string ret = "[NoClass]";
                try
                {
                    var st = new System.Diagnostics.StackTrace();
                    var index = Mathf.Min(st.FrameCount - 1, 2);
                    if (index < 0)
                    {
                        throw new StackOverflowException("plz don't see me");
                    }
                    System.Reflection.MethodBase method = st.GetFrame(index).GetMethod();
                    string ClassName = method.DeclaringType.Name;
                    string MethodName = method.Name;
                    int CurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId;

                    ret = string.Format("tid:{0}[{1}::{2}]", CurrentThread, ClassName, MethodName);
                }
                catch (Exception e)
                {
                    ret = e.Message;
                }

                return ret;

            }
        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void CanNotFoundFile(string filePath)
        {

            ErrorFormat("{0} Cannot Found!", filePath);
        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void ErrorFormat(UnityEngine.Object context, string template, params object[] args)
        {
            lock (s_lock)
            {

                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "{0} : {1}", CallerName, string.Format(template, args));

                UnityEngine.Debug.LogErrorFormat(context, logMessage.ToString(), args);
            }



        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void ErrorFormat(string template, params object[] args)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "{0} : {1}", CallerName, string.Format(template, args));


                UnityEngine.Debug.LogErrorFormat(logMessage.ToString(), args);
            }


        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void Error(object message)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "{0} : {1}", CallerName, message);

                UnityEngine.Debug.LogError(logMessage.ToString());
            }





        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void Error(UnityEngine.Object context, object message)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "{0} : {1}", CallerName, message);

                UnityEngine.Debug.LogError(logMessage.ToString(), context);
            }




        }


        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void Exception(System.Exception exception, UnityEngine.Object context = null)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "CAUGHT EXCEPTION !!!!");
                UnityEngine.Debug.LogException(exception, context);
            }

        }


        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void WarningFormat(UnityEngine.Object context, string template, params object[] args)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.magenta, "{0} : {1}", CallerName, string.Format(template, args));

                UnityEngine.Debug.LogWarning(logMessage.ToString(), context);
            }



        }
        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void WarningFormat(string template, params object[] args)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.magenta, "{0} : {1}", CallerName, string.Format(template, args));

                UnityEngine.Debug.LogWarning(logMessage.ToString());


            }

        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void Warning(object message)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.magenta, "{0} : {1}", CallerName, message);

                UnityEngine.Debug.LogWarning(logMessage.ToString());

            }


        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void Warning(UnityEngine.Object context, object message)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.magenta, "{0} : {1}", CallerName, message);

                UnityEngine.Debug.LogWarning(logMessage.ToString(), context);

            }


        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void MessageFormat(UnityEngine.Object context, string template, params object[] args)
        {

            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.blue, "{0} : {1}", CallerName, string.Format(template, args));


                UnityEngine.Debug.Log(logMessage.ToString(), context);
            }



        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void MessageFormat(string template, params object[] args)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.blue, "{0} : {1}", CallerName, string.Format(template, args));

                UnityEngine.Debug.Log(logMessage.ToString());


            }


        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void Message(object message)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.blue, "{0} : {1}", CallerName, message);

                UnityEngine.Debug.Log(logMessage.ToString());
            }

        }
        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void Message(Color c, object message)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(c, "{0} : {1}", CallerName, message);

                UnityEngine.Debug.Log(logMessage.ToString());
            }

        }



        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void Message(UnityEngine.Object context, object message)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "{0} : {1}", CallerName, message);

                UnityEngine.Debug.Log(logMessage.ToString(), context);

            }


        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void AssertFormat(bool condition, string template, params object[] args)
        {

            if (!condition)
            {
                ErrorFormat(template, args);

            }



        }



        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void VerboseFormat(UnityEngine.Object context, string template, params object[] args)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "{0} : {1}", CallerName, string.Format(template, args));


                UnityEngine.Debug.Log(logMessage.ToString(), context);
            }

        }

        [System.Diagnostics.Conditional("ENABLE_DEBUG")]
        public static void VerboseFormat(string template, params object[] args)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "{0} : {1}", CallerName, string.Format(template, args));

                UnityEngine.Debug.Log(logMessage.ToString());

            }

        }


        public static void Verbose(object message)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "{0} : {1}", CallerName, message);

                UnityEngine.Debug.Log(logMessage.ToString());
            }


        }

        public static void Verbose(UnityEngine.Object context, object message)
        {
            lock (s_lock)
            {
                logMessage.Clear();
                logMessage.AppendFormatWithColor(Color.red, "{0} : {1}", CallerName, message);

                UnityEngine.Debug.Log(logMessage.ToString(), context);
            }


        }




    }
}