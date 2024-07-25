using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraBurstEnd : SkillObject
{
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackTransform;

    public override void SendDamage()
    {
        Collider2D[] DetectObject = Physics2D.OverlapCircleAll(attackTransform.position, attackRadius, base.enableDamage);

        List<GameObject> Enemy = new List<GameObject>();

        foreach (Collider2D collider in DetectObject)
        {
            Enemy.Add(collider.gameObject);
        }

        Enemy.Sort((a, b) => Vector2.Distance(transform.position, a.transform.position).CompareTo(Vector2.Distance(transform.position, b.transform.position)));

        if (Enemy.Count != 0)
        {
            Enemy[0].GetComponent<Stat>().TakeDamage(DamageType.Magic, damage[0], critRate, critDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRadius);
    }
}
