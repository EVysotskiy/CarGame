using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { INTRO, MENU,PLAY }
public delegate void OnStateChangeHandler();
public class GameManager : MonoBehaviour
{
    protected GameManager(){}
    private static GameManager _instance = null;
    public event OnStateChangeHandler OnStateChange;
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
        InitializedMenu();

    }

    private void InitializedMenu ()
    {
        _menuController = new MenuController(
            Instantiate(PrefabManager.PrefabManager.GetPrefabByName(MenuController.UI_PREFAB_MENU_NAME))
                .GetComponent<MenuView>());
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
