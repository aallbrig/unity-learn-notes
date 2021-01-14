using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedDebug  : MonoBehaviour, IAttackable 
{
	public int OnAttack(GameObject attacker, Attack attack)
	{
		if(attack.IsCritical)
			Debug.Log("CRITICAL DAMAGE!!");
		
		Debug.LogFormat("{0} attacked {1} for {2} damage", attacker.name, name, attack.Damage);

        return attack.Damage;
	}
}
