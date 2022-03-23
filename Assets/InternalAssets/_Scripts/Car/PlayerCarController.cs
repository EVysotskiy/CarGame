using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : ICar
{
    private PlayerCarView _playerCarView;
    private const string PREFAB_PLAYER_NAME = "PlayerCar";
    public PlayerCarController()
    {
        Initialized();
    }

    private void Initialized()
    {
        _playerCarView = Instantiate(PrefabManager.PrefabManager.GetPrefabByName(PREFAB_PLAYER_NAME)).GetComponent<PlayerCarView>();
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