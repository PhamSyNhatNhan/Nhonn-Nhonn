using System.Collections.Generic;
using UnityEngine;

public class GuraAttack_2 : ProjectileObject
{
    private BoxHitbox hitbox;

    protected override void Awake()
    {
        base.Awake();
        hitbox = GetComponent<BoxHitbox>();
    }
    
    public override void SendDamage()
    {
        List<GameObject> Enemy = hitbox.detectObject(EnableDamage);

        for (int i = 0; i < Enemy.Count; i++)
        {
            Enemy[i].GetComponent<Stat>().TakeDamage(DamageType.Magic, damage[0], critRate, critDamage);
        } 
    }
}
