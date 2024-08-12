using System.Collections.Generic;
using UnityEngine;

public class GuraAttack : ProjectileObject
{
    private Hitbox hitbox;

    protected override void Awake()
    {
        base.Awake();
        hitbox = GetComponent<Hitbox>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
       
        EventManager.Player.OnPlayerAttack.Get("endAttack").Invoke(this, null);
    }

    public override void SendDamage()
    {
        List<GameObject> Enemy = hitbox.detectObject(EnableDamage);

        for (int i = 0; i < Enemy.Count; i++)
        {
            Enemy[i].GetComponent<Stat>().TakeDamage(type, damage[0], critRate, critDamage);
        } 
    }
}
