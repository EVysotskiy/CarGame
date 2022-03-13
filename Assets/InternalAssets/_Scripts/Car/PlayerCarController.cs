using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : ICar
{
    private PlayerCarView _playerCarView;

    public PlayerCarController(PlayerCarView playerCarView)
    {

    }
    public void Drive()
    {
        throw new System.NotImplementedException();
    }

    public bool IsCrashed()
    {
        throw new System.NotImplementedException();
    }
}