using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    //Toan bo cac event deu quan ly o day
    public static readonly PlayerEvents Player = new PlayerEvents();
        
    public class PlayerEvents
    {
        public class HealthEvent : UnityEvent<Component, float> {}
        public GenericEvent<HealthEvent> OnPlayerHealthChanged = new GenericEvent<HealthEvent>();
            
        public class CombatEvent : UnityEvent<Component, object> {}
        public GenericEvent<CombatEvent> OnPlayerAttack = new GenericEvent<CombatEvent>();
        public GenericEvent<CombatEvent> OnPlayerSkill = new GenericEvent<CombatEvent>();
        public GenericEvent<CombatEvent> OnPlayerBurst = new GenericEvent<CombatEvent>();
        public GenericEvent<CombatEvent> OnPlayerDash = new GenericEvent<CombatEvent>();
        public GenericEvent<CombatEvent> OnPlayerAttackSpeedChange = new GenericEvent<CombatEvent>();
    }
        
        
}
    
public class GenericEvent<T> where T: class, new()
{
    private Dictionary<String, T> map = new Dictionary<String, T>();

    public T Get(string channel = "")
    {
        map.TryAdd(channel, new T());
        return map[channel];
    }
}
