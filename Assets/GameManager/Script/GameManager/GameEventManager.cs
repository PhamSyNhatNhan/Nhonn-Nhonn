// Phien ban cu
/*
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
        public class ControllerEvent : UnityEvent<Component> {}
        public GenericEvent<ControllerEvent> PlayerFlipCall = new GenericEvent<ControllerEvent>();
        
        public class HealthEvent : UnityEvent<Component, float> {}
        public GenericEvent<HealthEvent> OnPlayerHealthChanged = new GenericEvent<HealthEvent>();
            
        public class CombatEvent : UnityEvent<Component, object> {}
        public GenericEvent<CombatEvent> OnPlayerAttack = new GenericEvent<CombatEvent>();
        public GenericEvent<CombatEvent> OnPlayerSkill = new GenericEvent<CombatEvent>();
        public GenericEvent<CombatEvent> OnPlayerUlti = new GenericEvent<CombatEvent>();
        public GenericEvent<CombatEvent> OnPlayerDash = new GenericEvent<CombatEvent>();
        public GenericEvent<CombatEvent> OnPlayerBurst = new GenericEvent<CombatEvent>();
        
        public GenericEvent<CombatEvent> OnPlayerAttackSpeedChange = new GenericEvent<CombatEvent>();
        public GenericEvent<CombatEvent> OnAttackEnd = new GenericEvent<CombatEvent>();
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

*/


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static readonly PlayerEvents Player = new PlayerEvents();

    public class PlayerEvents
    {
        public class ControllerEvent : UnityEvent<Component> { }
        public GenericEvent<ControllerEvent> PlayerFlipCall = new GenericEvent<ControllerEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );

        public class HealthEvent : UnityEvent<Component, float> { }
        public GenericEvent<HealthEvent> OnPlayerHealthChanged = new GenericEvent<HealthEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );

        public class CombatEvent : UnityEvent<Component, object> { }
        public GenericEvent<CombatEvent> OnPlayerAttack = new GenericEvent<CombatEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );
        public GenericEvent<CombatEvent> OnPlayerSkill = new GenericEvent<CombatEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );
        public GenericEvent<CombatEvent> OnPlayerUlti = new GenericEvent<CombatEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );
        public GenericEvent<CombatEvent> OnPlayerDash = new GenericEvent<CombatEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );
        public GenericEvent<CombatEvent> OnPlayerBurst = new GenericEvent<CombatEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );
        public GenericEvent<CombatEvent> OnPlayerAttackSpeedChange = new GenericEvent<CombatEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );
        public GenericEvent<CombatEvent> OnAttackEnd = new GenericEvent<CombatEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );
        public GenericEvent<CombatEvent> OnMoveToEnd = new GenericEvent<CombatEvent>(
            (source, dest) => source.AddListener(dest.Invoke)
        );
    }
}

public class GenericEvent<T> where T : UnityEventBase, new()
{
    private Dictionary<string, T> map = new Dictionary<string, T>();
    private T globalEvent = new T();
    private Action<T, T> patchAction;

    public GenericEvent(Action<T, T> patchAction)
    {
        this.patchAction = patchAction ?? throw new ArgumentNullException(nameof(patchAction));
    }

    public T Get(string channel = "")
    {
        if (string.IsNullOrEmpty(channel))
        {
            return globalEvent;
        }

        if (!map.ContainsKey(channel))
        {
            map[channel] = new T();
            patchAction(map[channel], globalEvent);
        }

        return map[channel];
    }

    public T TryAdd(string channel)
    {
        return Get(channel);
    }
}