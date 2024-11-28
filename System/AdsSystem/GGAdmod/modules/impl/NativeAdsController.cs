// using System;
// using System.Collections;
// using System.Collections.Generic;
// using GoogleMobileAds.Api;
// using UnityEngine;
// using UnityEngine.UI;
// namespace VPackage.Ads
// {
//     public class NativeAdsController : MonoBehaviour
//     {
//         [SerializeField] private string nativeIdIOS;
//         [SerializeField] private string nativeIdAndroid;
//         [SerializeField] public Image img;
//         [SerializeField] public TMPro.TMP_Text headline;
//
//         private string _nativeId;
//         private NativeAd _nativeAd;
//
//
//         private void Awake()
//         {
// #if UNITY_ANDROID
//                 _nativeId = nativeIdAndroid;
// #elif UNITY_IPHONE
//                 _nativeId = nativeIdIOS;
// #else
//             _nativeId = "";
// #endif
//         }
//
//         public void RequestNativeAd()
//         {
//             AdLoader adLoader = new AdLoader.Builder(_nativeId).ForNativeAd().Build();
//
//             adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
//             adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
//
//             adLoader.LoadAd(new AdRequest.Builder().Build());
//         }
//
//         private void HandleNativeAdLoaded(object sender, NativeAdEventArgs e)
//         {
//             Debug.Log("Native ad loaded");
//             this._nativeAd = e.nativeAd;
//
//             Texture2D iconTexture = this._nativeAd.GetIconTexture();
//             Sprite sprite = Sprite.Create(iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height),
//                 Vector2.one * .5f);
//             if (headline != null) headline.text = _nativeAd.GetHeadlineText();
//             if (img != null) img.sprite = sprite;
//         }
//
//         private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
//         {
//             Debug.Log("Native ad failed to load" + e.ToString());
//         }
//     }
// }