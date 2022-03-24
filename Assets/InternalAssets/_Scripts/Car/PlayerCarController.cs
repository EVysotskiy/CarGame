using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : ICar
{
    private PlayerCarView _playerCarView;
    private IContext _context;
    private const string PREFAB_PLAYER_NAME = "PlayerCar";
    public PlayerCarController(IContext context)
    {
        _context = context;
        Initialized();
    }

    private void Initialized()
    {
        _playerCarView = _context.InstanceView(PREFAB_PLAYER_NAME).GetComponent<PlayerCarView>();
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
}