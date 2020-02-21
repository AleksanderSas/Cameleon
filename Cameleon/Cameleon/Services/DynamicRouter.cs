namespace Cameleon.Services
{
    public interface IDynamicRouter
    {
        ITemplate Route(string url, string method);
    }

    public class DynamicRouter : IDynamicRouter
    {
        public ITemplate Route(string url, string method)
        {
            throw new System.NotImplementedException();
        }
    }
}
