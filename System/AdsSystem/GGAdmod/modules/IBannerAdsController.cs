using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;

namespace VPackages.System.Ads.modules
{
    public interface IBannerAdsController
    {
        UniTask<bool> Initialize(string bannerId, AdPosition bannerAdPosition);
        void ShowBannerAd();
        void DestroyBannerAd();

    }
}