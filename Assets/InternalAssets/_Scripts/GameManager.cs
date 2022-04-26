using System.Collections.Generic;
using InternalAssets._Scripts.Car;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum GameState { INTRO, MENU,PLAY }
public delegate void OnStateChangeHandler();
public class GameManager : MonoBehaviour
{
    [SerializeField][Space]
    private List<CarData> _carDatas;
    protected GameManager(){}
    private static GameManager _instance = null;
    private Context _context;
    public event OnStateChangeHandler OnStateChange;
    private CarController _carController;
    private CharacterController _characterController;
    private MenuController _menuController;
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
        InitializedCar();
    }
    
    private void CreateContext()
    {
        _context = new Context(this);
    }
    private void InitializedMenu()
    {
        _menuController = new MenuController(_context);
    }

    private void InitializedCar()
    {
        _carController = new CarController(_context, _carDatas);
    }
    
    public void SetGameState(GameState state)
    {
        gameState = state;
        OnStateChange?.Invoke();

        switch (state)
        {
            case GameState.PLAY:
                Debug.LogFormat($"<b><color=#00ff00>Start game session.</color></b>");
               // OnStartPlay?.Invoke();
                break;
        }
    }
    
    public void OnApplicationQuit(){
        GameManager._instance = null;
    }

    private void OnTouchScreen(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("OnTouchScreen");
    }

    private void OnStopedTouchScreen(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("OnStopedTouchScreen");
    }
}
