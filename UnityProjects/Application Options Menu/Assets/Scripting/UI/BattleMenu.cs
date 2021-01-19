using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenu : MonoBehaviour, IBattleMeterTick, IBattleCharReadyToAct, IBattleCharacterHasActed
{
    public GameObject playerStatsContainer;
    public GameObject playerStatsUI;
    public GameObject playerActionPanel;
    public GameObject playerTargetSelectionPanel;
    public GameObject targetSelectionButton;

    // TODO: hard coding this action is a hack
    public Button attackButton;
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

    private void ShowPlayerTargetSelection(GameObject battleChar)
    {
        // clear previous target list
        foreach (Transform child in playerTargetSelectionPanel.transform) GameObject.Destroy(child.gameObject);

        // get list of enemies
        var gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var go in gameObjects)
        {
            var enemyCharacterStats = go.GetComponent<BattleCharacterStats>();
            if (enemyCharacterStats.characterDefinition.CurrentHealth > 0)
            {
                var targetButton = Instantiate(targetSelectionButton, playerTargetSelectionPanel.transform);
                var text = targetButton.GetComponentInChildren<TextMeshProUGUI>();
                text.text = go.GetComponent<BattleCharacterStats>().GetCharacterName();
                targetButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    var cmd = new AttackBattleCommand(
                        battleChar.GetComponent<BattleCharacterStats>(), 
                        go.GetComponent<BattleCharacterStats>()
                    );
                    cmd.OnBattleCommandComplete += () => BattleManager.Instance.NotifyBattleCharacterHasActed(battleChar);
                    // player gets to cheat
                    BattleCommandManager.Instance.AddToFront(cmd);

                    playerTargetSelectionPanel.SetActive(false);
                    playerActionPanel.SetActive(false);
                });
            }
        }
        playerTargetSelectionPanel.SetActive(true);
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
        attackButton.interactable = true;
        playerActionPanel.SetActive(true);
        // get available actions from battle character game object
        // TODO: generate list of actions instead of hard coding!
        attackButton.onClick.AddListener(() =>
        {
            Debug.Log("attack button clicked!");
            // TODO: once you commit to an action, there is no going back. Is this really desired?
            attackButton.interactable = false;
            attackButton.onClick.RemoveAllListeners();
            ShowPlayerTargetSelection(battleChar);
        });
    }

    private void HandlePlayerCharacterHasActed(GameObject battleChar)
    {
        playerTargetSelectionPanel.SetActive(true);
        playerActionPanel.SetActive(false);
        // TODO: reset action container (if generating actions)
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

    public void NotifyBattleCharacterHasActed(GameObject battleChar)
    {
        if (_playerCharacters.Contains(battleChar)) HandlePlayerCharacterHasActed(battleChar);
    }
}
