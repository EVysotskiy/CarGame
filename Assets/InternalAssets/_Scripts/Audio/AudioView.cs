using UnityEngine;

public class AudioView : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceClick;
    [SerializeField] private AudioSource _audioSourceEffects;
    [SerializeField] private AudioSource _audioSourceCrashed;
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceCity;
    private const string SOUND_KEY = "sound";
    private bool _isSound; 

    private void Awake()
    {
        SetStateSound();
    }
    
    public void Click()
    {
        SoundPlay(_audioSourceClick);
    }

    public void Effect()
    {
        SoundPlay(_audioSourceEffects);
    }
    
    public void OnEditStateSound()
    {
        SetStateSound();
    }

    private void SetStateSound()
    {
        _isSound = PlayerPrefsUtils.GetBool(SOUND_KEY);
        SoundPlay(_audioSourceMusic);
        SoundPlay(_audioSourceCity);
    }

    public void OnCrached()
    {
        SoundPlay(_audioSourceCrashed);
    }

    private void SoundPlay(AudioSource audioSource)
    {
        if (_isSound)
        {
            audioSource.Play();
            return;
        }
        audioSource.Pause();
    }
    
}
