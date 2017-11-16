
namespace Croquette.Scenes
{

    public interface IScene 
    {
        void OnRequestDataScript();

        void OnUIClick(UnityEngine.UI.Selectable sender, int hint);
    }
}