namespace aoptest
{
    public interface IHttpApi
    {
        public string Get(string url);

        public string Post(string url, object data);
    }
}
