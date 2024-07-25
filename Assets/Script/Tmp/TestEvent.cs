using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Player.OnPlayerAttack.Get("").AddListener((component, data) => TestEventFunc("Attack"));
        EventManager.Player.OnPlayerAttackSpeedChange.Get("").AddListener((component, data) => ChangeAttackSpeed(float.Parse(data.ToString())));
    }
    

    private void OnDisable()
    {
        EventManager.Player.OnPlayerAttack.Get("").RemoveListener((component, data) => TestEventFunc("Attack"));
        EventManager.Player.OnPlayerAttackSpeedChange.Get("").RemoveListener((component, data) => ChangeAttackSpeed(float.Parse(data.ToString())));

    }
    private void ChangeAttackSpeed(float speed)
    {
        //Debug.Log(speed);
    }


    private void TestEventFunc(string eventName)
    {
        //Debug.Log(eventName);
    }
}
