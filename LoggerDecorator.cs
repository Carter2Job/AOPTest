using System;
using System.Reflection;

namespace aoptest
{
    public class LoggerDecorator<T> : DispatchProxy
    {
        private static readonly NLog.Logger nlogger = NLog.LogManager.GetCurrentClassLogger();
        private T _decorated;
        public static T Create(T decorated)
        {
            object proxy = Create<T, LoggerDecorator<T>>();
            ((LoggerDecorator<T>)proxy).SetParameters(decorated);

            return (T)proxy;
        }

        private void SetParameters(T decorated)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            _decorated = decorated;
        }


        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                LogBefore(targetMethod, args);

                var result = targetMethod.Invoke(_decorated, args);

                LogAfter(targetMethod, args, result);
                return result;
            }
            catch (Exception ex) when (ex is TargetInvocationException)
            {
                LogException(ex.InnerException ?? ex, targetMethod);
                throw ex.InnerException ?? ex;
            }
        }

        private void LogException(Exception exception, MethodInfo methodInfo = null)
        {
            nlogger.Info($"Class {_decorated.GetType().FullName}, Method {methodInfo.Name} threw exception:\n{exception}");
        }

        private void LogAfter(MethodInfo methodInfo, object[] args, object result)
        {
            nlogger.Info($"Class {_decorated.GetType().FullName}, Method {methodInfo.Name} executed, Output: {result}");
        }

        private void LogBefore(MethodInfo methodInfo, object[] args)
        {
            nlogger.Info($"Class {_decorated.GetType().FullName}, Method {methodInfo.Name} is executing");
        }
    }
}
