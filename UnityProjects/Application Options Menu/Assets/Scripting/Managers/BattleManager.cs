using System.Collections;
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
    public float battleMeterTickRate;
    public bool canAct;
    public bool isDead;
}
public class BattleManager : MonoBehaviour
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

    [SerializeField] private BattleCharacterStats[] battleCharacters;
    [SerializeField] private List<GameObject> _battleCharacters = new List<GameObject>();
    [SerializeField] private List<GameObject> _playerCharacters = new List<GameObject>();
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    
    private readonly Dictionary<GameObject, BattleInfo> _battleCharToInfo = new Dictionary<GameObject, BattleInfo>();

    private IEnumerator _battleMeterTickCoroutine;
    private const float BattleMeterTickRate = 0.2f;

    // Start everyone's ATB meter at 0
    // Command players and NPCs to initiate attacks
    // Once all players or enemies are defeated, show a rewards screen

    private void GenerateMonsters()
    {
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
        var battleInfo = _battleCharToInfo[battleChar];
        battleInfo.battleMeterVal = 0;
        battleInfo.canAct = true;
        _battleCharToInfo[battleChar] = battleInfo;
        Debug.Log(_battleCharToInfo[battleChar].canAct);
    }

    private void HandleOnBattleCharacterReadyToAct(GameObject battleChar)
    {
        if (battleChar.CompareTag("Enemy"))
        {
            var target = _playerCharacters[Random.Range(0, _playerCharacters.Count)];
            Debug.LogWarning("Enemy " + battleChar.name + " chooses to attack player " + target.name);
            OnBattleCharacterHasActedEvent?.Invoke(battleChar);
        }
    }

    private void HandleBattleMeterTick(GameObject battleChar)
    {
        var battleInfo = _battleCharToInfo[battleChar];

        if (!battleInfo.isDead && battleInfo.canAct)
        {
            // TODO: division sucks for performance
            var battleMeterTick = battleInfo.battleMeterTickRate * BattleMeterTickRate;
            var newBattleMeterVal = battleInfo.battleMeterVal + battleMeterTick;
            
            if (newBattleMeterVal >= 1.0)
            {
                battleInfo.canAct = false;
                _battleCharToInfo[battleChar] = battleInfo;
                Debug.LogWarning("Battle char " + battleChar.name + " is ready to act!");
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
            _battleCharacters.ForEach(HandleBattleMeterTick);
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
    
    private BattleCharacterStats.BattleCharacterDeath HandleBattleCharacterDeath(GameObject battleChar)
    {
        return () => CleanUpBattleCharacterInfo(battleChar);
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

            battleCharStats.OnBattleCharacterDeath += HandleBattleCharacterDeath(battleChar);
        }

        OnBattleCharacterReadyToActEvent += HandleOnBattleCharacterReadyToAct;
        OnBattleCharacterHasActedEvent += HandleOnBattleCharacterHasActed;

        _battleMeterTickCoroutine = BattleMeterTick();
        StartCoroutine(_battleMeterTickCoroutine);
    }

    private void OnDestroy()
    {
        OnBattleCharacterReadyToActEvent -= HandleOnBattleCharacterReadyToAct;
    }
}
