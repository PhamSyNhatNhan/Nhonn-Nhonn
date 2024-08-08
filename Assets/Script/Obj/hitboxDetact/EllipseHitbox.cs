using System.Collections.Generic;
using UnityEngine;

public class EllipseHitbox : Hitbox
{
    [SerializeField] private List<_EllipseHitbox> attackPoints;
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < attackPoints.Count; i++)
        {
            if(!attackPoints[i].IsShow) continue;
            
            Gizmos.color = i > 9 ? hitboxColors[9] : hitboxColors[i];
            DrawEllipse(attackPoints[i].AttackTransform.position, attackPoints[i].AttackSize);
        }
    }
    
    private void DrawEllipse(Vector3 position, Vector2 size)
    {
        int segments = 100;
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * 2.0f * Mathf.PI / segments;
            float x = Mathf.Cos(angle) * size.x / 2;
            float y = Mathf.Sin(angle) * size.y / 2;
            points[i] = position + new Vector3(x, y, 0);
        }

        for (int i = 0; i < segments; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }

    public override List<GameObject> detectObject(LayerMask enableDamage)
    {
        return detectObject(enableDamage, 0);
    }

    public override List<GameObject> detectObject(LayerMask enableDamage, int hitBox)
    {
        List<GameObject> listObject = new List<GameObject>();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPoints[hitBox].AttackTransform.position, Mathf.Max(attackPoints[hitBox].AttackSize.x, attackPoints[hitBox].AttackSize.y) / 2, enableDamage);
        
        foreach (Collider2D collider in detectedObjects)
        {
            if (IsWithinEllipse(collider.transform.position, attackPoints[hitBox].AttackTransform.position, attackPoints[hitBox].AttackSize))
            {
                listObject.Add(collider.gameObject);
            }
        }

        if (listObject.Count != 0)
        {
            listObject.Sort((a, b) => Vector2.Distance(transform.position, a.transform.position).CompareTo(Vector2.Distance(transform.position, b.transform.position)));
            return listObject;
        }

        return listObject;
    }

    private bool IsWithinEllipse(Vector3 point, Vector3 center, Vector2 size)
    {
        float dx = point.x - center.x;
        float dy = point.y - center.y;
        return (dx * dx) / (size.x * size.x / 4) + (dy * dy) / (size.y * size.y / 4) <= 1;
    }
}

[System.Serializable]
public class _EllipseHitbox
{
    [SerializeField] private bool isShow = true;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private Vector2 attackSize;

    public bool IsShow
    {
        get => isShow;
        set => isShow = value;
    }

    public Transform AttackTransform
    {
        get => attackTransform;
        set => attackTransform = value;
    }

    public Vector2 AttackSize
    {
        get => attackSize;
        set => attackSize = value;
    }
}
