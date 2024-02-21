using Project;
using Project.DependencyInjection.Core;
using UnityEngine.Scripting;

namespace DIRegistrators
{
    public class SaveSystemDIRegistrator : DIModule
    {
        [Preserve]
        protected override void Load()
        {
            Register<ISaveSystem, SaveSystem>(SaveSystem.GetInstance);
        } 
    }
}