using System;
using Cysharp.Threading.Tasks;

namespace VPackages.System.Ads
{
    public interface IAdsSystem
    {
        UniTask<bool> Initialize();
        void ShowBannerAd();
        void DestroyBannerAd();
        void LoadInterstitialAd();
        void ShowInterstitialAd();
        void LoadRewardedAd();
        void ShowRewardedAd(Action callback);
        
    }
}