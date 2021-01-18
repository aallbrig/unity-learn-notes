using UnityEngine;

public class BattleCharacterStats : MonoBehaviour, IBattleCharacter
{
    public delegate void BattleCharacterDeath();
    public event BattleCharacterDeath OnBattleCharacterDeath;

    public BattleCharacterStats_SO characterDefinitionTemplate;
    public BattleCharacterStats_SO characterDefinition;

    public int GetHealth()
    {
        return characterDefinition.CurrentHealth;
    }

    public void ExecuteAttack(GameObject attacker, GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public void ApplyHeal(int heal)
    {
        characterDefinition.ApplyHeal(heal);
    }

    public void ApplyMana(int mana)
    {
        characterDefinition.ApplyMana(mana);
    }

    public void TakeDamage(int damage)
    {
        characterDefinition.TakeDamage(damage);
    }

    public void TakeMana(int mana)
    {
        characterDefinition.TakeMana(mana);
    }

    private void HandleBattleCharacterDeath()
    {
        OnBattleCharacterDeath?.Invoke();
    }

    private void Awake()
    {
        if (characterDefinitionTemplate != null) characterDefinition = Instantiate(characterDefinitionTemplate);

        characterDefinition.Init();
    }

    private void Start()
    {
        
        // Subscribe and bubble up death event
        characterDefinition.OnBattleCharacterDeath += HandleBattleCharacterDeath;
    }

    private void OnDestroy()
    {
        characterDefinition.OnBattleCharacterDeath -= HandleBattleCharacterDeath;
    }
}
