using System;
using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;

namespace VPackages.System.Ads.modules.impl
{
    public class RewardAdsController : IRewardAdsController
    {
        private string _rewardedId;
        private RewardedAd _rewardedAd;

        public UniTask<bool> Initialize(string rewardedId)
        {
            _rewardedId = rewardedId;
            return new UniTask<bool>(true);
        }

        public void LoadRewardedAd()
        {

            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }
            var adRequest = new AdRequest();
            RewardedAd.Load(_rewardedId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    //print("Rewarded failed to load" + error);
                    return;
                }

                //print("Rewarded ad loaded !!");
                _rewardedAd = ad;
                RewardedAdEvents(_rewardedAd);
            });
        }
        public void ShowRewardedAd(Action callback)
        {

            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((Reward reward) =>
                {
                    Debug.Log($"Reward type :{reward.Type} with amount {reward.Amount}");
                    callback?.Invoke();
                });
            }
            else
            {
                //print("Rewarded ad not ready");
            }
        }

        private void RewardedAdEvents(RewardedAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log("Rewarded ad paid {0} {1}." +
                    adValue.Value +
                    adValue.CurrencyCode);
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Rewarded ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Rewarded ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Rewarded ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Rewarded ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Rewarded ad failed to open full screen content " +
                               "with error : " + error);
            };
        }
    }
}