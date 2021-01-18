using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Battle Character Save Data", menuName = "BattleCharacter/SaveData", order = 1)]
public class BattleCharacterSaveData_SO : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private int currentHealth;

    [Header("Leveling")]
    
    [SerializeField] public int currentLevel;
    [SerializeField] private int maxLevel = 30;
    [SerializeField] private int basisPoints = 200;
    [SerializeField] public int pointsTilNextLevel;

    [SerializeField] private float levelBuff = 0.1f; // require 10% more experience for each level

    [Header("Save Data")]
    [SerializeField] private string key;

    public float LevelMultiplier
    {
        get { return 1 + (currentLevel - 1) * levelBuff; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public void AggregateExperiencePoints(int points)
    {
        pointsTilNextLevel -= points;

        if (pointsTilNextLevel <= 0)
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);

            pointsTilNextLevel += (int) (basisPoints * LevelMultiplier);
        }
    }

    private void OnEnable()
    {
        if (pointsTilNextLevel == 0) pointsTilNextLevel = (int) (basisPoints * LevelMultiplier);
        
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
    }

    private void OnDisable()
    {
        if (key == "") key = name;

        PlayerPrefs.SetString(key, JsonUtility.ToJson(this));
        PlayerPrefs.Save();
    }
}
