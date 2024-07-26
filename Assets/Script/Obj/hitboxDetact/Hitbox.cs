using System.Collections.Generic;
using UnityEngine;

public abstract class Hitbox : MonoBehaviour
{
    public abstract List<GameObject> detectObject(LayerMask enableDamage);
}
