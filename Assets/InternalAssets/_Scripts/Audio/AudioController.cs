namespace InternalAssets._Scripts.Audio
{
    public class AudioController:Window<AudioView>
    {
        private AudioView _audioView;
        private const string PREFAB_NAME_AUDIO_VIEW = "audio/AudioSource";
        public AudioController(IContext context):base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            _audioView = CreateView<AudioView>(PREFAB_NAME_AUDIO_VIEW);
        }

        public void OnTurn()
        {
            _audioView.Effect();
        }

        public void OnClick()
        {
            _audioView.Click();
        }

        public void OnEditState()
        {
            _audioView.OnEditStateSound();
        }

        public void OnCrashed()
        {
            _audioView.OnCrached();
        }
    }
}