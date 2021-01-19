using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "BattleCharacter/Stats", order = 1)]
public class BattleCharacterStats_SO : ScriptableObject
{
    public delegate void BattleCharacterDeath();
    public event BattleCharacterDeath OnBattleCharacterDeath;

    public string characterName;

    public int maxHealth;
    public int currentHealth;

    public int maxMana;
    public int currentMana;

    public int baseDamage;
    public int currentDamage;

    public int baseResistance;
    public int currentResistance;

    public float secondsUntilMeterFull;

    public BattleCharacterSaveData_SO battleCharacterData;

    public string CharacterName => battleCharacterData != null ? battleCharacterData.characterName : characterName;

    public int MaxHealth =>
        battleCharacterData != null
            ? (int) (maxHealth * battleCharacterData.LevelMultiplier)
            : maxHealth;

    public int MaxMana =>
        battleCharacterData != null
            ? (int) (maxMana * battleCharacterData.LevelMultiplier)
            : maxMana;

    public int CurrentHealth
    {
        get => battleCharacterData != null ? battleCharacterData.CurrentHealth : currentHealth;
        set
        {
            if (battleCharacterData != null)
            {
                battleCharacterData.CurrentHealth = value;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    public void Init()
    {
        if (CurrentHealth <= 0)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void AggregateExperiencePoints(int points)
    {
        if (battleCharacterData != null) { battleCharacterData.AggregateExperiencePoints(points);}
    }

    public float LevelMultiplier => battleCharacterData != null ? battleCharacterData.LevelMultiplier : 1;

    public void ApplyHeal(int heal)
    {
        if (CurrentHealth + heal > MaxHealth) CurrentHealth = MaxHealth;
        else CurrentHealth += heal;
    }

    public void ApplyMana(int mana)
    {
        if (currentMana + mana > MaxMana) currentMana = MaxMana;
        else currentMana += mana;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0) Death();
    }

    public void TakeMana(int mana)
    {
        currentMana -= mana;

        if (currentMana < 0) currentMana = 0;
    }

    private void Death()
    {
        Debug.LogWarning("This battle character " + name + " has died!");
        OnBattleCharacterDeath?.Invoke();
    }
}