using System;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{
    [SerializeField] private GameObject bt;

    private String nameEvent;
    private object data;
    
    private void OnEnable()
    {
        EventManager.Ui.TriggerInteractiveUiEvent.Get().AddListener((component, status, nameEvent, data) => ChangeStatus(status, nameEvent, data));
    }

    private void OnDisable()
    {
        EventManager.Ui.TriggerInteractiveUiEvent.Get().RemoveListener((component, status, nameEvent, data) => ChangeStatus(status, nameEvent, data));
    }

    void Start()
    {
        bt.SetActive(false);
    }
    

    private void ChangeStatus(bool status, String nameEvent, object data)
    {
        bt.SetActive(status);
        this.nameEvent = nameEvent;
        this.data = data;
    }

    public void Click()
    {
        EventManager.Enviroment.TriggerInteractiveEvent.Get(nameEvent).Invoke(this, data);
    }
}
