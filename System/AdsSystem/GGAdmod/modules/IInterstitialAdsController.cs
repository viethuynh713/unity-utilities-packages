using Cysharp.Threading.Tasks;

namespace VPackages.System.Ads.modules
{
    public interface IInterstitialAdsController
    {
        UniTask<bool> Initialize(string interId);
        void LoadInterstitialAd();
        void ShowInterstitialAd();
    }
}