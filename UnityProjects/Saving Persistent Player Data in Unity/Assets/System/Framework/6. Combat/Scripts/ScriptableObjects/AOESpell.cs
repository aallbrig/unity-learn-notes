using UnityEngine;

[CreateAssetMenu(fileName = "AOESpell.asset", menuName = "AOE Spell")]
public class AOESpell : AttackDefinition
{
    public AOE aoeObject;
    public float radius;
    
    public int Cast(GameObject caster, int layer, float multiplier)
    {
        return Cast(caster, caster.transform.position, layer, multiplier);
    }
    
    public int Cast(GameObject caster, Vector3 position, int layer, float multiplier)
    {
        AOE aoe = Instantiate(aoeObject, position, Quaternion.identity);
        return aoe.Fire(this, caster, layer, multiplier);
    }
}