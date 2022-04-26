using System;
using System.Collections;
using System.Collections.Generic;
using InternalAssets._Scripts.Monetization.Ads;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoseGameView : MonoBehaviour
{ 
    [SerializeField]
    private GameObject _сontinueButton;
    private TypeAds _typeAds;
    private Coroutine _checkLoadedAdsCoroutine;
    
    private void Start()
    {
        _typeAds = GameController.Instance.TypeAds;
        _checkLoadedAdsCoroutine =  GameController.Instance.Context.Current.StartCoroutine(CheckLoadedAds());
    }

    private IEnumerator CheckLoadedAds()
    {
        _сontinueButton.SetActive(_typeAds is TypeAds.Loaded);
        yield return new WaitForSeconds(5f);
    }

    private void OnDestroy()
    {
        if (GameController.Instance)
        {
            GameController.Instance.Context.Current.StopCoroutine(_checkLoadedAdsCoroutine);
        }
    }
}
