namespace aoptest
{
    public class HttpApi : IHttpApi
    {
        public string Get(string url)
        {
            return "Helllo Call Get API";
        }

        public string Post(string url, object data)
        {
            return "Helllo Call Post API";
        }
    }
}
