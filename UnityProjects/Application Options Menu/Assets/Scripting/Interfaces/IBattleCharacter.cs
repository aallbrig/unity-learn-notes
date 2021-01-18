using UnityEngine;

public interface IBattleCharacter
{
    int GetHealth();
    void ExecuteAttack(GameObject attacker, GameObject target);
    void ApplyHeal(int heal);
    void ApplyMana(int mana);
    void TakeDamage(int damage);
    void TakeMana(int mana);
}