using System;
using Cysharp.Threading.Tasks;

namespace VPackages.System.Ads.modules
{
    public interface IRewardAdsController
    {
        UniTask<bool> Initialize(string rewardedId);
        void LoadRewardedAd();
        void ShowRewardedAd(Action callback);
    }
    
}