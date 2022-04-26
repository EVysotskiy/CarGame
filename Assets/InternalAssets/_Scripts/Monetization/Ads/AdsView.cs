using UnityEngine;
using UnityEngine.Advertisements;

namespace InternalAssets._Scripts.Monetization.Ads
{
    public class AdsView:MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string _androidAdUnitId = "Interstitial_Android";
        private const string _iOsAdUnitId = "Interstitial_iOS";
        private string _adUnitId;
        void Awake()
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsAdUnitId
                : _androidAdUnitId;
            LoadAd();
        }
        
        public void LoadAd()
        {
            Advertisement.Load(_adUnitId, this);
        }
        
        public void ShowAd()
        {
            Advertisement.Show(_adUnitId, this);
        }
        
        public void OnUnityAdsAdLoaded(string placementId)
        {
            GameController.Instance.SetTypeAds(TypeAds.Loaded);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
            GameController.Instance.SetTypeAds(TypeAds.NotUploaded);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
            GameController.Instance.SetTypeAds(TypeAds.NotUploaded);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            GameController.Instance.SetTypeAds(TypeAds.NotUploaded);
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            LoadAd();
        }
    }
}