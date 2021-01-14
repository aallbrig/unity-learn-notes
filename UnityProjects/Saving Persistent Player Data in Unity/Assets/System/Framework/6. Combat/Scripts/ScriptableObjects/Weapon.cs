using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")]
public class Weapon : AttackDefinition
{
	public Rigidbody weaponPreb;
	
	public int ExecuteAttack(GameObject attacker, GameObject target, float multiplier)
	{
		if (target == null)
			return 0;
        
		// check if target is in range of player
		if(Vector3.Distance(attacker.transform.position, target.transform.position) > range)
			return 0;
        
		// check if target is in front of the player
		if (!attacker.transform.IsFacingTarget(target.transform))
			return 0;
        
		// at this point the attack will connect
		var attack = CreateAttack(attacker.GetComponent<CharacterStats>(), target.GetComponent<CharacterStats>(), multiplier);
        
		var attackables = target.GetComponentsInChildren(typeof(IAttackable));

		foreach (var a in attackables)
		{
			((IAttackable)a).OnAttack(attacker.gameObject, attack);
		}

        return attack.Damage;
	}
}
