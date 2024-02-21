using Project;
using Project.DependencyInjection.Core;
using UnityEngine.Scripting;

namespace DIRegistrators
{
    public class ProfileDIRegistrator : DIModule
    {
        [Preserve]
        public ProfileDIRegistrator()
        {

        }

        [Preserve]
        protected override void Load()
        {
            Register<IProfile, Profile>(Profile.GetInstance);
        }
    }
}