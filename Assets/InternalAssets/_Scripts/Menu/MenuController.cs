public class MenuController : Window<MenuView>
{
    private MenuView _menuView;
    public const string UI_PREFAB_MENU_NAME = "MenuView";
    
    public MenuController(IContext context):base(context)
    {
        Initialized();
        _menuView.OnStartPlay += OnStartPlay;
    }

    private void Initialized()
    {
        _menuView = CreateView<MenuView>(UI_PREFAB_MENU_NAME);
    }
    private void OnStartPlay()
    {
        GameManager.Instance.SetGameState(GameState.PLAY);
        CloseMenuPanel();
    }
    
    private void CloseMenuPanel()
    {
        _menuView.SetStateCanvasMenu(false);
    }
}