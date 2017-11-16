namespace Croquette
{
    public interface IResourceEventListener
    {
        void OnResLoadBegin(IResource pRes);
        void OnResLoadEnd(IResource pRes);
    }
}