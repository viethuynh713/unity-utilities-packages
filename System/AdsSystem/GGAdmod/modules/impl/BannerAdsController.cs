using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;

namespace VPackages.System.Ads.modules.impl
{
    public class BannerAdsController :  IBannerAdsController
    {
        private AdPosition _bannerAdPosition;
        private string _bannerId;
        private BannerView _bannerView;

        public UniTask<bool> Initialize(string bannerId, AdPosition bannerAdPosition)
        {
            _bannerId = bannerId;
            return new UniTask<bool>(true);
        }
        

        public void ShowBannerAd()
        {
            //create a banner
            CreateBannerView();

            //listen to banner events
            ListenToBannerEvents();

            //load the banner
            if (_bannerView == null)
            {
                CreateBannerView();
            }

            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");

            _bannerView?.LoadAd(adRequest); //show the banner on the screen
        }

        private void CreateBannerView()
        {
            if (_bannerView != null)
            {
                DestroyBannerAd();
            }

            _bannerView = new BannerView(_bannerId, AdSize.Banner, _bannerAdPosition);
        }

        private void ListenToBannerEvents()
        {
            _bannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("Banner view loaded an ad with response : "
                          + _bannerView.GetResponseInfo());
            };
            // Raised when an ad fails to load into the banner view.
            _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                               + error);
            };
            // Raised when the ad is estimated to have earned money.
            _bannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log("Banner view paid {0} {1}." +
                          adValue.Value +
                          adValue.CurrencyCode);
            };
            // Raised when an impression is recorded for an ad.
            _bannerView.OnAdImpressionRecorded += () => { Debug.Log("Banner view recorded an impression."); };
            // Raised when a click is recorded for an ad.
            _bannerView.OnAdClicked += () => { Debug.Log("Banner view was clicked."); };
            // Raised when an ad opened full screen content.
            _bannerView.OnAdFullScreenContentOpened += () => { Debug.Log("Banner view full screen content opened."); };
            // Raised when the ad closed full screen content.
            _bannerView.OnAdFullScreenContentClosed += () => { Debug.Log("Banner view full screen content closed."); };
        }

        public void DestroyBannerAd()
        {
            if (_bannerView != null)
            {
                _bannerView.Destroy();
                _bannerView = null;
            }
        }
    }
}