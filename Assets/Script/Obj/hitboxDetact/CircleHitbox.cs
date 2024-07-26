using System.Collections.Generic;
using UnityEngine;

public class CircleHitbox : Hitbox
{
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackTransform;
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRadius);
    }
    
    public override List<GameObject> detectObject(LayerMask enableDamage)
    {
        Collider2D[] DetectObject = Physics2D.OverlapCircleAll(attackTransform.position, attackRadius, enableDamage);

        List<GameObject> listObject = new List<GameObject>();

        foreach (Collider2D collider in DetectObject)
        {
            listObject.Add(collider.gameObject);
        }

        if (listObject.Count != 0)
        {
            listObject.Sort((a, b) => Vector2.Distance(transform.position, a.transform.position).CompareTo(Vector2.Distance(transform.position, b.transform.position)));
            return listObject;
        }
        
        return listObject;
    }
    
}
