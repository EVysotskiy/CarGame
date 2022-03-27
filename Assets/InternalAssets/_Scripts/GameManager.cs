using UnityEngine;

public enum GameState { INTRO, MENU,PLAY }
public delegate void OnStateChangeHandler();
public class GameManager : MonoBehaviour
{
    protected GameManager(){}
    private static GameManager _instance = null;
    private Context _context;
    public event OnStateChangeHandler OnStateChange;
    private MenuController _menuController;
    private PlayerCarController _playerCarController;
    public GameState gameState { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if (GameManager._instance == null)
            {
                DontDestroyOnLoad(GameManager._instance);
                GameManager._instance = new GameManager();
            }

            return GameManager._instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        CreateContext();
        InitializedMenu();
        InitializedPlayer();

    }

    private void CreateContext()
    {
        _context = new Context(this);
    }
    private void InitializedMenu ()
    {
        _menuController = new MenuController(_context);
    }

    private void InitializedPlayer()
    {
        _playerCarController = new PlayerCarController(_context);
    }
    public void SetGameState(GameState state)
    {
        gameState = state;
        OnStateChange?.Invoke();
    }
    
    public void OnApplicationQuit(){
        GameManager._instance = null;
    }
}
