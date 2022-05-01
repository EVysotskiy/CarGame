using InternalAssets._Scripts.Monetization.Ads;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController:Window<AdsView>,IUnityAdsInitializationListener
{
    private const string PATH_MONETIZATION_CONFIG = "ads/adsConfigure";
    private const string NAME_ADS_VIEW_PREFAB = "ads/Ads";
    private AdsView _adsView;
    public AdsController(IContext context):base(context)
    {
        Initialize();
    }

    private void Initialize()
    {
        var monetizationConfig = GetMonetizationConfig();
        _adsView = CreateView<AdsView>(NAME_ADS_VIEW_PREFAB);
#if UNITY_IOS
        Advertisement.Initialize(monetizationConfig.iosGameId);
#endif
        Advertisement.Initialize(monetizationConfig.androidGameId);
    }

    private ConfigAds GetMonetizationConfig()
    {
        var configJson = Resources.Load<TextAsset>(PATH_MONETIZATION_CONFIG);
        return JsonUtility.FromJson<ConfigAds>(configJson.text);
    }

    public void ShowAdd()
    {
        _adsView.ShowAd();
    }
    public void OnInitializationComplete()
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }
}