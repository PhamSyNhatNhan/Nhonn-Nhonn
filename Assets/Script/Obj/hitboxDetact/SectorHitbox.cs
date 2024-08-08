using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SectorHitbox : Hitbox
{
    [SerializeField] private List<_SectorHitbox> attackPoints;
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < attackPoints.Count; i++)
        {
            if(!attackPoints[i].IsShow) continue;
            
            Gizmos.color = i > 9 ? hitboxColors[9] : hitboxColors[i];
            DrawSector(attackPoints[i].AttackTransform.position, attackPoints[i].InnerRadius, attackPoints[i].OuterRadius, attackPoints[i].AttackAngles, attackPoints[i].AttackDirections);
        }
    }

    private void DrawSector(Vector3 position, float innerRadius, float outerRadius, float angle, float direction)
    {
        int segments = 50; 
        float halfAngle = angle / 2;

        Vector3 prevInnerPoint = position + new Vector3(Mathf.Cos((direction - halfAngle) * Mathf.Deg2Rad) * innerRadius, Mathf.Sin((direction - halfAngle) * Mathf.Deg2Rad) * innerRadius, 0);
        Vector3 prevOuterPoint = position + new Vector3(Mathf.Cos((direction - halfAngle) * Mathf.Deg2Rad) * outerRadius, Mathf.Sin((direction - halfAngle) * Mathf.Deg2Rad) * outerRadius, 0);

        Gizmos.DrawLine(prevInnerPoint, prevOuterPoint);
        
        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = direction - halfAngle + (i / (float)segments) * angle;
            float rad = currentAngle * Mathf.Deg2Rad;

            Vector3 innerPoint = position + new Vector3(Mathf.Cos(rad) * innerRadius, Mathf.Sin(rad) * innerRadius, 0);
            Vector3 outerPoint = position + new Vector3(Mathf.Cos(rad) * outerRadius, Mathf.Sin(rad) * outerRadius, 0);

            Gizmos.DrawLine(prevInnerPoint, innerPoint);
            Gizmos.DrawLine(prevOuterPoint, outerPoint);

            prevInnerPoint = innerPoint;
            prevOuterPoint = outerPoint;
        }

        Gizmos.DrawLine(position + new Vector3(Mathf.Cos((direction + halfAngle) * Mathf.Deg2Rad) * innerRadius, Mathf.Sin((direction + halfAngle) * Mathf.Deg2Rad) * innerRadius, 0),
                        position + new Vector3(Mathf.Cos((direction + halfAngle) * Mathf.Deg2Rad) * outerRadius, Mathf.Sin((direction + halfAngle) * Mathf.Deg2Rad) * outerRadius, 0));
    }

    public override List<GameObject> detectObject(LayerMask enableDamage)
    {
        return detectObject(enableDamage, 0);
    }

    public override List<GameObject> detectObject(LayerMask enableDamage, int hitBox)
    {
        List<GameObject> listObject = new List<GameObject>();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPoints[hitBox].AttackTransform.position, attackPoints[hitBox].OuterRadius, enableDamage);

        Vector2 origin = attackPoints[hitBox].AttackTransform.position;
        float innerRadiusSq = attackPoints[hitBox].InnerRadius * attackPoints[hitBox].InnerRadius; 
        float outerRadiusSq = attackPoints[hitBox].OuterRadius * attackPoints[hitBox].OuterRadius; 
        float halfAngle = attackPoints[hitBox].AttackAngles / 2;
        float directionRad = attackPoints[hitBox].AttackDirections * Mathf.Deg2Rad;
        
        Vector2 mainDirection = new Vector2(Mathf.Cos(directionRad), Mathf.Sin(directionRad));

        foreach (Collider2D collider in detectedObjects)
        {
            Vector2 directionToCollider = (collider.transform.position - (Vector3)origin).normalized;
            float distanceSq = (collider.transform.position - (Vector3)origin).sqrMagnitude; 
            float angleToCollider = Vector2.Angle(mainDirection, directionToCollider);
            
            if (distanceSq >= innerRadiusSq && distanceSq <= outerRadiusSq && angleToCollider <= halfAngle)
            {
                listObject.Add(collider.gameObject);
            }
        }

        if (listObject.Count != 0)
        {
            listObject.Sort((a, b) => Vector2.Distance(transform.position, a.transform.position).CompareTo(Vector2.Distance(transform.position, b.transform.position)));
        }

        return listObject;
    }
}

[System.Serializable]
public class _SectorHitbox
{
    [SerializeField] private bool isShow = true;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float outerRadius; 
    [SerializeField] private float innerRadius; 
    [SerializeField] private float attackAngles; // goc hinh quat
    [SerializeField] private float attackDirections; // huong hinh quat 

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

    public float OuterRadius
    {
        get => outerRadius;
        set => outerRadius = value;
    }

    public float InnerRadius
    {
        get => innerRadius;
        set => innerRadius = value;
    }

    public float AttackAngles
    {
        get => attackAngles;
        set => attackAngles = value;
    }

    public float AttackDirections
    {
        get => attackDirections;
        set => attackDirections = value;
    }
}

