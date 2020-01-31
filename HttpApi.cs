namespace aoptest
{
    public class HttpApi : IHttpApi
    {
        public string Get(string url)
        {
            return "Helllo Call Get API";
        }
    }
}
