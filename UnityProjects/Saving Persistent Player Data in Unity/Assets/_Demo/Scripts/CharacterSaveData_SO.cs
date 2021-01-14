using UnityEngine;

[CreateAssetMenu(fileName = "New Character Save Data", menuName = "Character/Data", order = 1)]
public class CharacterSaveData_SO : ScriptableObject
{
    [Header("Stats")]

    [SerializeField] private int currentHealth;

    [Header("Leveling")]

    [SerializeField] public int currentLevel = 1;
    [SerializeField] private int maxLevel = 30;
    [SerializeField] private int basisPoints = 200;
    [SerializeField] public int pointsTillNextLevel;

    [SerializeField] private float levelBuff = 0.1f; // 10% more xp for each level

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

    public void AggregateAttackPoints(int points)
    {
        pointsTillNextLevel -= points;

        if (pointsTillNextLevel <= 0)
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);

            pointsTillNextLevel += (int) (basisPoints * LevelMultiplier);
            
            Debug.Log("Level up. New level: " + currentLevel);
        }
    }

    private void OnEnable()
    {
        if (pointsTillNextLevel == 0)
        {
            pointsTillNextLevel = (int) (basisPoints * LevelMultiplier);
        }

        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
    }

    private void OnDisable()
    {
        if (key == "") key = name;

        var serializedJsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, serializedJsonData);
        PlayerPrefs.Save();
    }
}
