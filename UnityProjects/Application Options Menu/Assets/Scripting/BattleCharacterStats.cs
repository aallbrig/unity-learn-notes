using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BattleCharacterStats : MonoBehaviour, IBattleCharacter
{
    public delegate void BattleCharacterDeath(GameObject battleChar);
    public static event BattleCharacterDeath OnBattleCharacterDeath;
    public delegate void BattleCharacterAttackComplete();
    public event BattleCharacterAttackComplete OnBattleCharacterAttackComplete;

    public BattleCharacterStats_SO characterDefinitionTemplate;
    public BattleCharacterStats_SO characterDefinition;

    private ToonRTSAnimationController _animationController;

    public string GetCharacterName()
    {
        return characterDefinition.CharacterName;
    }
    public int GetHealth()
    {
        return characterDefinition.CurrentHealth;
    }

    public int GetMaxHealth()
    {
        return characterDefinition.MaxHealth;
    }

    private IEnumerator AgentReachedDestination(NavMeshAgent agent, Action callback)
    {
        while (true)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance + 3)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Done
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.25f);
        }

        callback?.Invoke();
    }

    public void ExecuteAttack(BattleCharacterStats target)
    {
        if (target.characterDefinition.CurrentHealth <= 0)
        {
            OnBattleCharacterAttackComplete?.Invoke();
            return;
        }

        var startingPosition = transform.position;
        var startingRotation = transform.rotation;
        var agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 4;
        agent.SetDestination(target.transform.position);

        StartCoroutine(AgentReachedDestination(agent, () =>
        {
            agent.stoppingDistance = 0;
            agent.SetDestination(startingPosition);
            _animationController.AttackAnimation();
            target.TakeDamage(characterDefinition.baseDamage);
            StartCoroutine(AgentReachedDestination(agent, () =>
            {
                transform.rotation = startingRotation;
                OnBattleCharacterAttackComplete?.Invoke();
            }));
        }));
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
        _animationController.TakeDamageAnimation();
        characterDefinition.TakeDamage(damage);
    }

    public void TakeMana(int mana)
    {
        characterDefinition.TakeMana(mana);
    }

    private void HandleBattleCharacterDeath()
    {
        _animationController.Die();
        OnBattleCharacterDeath?.Invoke(gameObject);
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
        _animationController = GetComponent<ToonRTSAnimationController>();
    }

    private void OnDestroy()
    {
        characterDefinition.OnBattleCharacterDeath -= HandleBattleCharacterDeath;
        StopAllCoroutines();
    }
}
