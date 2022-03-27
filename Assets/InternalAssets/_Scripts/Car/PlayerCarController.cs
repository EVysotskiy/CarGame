using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum TypeTurn
{
    Left,
    Right
}
public class PlayerCarController : Window<PlayerCarView>
{
    private PlayerCarView _playerCarView;
    private const string PREFAB_PLAYER_NAME = "PlayerCar";
    public PlayerCarController(IContext context) : base(context)
    {
        Initialized();
    }

    private void Initialized()
    {
        _playerCarView = CreateView<PlayerCarView>(PREFAB_PLAYER_NAME);
    }

    public void Drive()
    {
        throw new System.NotImplementedException();
    }

    public void Stop()
    {
        throw new System.NotImplementedException();
    }

    public bool IsCrashed()
    {
        throw new System.NotImplementedException();
    }

    public void Turn (TypeTurn typeTurn)
    {
        switch (typeTurn)
        {
            case TypeTurn.Left:
                
                break;
            
            case TypeTurn.Right:
                
                break;
            
            default: throw new WarningException();
        }
    }
}