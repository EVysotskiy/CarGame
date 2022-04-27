using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsView : MonoBehaviour
{
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private  Image _imageSoundState;
    [SerializeField] private GameObject _infoPane;
    private const string SOUND_KEY = "sound";
    private bool _isSound;
    private void Awake()
    {
        _isSound = PlayerPrefsUtils.GetBool(SOUND_KEY);
        var canvas = GetComponent<Canvas>(); 
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 30;
    }

    public void SwitchSoundState()
    {
        SetSoundState(!_isSound);
        RenderImageSoundState();

    }

    private void SetSoundState(bool isSound)
    {
        _isSound = isSound;
        PlayerPrefsUtils.SetBool(SOUND_KEY,isSound);
    }

    private void RenderImageSoundState()
    {
        if (_isSound)
        {
            _imageSoundState.sprite = _soundOn;
            return;
        }

        _imageSoundState.sprite = _soundOff;
    }

    public void OpenInfoPanel()
    {
        _infoPane.SetActive(true);
    }

    public void CloseInfoPanel()
    {
        _infoPane.SetActive(false);
    }
    public void Close()
    {
        Destroy(gameObject);
    }
}
