using System;
using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using VPackages.System.Ads;
using VPackages.System.Ads.modules;
using VPackages.System.Ads.modules.impl;

namespace VPackages.System.GGAdmod.impl
{
    public class AdmodSystemBase : IAdsSystem
    {
#if UNITY_ANDROID
        private string _bannerId = "";
        private string _interId = "";
        private string _rewardId = "";
#elif UNITY_IOS
        private string _bannerId = "";
        private string _interId = "";
        private string _rewardId = "";
#else
        private string _bannerId = "";
        private string _interId = "";
        private string _rewardId = "";


#endif
        private IBannerAdsController _bannerAdsController;
        private IInterstitialAdsController _interstitialAdsController;
        private IRewardAdsController _rewardAdsController;

        //public NativeAdsController NativeAdsController;
        
        public async UniTask<bool> Initialize()
        {
            _bannerAdsController = new BannerAdsController();
            _interstitialAdsController = new InterstitialAdsController();
            _rewardAdsController = new RewardAdsController();

            await _bannerAdsController.Initialize(_bannerId, AdPosition.Bottom);
            await _interstitialAdsController.Initialize(_interId);
            await _rewardAdsController.Initialize(_rewardId);
            
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            MobileAds.Initialize(initStatus =>
            {
                LoadInterstitialAd();
                LoadRewardedAd();
                ShowBannerAd();
            }); 
            return true;
        }

        public void ShowBannerAd()
        {
            _bannerAdsController.ShowBannerAd();
        }

        public void DestroyBannerAd()
        {
            _bannerAdsController.DestroyBannerAd();
        }

        public void LoadInterstitialAd()
        {
            _interstitialAdsController.LoadInterstitialAd();
        }

        public void ShowInterstitialAd()
        {
            _interstitialAdsController.ShowInterstitialAd();
        }

        public void LoadRewardedAd()
        {
            _rewardAdsController.LoadRewardedAd();
        }

        public void ShowRewardedAd(Action callback)
        {
            _rewardAdsController.ShowRewardedAd(callback);
        }

    }
}