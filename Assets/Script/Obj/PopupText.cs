using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    private float destroyTime = 0.5f;
    private Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
    private Vector3 randomIntensity = new Vector3(0.2f, 0.1f, 0.1f);
    
    void Start()
    {
        Destroy(gameObject, destroyTime);
        
        transform.localPosition += new Vector3(
            Random.Range(0.0f, 0.0f),
            Random.Range(0.3f, offset.y),
            Random.Range(0.0f, 0.0f)
        );
        transform.localPosition += new Vector3(
            Random.Range(-randomIntensity.x, randomIntensity.x),
            Random.Range(-randomIntensity.y, randomIntensity.y),
            Random.Range(-randomIntensity.z, randomIntensity.z)
        );
        
    }
}
