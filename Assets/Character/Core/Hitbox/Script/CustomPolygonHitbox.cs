using System.Collections.Generic;
using UnityEngine;

public class CustomPolygonHitbox : Hitbox
{
    [SerializeField] private List<_CustomPolygonHitbox> attackPoints;

    private void OnDrawGizmos()
    {
        if (attackPoints == null || attackPoints.Count == 0)
        {
            return;
        }

        for (int i = 0; i < attackPoints.Count; i++)
        {
            if (attackPoints[i] == null || attackPoints[i].Points == null || attackPoints[i].Points.Count < 3)
            {
                continue;
            }

            Gizmos.color = i < hitboxColors.Count ? hitboxColors[i] : Color.white;

            for (int j = 0; j < attackPoints[i].Points.Count; j++)
            {
                if (attackPoints[i].Points[j] == null)
                {
                    continue;
                }

                Vector3 currentPoint = attackPoints[i].Points[j].position;
                Vector3 nextPoint = attackPoints[i].Points[(j + 1) % attackPoints[i].Points.Count].position;

                Gizmos.DrawLine(currentPoint, nextPoint);
            }
        }
    }

    public override List<GameObject> detectObject(LayerMask enableDamage)
    {
        return detectObject(enableDamage, 0);
    }

    public override List<GameObject> detectObject(LayerMask enableDamage, int hitBox)
    {
        List<GameObject> listObject = new List<GameObject>();

        if (attackPoints == null || hitBox >= attackPoints.Count || attackPoints[hitBox] == null || attackPoints[hitBox].Points == null || attackPoints[hitBox].Points.Count < 3)
        {
            return listObject;
        }

        // Tạo một PolygonCollider2D tạm thời
        GameObject tempObject = new GameObject("TempPolygonCollider");
        tempObject.transform.position = Vector3.zero;
        PolygonCollider2D polygonCollider = tempObject.AddComponent<PolygonCollider2D>();

        Vector2[] points = new Vector2[attackPoints[hitBox].Points.Count];
        for (int i = 0; i < attackPoints[hitBox].Points.Count; i++)
        {
            points[i] = attackPoints[hitBox].Points[i].position;
        }

        polygonCollider.points = points;
        
        Collider2D[] allColliders = Physics2D.OverlapAreaAll(polygonCollider.bounds.min, polygonCollider.bounds.max, enableDamage);

        foreach (Collider2D collider in allColliders)
        {
            if (polygonCollider.OverlapPoint(collider.transform.position))
            {
                listObject.Add(collider.gameObject);
            }
        }

        // Hủy đối tượng PolygonCollider2D tạm thời
        Destroy(tempObject);

        if (listObject.Count != 0)
        {
            listObject.Sort((a, b) => Vector2.Distance(transform.position, a.transform.position).CompareTo(Vector2.Distance(transform.position, b.transform.position)));
        }

        return listObject;
    }
}


[System.Serializable]
public class _CustomPolygonHitbox
{
    [SerializeField] private List<Transform> points;

    public List<Transform> Points
    {
        get => points;
        set => points = value;
    }
}
