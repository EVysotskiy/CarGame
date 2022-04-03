using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum GameState { INTRO, MENU,PLAY }
public delegate void OnStateChangeHandler();
public class GameManager : MonoBehaviour
{
    protected GameManager(){}
    private static GameManager _instance = null;
    private Context _context;
    public event OnStateChangeHandler OnStateChange;
    public event OnStartPlayHandler OnStartPlay;
    private MenuController _menuController;
    private PlayerCarController _playerCarController;
    private CharacterController _characterController; 
    public TouchHandler TouchHandler { get; private set; }
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
        InitializedTouchHandler();
        InitializedMenu();
        InitializedPlayer();
    }

    private void InitializedTouchHandler()
    {
        TouchHandler = gameObject.AddComponent<TouchHandler>();
        TouchHandler.SubscribeTouchScreen(OnTouchScreen);
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
        _characterController = new CharacterController(_context);
        //_playerCarController = new PlayerCarController(_context,OnStartPlay);
    }
    public void SetGameState(GameState state)
    {
        gameState = state;
        OnStateChange?.Invoke();

        switch (state)
        {
            case GameState.PLAY:
                Debug.LogFormat($"<b><color=#00ff00>Start game session.</color></b>");
                OnStartPlay?.Invoke();
                break;
        }
    }
    
    public void OnApplicationQuit(){
        GameManager._instance = null;
    }

    private void OnTouchScreen(InputAction.CallbackContext callbackContext)
    {
        
    }
}
