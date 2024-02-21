using Project.DependencyInjection.Core;
using Sounds;
using Sounds.Interface;

namespace DIRegistrators
{
    public class SoundManagerDIRegistrator : DIModule
    {
        protected override void Load()
        {
            Register<ISoundManager, SoundManager>(SoundManager.GetInstance);
        }
    }
}