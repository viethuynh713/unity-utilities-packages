using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;

namespace VPackages.System.Ads.modules.impl
{
    public class InterstitialAdsController :IInterstitialAdsController
    {
        private string _interId;

        private InterstitialAd _interstitialAd;

        public UniTask<bool> Initialize(string interId)
        {
            _interId = interId;
            LoadInterstitialAd();
            return new UniTask<bool>(true);
        }

        public void LoadInterstitialAd()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            var adRequest = new AdRequest();

            InterstitialAd.Load(_interId, adRequest, (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    //print("Interstitial ad failed to load" + error);
                    return;
                }

                //print("Interstitial ad loaded !!" + ad.GetResponseInfo());

                _interstitialAd = ad;
                InterstitialEvent(_interstitialAd);
            });
        }

        public void ShowInterstitialAd()
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                _interstitialAd.Show();
            }
            else
            {
                //print("Interstitial ads not ready!!");
            }
        }

        private void InterstitialEvent(InterstitialAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log("Interstitial ad paid {0} {1}." +
                          adValue.Value +
                          adValue.CurrencyCode);
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () => { Debug.Log("Interstitial ad recorded an impression."); };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () => { Debug.Log("Interstitial ad was clicked."); };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () => { Debug.Log("Interstitial ad full screen content opened."); };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () => { Debug.Log("Interstitial ad full screen content closed."); };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);
            };
        }
    }
}