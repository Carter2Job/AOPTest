using System;

namespace aoptest
{
    class Program
    {
        static void Main(string[] args)
        {
            var decoratedAPI = LoggerDecorator<IHttpApi>.Create(new HttpApi());
            decoratedAPI.Get("https://www.google.com");
        }
    }
}
