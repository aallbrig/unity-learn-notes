using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour, IBattleMeterTick, IBattleCharReadyToAct
{
    public GameObject playerStatsContainer;
    public GameObject playerStatsUI;
    public GameObject playerActionPanel;
    private bool isDisplayingActionPanel;
    private readonly List<GameObject> _playerCharacters = new List<GameObject>();
    private readonly Dictionary<GameObject, PlayerStatsUI> _playerCharToStatsUI = new Dictionary<GameObject, PlayerStatsUI>();

    private void SetupPlayerStatsUI(GameObject battleChar)
    {
        var playerStatsUI = Instantiate(this.playerStatsUI, playerStatsContainer.transform);
        var playerStatsUIScript = playerStatsUI.GetComponent<PlayerStatsUI>();
        playerStatsUIScript.SetBattleCharacter(battleChar.GetComponent<BattleCharacterStats>());

        _playerCharToStatsUI.Add(battleChar, playerStatsUIScript);
    }

    private void HandlePlayerBattleMeterTick(GameObject battleChar, float meterValue)
    {
        if (_playerCharToStatsUI.ContainsKey(battleChar))
        {
            var statsUI = _playerCharToStatsUI[battleChar];
            statsUI.UpdateBattleMeter(meterValue);
        }
    }

    private void HandlePlayerCharReadyToAct(GameObject battleChar)
    {
        playerActionPanel.SetActive(true);
    }

    private void Start()
    {
        BattleEventBroker.Instance.SubscribeToBattleMeterTick(this);
        BattleEventBroker.Instance.SubscribeToBattleCharReadyToAct(this);

        _playerCharacters.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        _playerCharacters.ForEach(SetupPlayerStatsUI);
    }

    private void OnDestroy()
    {
        BattleEventBroker.Instance.UnsubscribeToBattleMeterTick(this);
    }

    public void NotifyBattleMeterTick(GameObject battleChar, float tickValue)
    {
        if (_playerCharacters.Contains(battleChar)) HandlePlayerBattleMeterTick(battleChar, tickValue);
    }

    public void NotifyBattleCharReadyToAct(GameObject battleChar)
    {
        if (_playerCharacters.Contains(battleChar)) HandlePlayerCharReadyToAct(battleChar);
    }
}
