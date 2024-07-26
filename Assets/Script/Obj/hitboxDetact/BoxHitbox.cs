using System.Collections.Generic;
using UnityEngine;

public class BoxHitbox : Hitbox
{
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private Transform attackTransform;
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackTransform.position, attackSize);
    }
    
    public override List<GameObject> detectObject(LayerMask enableDamage)
    {
        Collider2D[] DetectObject = Physics2D.OverlapBoxAll(attackTransform.position, attackSize, 0, enableDamage);


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
