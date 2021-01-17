using UnityEngine;

public class RandomBattleManager : Singleton<RandomBattleManager>
{
    public delegate void RandomBattleTriggered();
    public static event RandomBattleTriggered OnRandomBattleTriggered;
    
    public Transform target;
    private Vector3 _lastEvaluatedPosition;
    private const float EncounterDistance = 5;
    private float _randomEncounterChance = 0.20f;
    private float _increaseEncounterChangeBy = 0.05f;

    private void TriggerRandomEncounter()
    {
        Debug.LogWarning("Random encounter triggered!");
        OnRandomBattleTriggered?.Invoke();
    }

    private void MaybeTriggerRandomEncounter()
    {
        var encounterRoll = Random.Range(0f, 1f);
        if (encounterRoll <= _randomEncounterChance)
        {
            TriggerRandomEncounter();
        }
        else
        {
            _randomEncounterChance += _increaseEncounterChangeBy;
        }
    }

    private void Start()
    {
        _lastEvaluatedPosition = target.transform.position;
    }

    private void Update()
    {
        // Every X distance, evaluate if player encounters a random battle
        if (Vector3.Distance(_lastEvaluatedPosition, target.transform.position) >= EncounterDistance)
        {
            _lastEvaluatedPosition = target.transform.position;
            MaybeTriggerRandomEncounter();
        }
    }
}