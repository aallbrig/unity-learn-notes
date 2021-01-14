using UnityEngine;

public class AOE : MonoBehaviour
{
    public float TimeOnField;

    public int Fire(AOESpell definition, GameObject caster, int layer, float multiplier)
    {
        var collidedObjects = Physics.OverlapSphere(transform.position, definition.radius);
        int attackPoints = 0;

        foreach (var collision in collidedObjects)
        {
            var go = collision.gameObject;

            if (Physics.GetIgnoreLayerCollision(layer, go.layer))
                break;

            var attack = definition.CreateAttack(caster.GetComponent<CharacterStats>(), go.GetComponent<CharacterStats>(), multiplier);

            var attackables = go.GetComponents(typeof(IAttackable));

            foreach (var attackable in attackables)
            {
                ((IAttackable)attackable).OnAttack(caster, attack);
                attackPoints += attack.Damage;
            }
        }

        Destroy(gameObject, TimeOnField);

        return attackPoints;
    }
}