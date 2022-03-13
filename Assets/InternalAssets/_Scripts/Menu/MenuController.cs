using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : Window
{
    private MenuView _menuView { get; }
    public const string UI_PREFAB_MENU_NAME = "MenuView";
    
    public MenuController(MenuView menuView)
    {
        _menuView = menuView;
        _menuView.OnStartPlay += OnStartPlay;
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
