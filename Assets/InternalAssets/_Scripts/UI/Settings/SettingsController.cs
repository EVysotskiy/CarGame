namespace InternalAssets._Scripts.UI.Settings
{
    public class SettingsController:Window<SettingsView>
    {
        private SettingsView _settingsView;
        private const string PREFAB_SETTINGS_NAME = "settings/Settings";
        public SettingsController(IContext context) : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            _settingsView = CreateView<SettingsView>(PREFAB_SETTINGS_NAME);
        }
    }
}