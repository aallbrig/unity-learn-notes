using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour, IBattleMeterTick
{
    private List<GameObject> _playerCharacters = new List<GameObject>();

    private void Start()
    {
        _playerCharacters.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        BattleEventBroker.Instance.SubscribeToBattleMeterTick(this);
    }

    private void HandlePlayerBattleMeterTick(GameObject battleChar)
    {
        Debug.Log("Battle meter tick for " + battleChar.name);
    }

    public void NotifyBattleMeterTick(GameObject battleChar, float tickValue)
    {
        if (_playerCharacters.Contains(battleChar)) HandlePlayerBattleMeterTick(battleChar);
    }
}
