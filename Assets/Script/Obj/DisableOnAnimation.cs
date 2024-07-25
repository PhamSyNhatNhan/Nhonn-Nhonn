using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnAnimation : MonoBehaviour
{
    private Animator amt;
    
    private void OnEnable()
    {
        if (amt == null)
            amt = GetComponent<Animator>();
        if (amt != null)
            StartCoroutine(DisableAfterAnimation());
    }
    
    private IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(amt.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
    
}
