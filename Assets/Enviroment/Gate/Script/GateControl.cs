using System;
using System.Collections;
using UnityEngine;

public class GateControl : InteractiveObject
{
    [SerializeField] private String nameScene;
    
    [SerializeField] private GameObject prefabGateDoorLeft;
    [SerializeField] private GameObject prefabGateDoorRight;

    [SerializeField] private float openSpeed = 1f;
    
    [SerializeField] private Quaternion leftClosedRot, rightClosedRot;
    [SerializeField] private Quaternion leftOpenRot, rightOpenRot;
    private bool isOpen = false;
    
    protected override void OnEnable()
    {
        EventManager.Enviroment.TriggerInteractiveEvent.Get(nameObject).AddListener((component, data) => ChangeGateStatus(data));
    }

    protected override void OnDisable()
    {
        EventManager.Enviroment.TriggerInteractiveEvent.Get(nameObject).RemoveListener((component, data) => ChangeGateStatus(data));
    }

    protected override void Start()
    {
        
    }

    
    protected override void Update()
    {
        
    }

    private void ChangeGateStatus(object data)
    {
        bool status = (bool)data;
        
        if (status != isOpen)
        {
            isOpen = status;
            StopAllCoroutines();
            float duration = 1f / openSpeed; 
            StartCoroutine(RotateGate(status, duration));
            StartCoroutine(EndGateChange(status, duration));
        }
    }
    
    private IEnumerator RotateGate(bool open, float duration)
    {
        Quaternion targetLeft = open ? leftOpenRot : leftClosedRot;
        Quaternion targetRight = open ? rightOpenRot : rightClosedRot;

        float elapsedTime = 0f;

        Quaternion startLeft = prefabGateDoorLeft.transform.localRotation;
        Quaternion startRight = prefabGateDoorRight.transform.localRotation;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            prefabGateDoorLeft.transform.localRotation = Quaternion.Slerp(startLeft, targetLeft, t);
            prefabGateDoorRight.transform.localRotation = Quaternion.Slerp(startRight, targetRight, t);

            yield return null;
        }

        prefabGateDoorLeft.transform.localRotation = targetLeft;
        prefabGateDoorRight.transform.localRotation = targetRight;
    }

    private IEnumerator EndGateChange(bool status, float time)
    {
        yield return new WaitForSeconds(time);
        
        EventManager.Gm.TriggerGenericEvent.Get("Scene").Invoke(this, nameScene);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.Ui.TriggerInteractiveUiEvent.Get().Invoke(this, true, nameObject, true);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        EventManager.Ui.TriggerInteractiveUiEvent.Get().Invoke(this, false, nameObject, false);

    }
}
