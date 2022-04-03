using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterActionType
{
    Runs,
    Stands,
}

public class CharacterController : Window<CharacterView>
{
    private const string PREFAB_CHARACTER_NAME = "Charecter";
    public CharacterActionType CharacterActionType { get; private set; }
    private float _speed;
    private List<CharacterView> _characterViews;

    public CharacterController(IContext context) : base(context)
    {
        Initialized();
        GameManager.Instance.TouchHandler.SubscribeTouchScreen(OnTouchScreen);
    }

    private void Initialized()
    {
        _characterViews = new List<CharacterView>();
        for (int i = 0; i < 100000; i++)
        {
            _characterViews.Add(CreateView<CharacterView>(PREFAB_CHARACTER_NAME).GetComponent<CharacterView>());
        }
        SetActionType(CharacterActionType.Stands);
    }
    private void SetActionType(CharacterActionType actionType)
    {
        CharacterActionType = actionType;
    }

    public void Run()
    {
        SetActionType(CharacterActionType.Runs);
        RunOrStandAll(CharacterActionType);
    }

    public void Stand()
    {
        SetActionType(CharacterActionType.Stands);
        RunOrStandAll(CharacterActionType);
    }

    private void OnTouchScreen(InputAction.CallbackContext callbackContext)
    {
        if (GameManager.Instance.gameState is GameState.PLAY == false) { return;}
        
        if (CharacterActionType is CharacterActionType.Runs)
        {
            Stand();
            return;
        }
        
        Run();
    }
    private void RunOrStandAll(CharacterActionType actionType)
    {
        _characterViews.ForEach(player =>
        {
            if (actionType is CharacterActionType.Runs)
            {
                if (player == null)
                {
                    return;
                }
                player.Run();
                return;
            }
            player.Stand();
        });
    }

    ~CharacterController()
    {
        GameManager.Instance.TouchHandler.UnsubscribeTouchScreen(OnTouchScreen);
    }
}
