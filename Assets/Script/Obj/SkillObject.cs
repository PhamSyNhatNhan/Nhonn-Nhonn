using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    protected DamageType type = DamageType.True;
    protected List<float> damage = new List<float> { 0.0f };
    protected float critRate = 0.0f;
    protected float critDamage = 0.0f;
    protected float attackSpeed = 100.0f;
    
    [SerializeField] private bool canSpeedUp = false;
    [SerializeField] protected LayerMask enableDamage;

    private Animator amt;

    protected virtual void Awake()
    {
        amt = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if(canSpeedUp)
            EventManager.Player.OnPlayerAttackSpeedChange.Get("").AddListener((component, data) => ChangeAttackSpeed(float.Parse(data.ToString())));
    }

    private void OnDisable()
    {
        if(canSpeedUp)
            EventManager.Player.OnPlayerAttackSpeedChange.Get("").RemoveListener((component, data) => ChangeAttackSpeed(float.Parse(data.ToString())));
    }

    private void ChangeAttackSpeed(float _attackSpeed)
    {
        if (canSpeedUp)
        {
           attackSpeed = _attackSpeed;
           amt.speed = attackSpeed / 100.0f;
        }
    }
    
    private void ChangeAttackSpeed()
    {
        if (canSpeedUp)
        {
            amt.speed = attackSpeed / 100.0f;
        }
    }

    public void SetUp(DamageType type, List<float> damage, float critRate, float critDamage, float attackSpeed)
    {
        this.type = type;
        this.damage = damage;
        this.critRate = critRate;
        this.critDamage = critDamage;
        this.attackSpeed = attackSpeed;
        ChangeAttackSpeed();
    }
    public void SetUp(DamageType type, List<float> damage, float critRate, float critDamage)
    {
        this.type = type;
        this.damage = damage;
        this.critRate = critRate;
        this.critDamage = critDamage;
    }

    public virtual void SendDamage(){}
    

    public LayerMask EnableDamage
    {
        get => enableDamage;
        set => enableDamage = value;
    }
    
}
