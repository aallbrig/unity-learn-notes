﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public struct BattleInfo
{
    public BattleInfo(BattleCharacterStats stats, float battleMeterVal, float battleMeterTickRate, bool canAct = true, bool isDead = false)
    {
        this.stats = stats;
        this.battleMeterVal = battleMeterVal;
        this.battleMeterTickRate = battleMeterTickRate;
        this.canAct = canAct;
        this.isDead = isDead;
    }
    
    public BattleCharacterStats stats;
    // 1.0 == ready to act
    public float battleMeterVal;
    public readonly float battleMeterTickRate;
    public bool canAct;
    public bool isDead;
}

public class BattleManager : Singleton<BattleManager>, IBattleCharReadyToAct, IBattleCharacterHasActed, IBattleCharacterDeath, IBattleCommandStart, IBattleCommandComplete
{
    public delegate void BattleMeterTickEvent(GameObject battleChar, float battleMeterValue);
    public event BattleMeterTickEvent OnBattleMeterTickEvent;
    public delegate void BattleCharacterAction(GameObject battleChar);
    public event BattleCharacterAction OnBattleCharacterReadyToActEvent;
    public event BattleCharacterAction OnBattleCharacterHasActedEvent;

    public delegate void BattleLostEvent();
    public static event BattleLostEvent OnBattleLostEvent;
    public delegate void BattleVictoryEvent();
    public static event BattleVictoryEvent OnBattleVictoryEvent;

    public List<GameObject> possibleEnemies;
    public List<Transform> possibleEnemyLocations;

    private bool stopBattleMeterTicks;
    [SerializeField] private BattleCharacterStats[] battleCharacters;
    [SerializeField] private List<GameObject> _battleCharacters = new List<GameObject>();
    [SerializeField] private List<GameObject> _playerCharacters = new List<GameObject>();
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    
    private readonly Dictionary<GameObject, BattleInfo> _battleCharToInfo = new Dictionary<GameObject, BattleInfo>();

    private IEnumerator _battleMeterTickCoroutine;
    private const float BattleMeterTickRate = 0.5f;

    private void GenerateMonsters()
    {
        // spawn at least 1 enemy
        var enemiesToSpawn = Random.Range(1, possibleEnemyLocations.Count);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            var enemy = possibleEnemies[Random.Range(0, possibleEnemies.Count)];
            var spawnPosition = possibleEnemyLocations[Random.Range(0, possibleEnemyLocations.Count)];
            // remove so it can't be used by another enemy
            possibleEnemyLocations.Remove(spawnPosition);
            Instantiate(enemy, spawnPosition.position, spawnPosition.rotation);
        }
    }

    private void HandleOnBattleCharacterHasActed(GameObject battleChar)
    {
        if (_battleCharToInfo.ContainsKey(battleChar))
        {
            var battleInfo = _battleCharToInfo[battleChar];
            battleInfo.battleMeterVal = 0;
            battleInfo.canAct = true;
            _battleCharToInfo[battleChar] = battleInfo;
        }
    }

    private void HandleOnBattleCharacterReadyToAct(GameObject battleChar)
    {
        if (battleChar.CompareTag("Enemy") && _enemies.Contains(battleChar))
        {
            var target = _playerCharacters[Random.Range(0, _playerCharacters.Count)];
            var cmd = new AttackBattleCommand(battleChar.GetComponent<BattleCharacterStats>(), target.GetComponent<BattleCharacterStats>());
            cmd.OnBattleCommandComplete += () => OnBattleCharacterHasActedEvent?.Invoke(battleChar);
            BattleCommandManager.Instance.Add(cmd);
        }
    }

    private void HandleBattleMeterTick(GameObject battleChar)
    {
        var battleInfo = _battleCharToInfo[battleChar];

        if (!battleInfo.isDead && battleInfo.canAct)
        {
            var battleMeterTick = battleInfo.battleMeterTickRate * BattleMeterTickRate;
            var newBattleMeterVal = battleInfo.battleMeterVal + battleMeterTick;
            
            if (newBattleMeterVal >= 1.0)
            {
                battleInfo.canAct = false;
                battleInfo.battleMeterVal = 1.0f;
                _battleCharToInfo[battleChar] = battleInfo;
                Debug.LogWarning("Battle char " + battleChar.name + " is ready to act!");

                OnBattleMeterTickEvent?.Invoke(battleChar, newBattleMeterVal);
                OnBattleCharacterReadyToActEvent?.Invoke(battleChar);
            }
            else
            {
                battleInfo.battleMeterVal = newBattleMeterVal;
                _battleCharToInfo[battleChar] = battleInfo;
                OnBattleMeterTickEvent?.Invoke(battleChar, newBattleMeterVal);
            }

        }
    }

    private IEnumerator BattleMeterTick()
    {
        while (true)
        {
            if (!stopBattleMeterTicks) _battleCharacters.ForEach(HandleBattleMeterTick);

            yield return new WaitForSeconds(BattleMeterTickRate);
        }
    }

    private void HandleAllPlayersSlain()
    {
        StopCoroutine(_battleMeterTickCoroutine);
        
        OnBattleLostEvent?.Invoke();
    }
    
    private void HandleAllEnemiesSlain()
    {
        StopCoroutine(_battleMeterTickCoroutine);
        
        OnBattleVictoryEvent?.Invoke();
    }
    
    private void CleanUpBattleCharacterInfo(GameObject battleChar)
    {
        _battleCharacters.Remove(battleChar);
        _battleCharToInfo.Remove(battleChar);

        if (battleChar.CompareTag("Enemy"))
        {
            _enemies.Remove(battleChar);

            if (_enemies.Count <= 0) HandleAllEnemiesSlain();
        }
        else
        {
            _playerCharacters.Remove(battleChar);

            if (_playerCharacters.Count <= 0) HandleAllPlayersSlain();
        }
    }
    
    private void HandleBattleCharacterDeath(GameObject battleChar)
    {
        CleanUpBattleCharacterInfo(battleChar);
    }

    private void Start()
    {
        GenerateMonsters();
        // Generate list of battle characters' game objects (easier to work with)
        battleCharacters = FindObjectsOfType<BattleCharacterStats>();
        foreach (var battleCharStats in battleCharacters)
        {
            var battleChar = battleCharStats.gameObject;
            _battleCharacters.Add(battleChar);

            if (battleChar.CompareTag("Player"))
                _playerCharacters.Add(battleChar);
            else if (battleChar.CompareTag("Enemy"))
                _enemies.Add(battleChar);

            var randomBattleMeterStartValue = Random.Range(0f, 0.5f);
            // TODO: division sucks, I wonder how I can calculate this using multiplication
            var battleMeterTickRate = 1 / battleCharStats.characterDefinition.secondsUntilMeterFull;

            _battleCharToInfo.Add(
                battleChar,
                new BattleInfo(battleCharStats, randomBattleMeterStartValue, battleMeterTickRate, true, false)
            );
        }

        BattleEventBroker.Instance.SubscribeToBattleCharReadyToAct(this);
        BattleEventBroker.Instance.SubscribeToBattleCharacterHasActed(this);
        BattleEventBroker.Instance.SubscribeToBattleCharacterDeath(this);
        BattleEventBroker.Instance.SubscribeToBattleCommandStart(this);
        BattleEventBroker.Instance.SubscribeToBattleCommandComplete(this);
        OnBattleLostEvent += () =>
        {
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(enemy);
        };
        OnBattleVictoryEvent += () =>
        {
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(enemy);
        };

        _battleMeterTickCoroutine = BattleMeterTick();
        StartCoroutine(_battleMeterTickCoroutine);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        StopCoroutine(_battleMeterTickCoroutine);

        BattleEventBroker.Instance.UnsubscribeToBattleCharReadyToAct(this);
        BattleEventBroker.Instance.UnsubscribeToBattleCharacterHasActed(this);
        BattleEventBroker.Instance.UnsubscribeToBattleCharacterDeath(this);
    }

    public void NotifyBattleCharReadyToAct(GameObject battleChar) =>
        HandleOnBattleCharacterReadyToAct(battleChar);

    public void NotifyBattleCharacterHasActed(GameObject battleChar) =>
        HandleOnBattleCharacterHasActed(battleChar);

    public void NotifyBattleCharacterDeath(GameObject battleChar) =>
        HandleBattleCharacterDeath(battleChar);

    public void NotifyBattleCommandStart()
    {
        stopBattleMeterTicks = true;
    }

    public void NotifyBattleCommandComplete()
    {
        stopBattleMeterTicks = false;
    }
}
