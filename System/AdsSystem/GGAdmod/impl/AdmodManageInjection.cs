using VContainer.Unity;
using VPackages.System.GGAdmod.impl;

namespace VPackages.System.Ads.impl
{
    public class AdmodManageInjection : AdmodSystemBase, IInitializable
    {
        async void IInitializable.Initialize()
        {
            await base.Initialize();
        }
    }
}