using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float baseHealth = 5000.0f;
    [SerializeField] private float maxHealth;
    [SerializeField] private float curHealth;
    [SerializeField] private float bonusFlatHealth;
    [SerializeField] private float bonusMultiplierHealth;
    
    [Header("Defense")]
    [SerializeField] private float baseDefense = 100.0f;
    [SerializeField] private float maxDefense;
    [SerializeField] private float curDefense;
    [SerializeField] private float bonusFlatDefense;
    [SerializeField] private float bonusMultiplierDefense;
    
    [Header("Resistant")]
    [SerializeField] private float baseResistantPhysical = 0.0f;
    [SerializeField] private float maxResistantPhysical;
    [SerializeField] private float curResistantPhysical;
    [SerializeField] private float bonusFlatResistantPhysical;
    [SerializeField] private float bonusMultiplierResistantPhysical;
    
    [SerializeField] private float baseResistantMagic = 0.0f;
    [SerializeField] private float maxResistantMagic;
    [SerializeField] private float curResistantMagic;
    [SerializeField] private float bonusFlatResistantMagic;
    [SerializeField] private float bonusMultiplierResistantMagic;
    
    [Header("Attack")]
    [SerializeField] private float baseAttack = 0.0f;
    [SerializeField] private float maxAttack;
    [SerializeField] private float curAttack;
    [SerializeField] private float bonusFlatAttack;
    [SerializeField] private float bonusMultiplierAttack;
    
    [Header("AttackSpeed")] //Tinh theo %
    [SerializeField] private float baseAttackSpeed = 100.0f;
    [SerializeField] private float maxAttackSpeed;
    [SerializeField] private float curAttackSpeed;
    [SerializeField] private float bonusMultiplierAttackSpeed;
    
    [Header("BonusDamage")] //Tinh theo %
    [SerializeField] private float baseBonusDamage = 0.0f;
    [SerializeField] private float maxBonusDamage;
    [SerializeField] private float curBonusDamage;
    [SerializeField] private float bonusMultiplierBonusDamage;
    
    [Header("BonusPhysical")] //Tinh theo %
    [SerializeField] private float baseBonusPhysical = 0.0f;
    [SerializeField] private float maxBonusPhysical;
    [SerializeField] private float curBonusPhysical;
    [SerializeField] private float bonusMultiplierBonusPhysical;
    
    [Header("BonusMagic")] //Tinh theo %
    [SerializeField] private float baseBonusMagic = 0.0f;
    [SerializeField] private float maxBonusMagic;
    [SerializeField] private float curBonusMagic;
    [SerializeField] private float bonusMultiplierBonusMagic;
    
    [Header("MultiplierDamageBonus")] //Tinh theo %
    [SerializeField] private float baseMultiplierDamageBonus = 0.0f;
    [SerializeField] private float maxMultiplierDamageBonus;
    [SerializeField] private float curMultiplierDamageBonus;
    [SerializeField] private float bonusMultiplierMultiplierDamageBonus;
    
    [Header("MultiplierDamageTaken")] //Tinh theo %
    [SerializeField] private float baseMultiplierDamageTaken = 0.0f;
    [SerializeField] private float maxMultiplierDamageTaken;
    [SerializeField] private float curMultiplierDamageTaken;
    [SerializeField] private float bonusMultiplierMultiplierDamageTaken;
    
    [Header("CritRate")] //Tinh theo %
    [SerializeField] private float baseCritRate = 5.0f;
    [SerializeField] private float maxCritRate;
    [SerializeField] private float curCritRate;
    [SerializeField] private float bonusMultiplierCritRate;
    
    [Header("CritDamage")] //Tinh theo %
    [SerializeField] private float baseCritDamage = 50.0f;
    [SerializeField] private float maxCritDamage;
    [SerializeField] private float curCritDamage;
    [SerializeField] private float bonusMultiplierCritDamage;

    [Header("DamageCaculation")] 
    [SerializeField] private bool canDamge = true;
    private float lastDamageTime  = -100.0f;
    private float iFrame = 0.02f;
    [SerializeField] private GameObject popupTextPrefab;
    
    
    private void Start()
    {
        setStartStat();
    }

    protected virtual  void setStartStat()
    {
        maxHealth = baseHealth * (1.0f + (bonusMultiplierHealth / 100.0f)) + bonusFlatHealth;
        curHealth = maxHealth;
        maxDefense = baseDefense * (1.0f + (bonusMultiplierDefense / 100.0f)) + bonusFlatDefense;
        curDefense = maxDefense;
        maxResistantPhysical = maxResistantPhysical * (1.0f + (BonusMultiplierResistantPhysical / 100.0f)) + bonusFlatResistantPhysical;
        curResistantPhysical = maxResistantPhysical;
        maxResistantMagic = maxResistantMagic * (1.0f + (BonusMultiplierResistantMagic / 100.0f)) + bonusFlatResistantMagic;
        curResistantMagic = maxResistantMagic;
        maxAttack = baseAttack * (1.0f + (bonusMultiplierAttack / 100.0f)) + bonusFlatAttack;
        curAttack = maxAttack;

        maxAttackSpeed = baseAttackSpeed + bonusMultiplierAttackSpeed;
        curAttackSpeed = maxAttackSpeed;
        maxBonusDamage = baseBonusDamage + bonusMultiplierBonusDamage;
        curBonusDamage = maxBonusDamage;
        maxBonusPhysical = baseBonusPhysical + bonusMultiplierBonusPhysical;
        curBonusPhysical = maxBonusPhysical;
        maxBonusMagic = baseBonusMagic + bonusMultiplierBonusMagic;
        curBonusMagic = maxBonusMagic;
        maxMultiplierDamageBonus = baseMultiplierDamageBonus + bonusMultiplierMultiplierDamageBonus;
        curMultiplierDamageBonus = maxMultiplierDamageBonus;
        maxMultiplierDamageTaken = baseMultiplierDamageTaken + bonusMultiplierMultiplierDamageTaken;
        curMultiplierDamageTaken = maxMultiplierDamageTaken;
        maxCritRate = baseCritRate + bonusMultiplierCritRate;
        curCritRate = maxCritRate;
        maxCritDamage = baseCritDamage + bonusMultiplierCritDamage;
        curCritDamage = maxCritDamage;
    }

    public virtual void TakeDamage(DamageType type , float damage, float _critRate, float _critDamage)
    {
        if ((Time.time >= lastDamageTime + iFrame) && canDamge)
        {
            lastDamageTime = Time.time;
            float tmpDmgTake = 0.0f;

            if (type == DamageType.True)
            {
                tmpDmgTake = damage * (1.0f + curMultiplierDamageTaken / 100f);
            }
            else if (type == DamageType.Physical)
            {
                tmpDmgTake = damage * (CaculateResistant(type)) * (CaculateDefense()) *
                             (1.0f + curMultiplierDamageTaken / 100f);
            }
            else if (type == DamageType.Magic)
            {
                tmpDmgTake = damage * (CaculateResistant(type)) *
                             (1.0f + curMultiplierDamageTaken / 100f);
            }

            bool isCrit = false;
            if (UnityEngine.Random.Range(0f, 100f) <= _critRate)
            {
                isCrit = true;
                tmpDmgTake *= tmpDmgTake * (1.0f + _critDamage / 100.0f);
            }

            if (popupTextPrefab)
            {
                PopupTextShow(type, tmpDmgTake, isCrit);
            }
            
            curHealth -= tmpDmgTake;
            if (curHealth < 0) Destroy(gameObject);
        }
    }

    protected virtual float CaculateResistant(DamageType type)
    {
        if (type == DamageType.Physical)
        {
            if ((curResistantPhysical / 100.0f) < 0)
            {
                return 1.0f - ((curResistantPhysical / 100.0f) * 0.5f);
            }
            else if ((curResistantPhysical / 100.0f) >= 0.0f && (curResistantPhysical / 100.0f) <= 0.3f)
            {
                return 1.0f - (curResistantPhysical / 100.0f);
            }
            else
            {
                return 0.3f + (1.0f / (4.0f * (curResistantPhysical / 100.0f) + 1.0f));
            }
        }
        else if (type == DamageType.Magic)
        {
            if ((curResistantMagic / 100.0f) < 0)
            {
                return 1.0f - ((curResistantMagic / 100.0f) * 0.5f);
            }
            else if ((curResistantMagic / 100.0f) >= 0.0f && (curResistantMagic / 100.0f) <= 0.7f)
            {
                return 1.0f - (curResistantMagic / 100.0f);
            }
            else
            {
                return 0.7f + (1.0f / (4.0f * (curResistantMagic / 100.0f) + 1.0f));
            }
        }
        else
        {
            return 0.0f;
        }
    }
    protected virtual float CaculateDefense()
    {
        return 1.0f - curDefense / (curDefense * 2.0f + 500);
    }

    private void PopupTextShow(DamageType type, float damage, bool isCrit)
    {
        GameObject popText = Instantiate(popupTextPrefab, transform.position, Quaternion.identity, transform);
        TextMesh tm = popText.GetComponent<TextMesh>();
        if (type == DamageType.True)
        {
            if (isCrit)
            {
                tm.text = "\u2727" + damage.ToString();
            }
            else
            {
                tm.text = damage.ToString();
            }
        }
        if (type == DamageType.Physical)
        {
            tm.color = Color.cyan;
            if (isCrit)
            {
                tm.text = "\u2727" + damage.ToString();
            }
            else
            {
                tm.text = damage.ToString();
            }
        }
        if (type == DamageType.Magic)
        {
            tm.color = Color.magenta;
            if (isCrit)
            {
                tm.text = "\u2727" + damage.ToString();
            }
            else
            {
                tm.text = damage.ToString();
            }
        }
    }
    
    public float CaculateDamage(DamageType type, float damage)
    {
        float tmpDamage = 0.0f;
        
        if (type == DamageType.True)
        {
            tmpDamage = damage * (1.0f + curBonusDamage / 100.0f) * (1.0f + curMultiplierDamageBonus / 100.0f);
        }
        else if (type == DamageType.Physical)
        {
            tmpDamage = damage * (1.0f + (curBonusDamage + curBonusPhysical) / 100.0f) * (1.0f + curMultiplierDamageBonus / 100.0f);
        }
        else if (type == DamageType.Magic)
        {
            tmpDamage = damage * (1.0f + (curBonusDamage + curBonusMagic) / 100.0f) * (1.0f + curMultiplierDamageBonus / 100.0f);
        }
        
        return tmpDamage;
    }
    
    


    public float BaseHealth
    {
        get => baseHealth;
        set => baseHealth = value;
    }

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public float CurHealth
    {
        get => curHealth;
        set => curHealth = value;
    }

    public float BonusFlatHealth
    {
        get => bonusFlatHealth;
        set => bonusFlatHealth = value;
    }

    public float BonusMultiplierHealth
    {
        get => bonusMultiplierHealth;
        set => bonusMultiplierHealth = value;
    }
    
    public float BaseDefense
    {
        get => baseDefense;
        set => baseDefense = value;
    }

    public float MaxDefense
    {
        get => maxDefense;
        set => maxDefense = value;
    }

    public float CurDefense
    {
        get => curDefense;
        set => curDefense = value;
    }

    public float BonusFlatDefense
    {
        get => bonusFlatDefense;
        set => bonusFlatDefense = value;
    }

    public float BonusMultiplierDefense
    {
        get => bonusMultiplierDefense;
        set => bonusMultiplierDefense = value;
    }

    public float BaseResistantPhysical
    {
        get => baseResistantPhysical;
        set => baseResistantPhysical = value;
    }

    public float MaxResistantPhysical
    {
        get => maxResistantPhysical;
        set => maxResistantPhysical = value;
    }

    public float CurResistantPhysical
    {
        get => curResistantPhysical;
        set => curResistantPhysical = value;
    }

    public float BonusFlatResistantPhysical
    {
        get => bonusFlatResistantPhysical;
        set => bonusFlatResistantPhysical = value;
    }

    public float BonusMultiplierResistantPhysical
    {
        get => bonusMultiplierResistantPhysical;
        set => bonusMultiplierResistantPhysical = value;
    }

    public float BaseResistantMagic
    {
        get => baseResistantMagic;
        set => baseResistantMagic = value;
    }

    public float MaxResistantMagic
    {
        get => maxResistantMagic;
        set => maxResistantMagic = value;
    }

    public float CurResistantMagic
    {
        get => curResistantMagic;
        set => curResistantMagic = value;
    }

    public float BonusFlatResistantMagic
    {
        get => bonusFlatResistantMagic;
        set => bonusFlatResistantMagic = value;
    }

    public float BonusMultiplierResistantMagic
    {
        get => bonusMultiplierResistantMagic;
        set => bonusMultiplierResistantMagic = value;
    }

    public float BaseAttack
    {
        get => baseAttack;
        set => baseAttack = value;
    }

    public float MaxAttack
    {
        get => maxAttack;
        set => maxAttack = value;
    }

    public float CurAttack
    {
        get => curAttack;
        set => curAttack = value;
    }

    public float BonusFlatAttack
    {
        get => bonusFlatAttack;
        set => bonusFlatAttack = value;
    }

    public float BonusMultiplierAttack
    {
        get => bonusMultiplierAttack;
        set => bonusMultiplierAttack = value;
    }

    public float BaseBonusPhysical
    {
        get => baseBonusPhysical;
        set => baseBonusPhysical = value;
    }

    public float MaxBonusPhysical
    {
        get => maxBonusPhysical;
        set => maxBonusPhysical = value;
    }

    public float CurBonusPhysical
    {
        get => curBonusPhysical;
        set => curBonusPhysical = value;
    }

    public float BonusMultiplierBonusPhysical
    {
        get => bonusMultiplierBonusPhysical;
        set => bonusMultiplierBonusPhysical = value;
    }

    public float BaseBonusMagic
    {
        get => baseBonusMagic;
        set => baseBonusMagic = value;
    }

    public float MaxBonusMagic
    {
        get => maxBonusMagic;
        set => maxBonusMagic = value;
    }

    public float CurBonusMagic
    {
        get => curBonusMagic;
        set => curBonusMagic = value;
    }

    public float BonusMultiplierBonusMagic
    {
        get => bonusMultiplierBonusMagic;
        set => bonusMultiplierBonusMagic = value;
    }

    public float BaseMultiplierDamageBonus
    {
        get => baseMultiplierDamageBonus;
        set => baseMultiplierDamageBonus = value;
    }

    public float MaxMultiplierDamageBonus
    {
        get => maxMultiplierDamageBonus;
        set => maxMultiplierDamageBonus = value;
    }

    public float CurMultiplierDamageBonus
    {
        get => curMultiplierDamageBonus;
        set => curMultiplierDamageBonus = value;
    }

    public float BonusMultiplierMultiplierDamageBonus
    {
        get => bonusMultiplierMultiplierDamageBonus;
        set => bonusMultiplierMultiplierDamageBonus = value;
    }

    public float BaseMultiplierDamageTaken
    {
        get => baseMultiplierDamageTaken;
        set => baseMultiplierDamageTaken = value;
    }

    public float MaxMultiplierDamageTaken
    {
        get => maxMultiplierDamageTaken;
        set => maxMultiplierDamageTaken = value;
    }

    public float CurMultiplierDamageTaken
    {
        get => curMultiplierDamageTaken;
        set => curMultiplierDamageTaken = value;
    }

    public float BonusMultiplierMultiplierDamageTaken
    {
        get => bonusMultiplierMultiplierDamageTaken;
        set => bonusMultiplierMultiplierDamageTaken = value;
    }

    public float BaseCritRate
    {
        get => baseCritRate;
        set => baseCritRate = value;
    }

    public float MaxCritRate
    {
        get => maxCritRate;
        set => maxCritRate = value;
    }

    public float CurCritRate
    {
        get => curCritRate;
        set => curCritRate = value;
    }

    public float BonusMultiplierCritRate
    {
        get => bonusMultiplierCritRate;
        set => bonusMultiplierCritRate = value;
    }

    public float BaseCritDamage
    {
        get => baseCritDamage;
        set => baseCritDamage = value;
    }

    public float MaxCritDamage
    {
        get => maxCritDamage;
        set => maxCritDamage = value;
    }

    public float CurCritDamage
    {
        get => curCritDamage;
        set => curCritDamage = value;
    }

    public float BonusMultiplierCritDamage
    {
        get => bonusMultiplierCritDamage;
        set => bonusMultiplierCritDamage = value;
    }

    public float BaseBonusDamage
    {
        get => baseBonusDamage;
        set => baseBonusDamage = value;
    }

    public float MaxBonusDamage
    {
        get => maxBonusDamage;
        set => maxBonusDamage = value;
    }

    public float CurBonusDamage
    {
        get => curBonusDamage;
        set => curBonusDamage = value;
    }

    public float BonusMultiplierBonusDamage
    {
        get => bonusMultiplierBonusDamage;
        set => bonusMultiplierBonusDamage = value;
    }

    public bool CanDamge
    {
        get => canDamge;
        set => canDamge = value;
    }

    public float BaseAttackSpeed
    {
        get => baseAttackSpeed;
        set => baseAttackSpeed = value;
    }

    public float MaxAttackSpeed
    {
        get => maxAttackSpeed;
        set => maxAttackSpeed = value;
    }

    public float CurAttackSpeed
    {
        get => curAttackSpeed;
        set 
        {
            curAttackSpeed = value;
            if (EventManager.Player.OnPlayerAttackSpeedChange != null)
            {
                EventManager.Player.OnPlayerAttackSpeedChange.Get("").Invoke(this, curAttackSpeed);
            }
        }
    }

    public float BonusMultiplierAttackSpeed
    {
        get => bonusMultiplierAttackSpeed;
        set => bonusMultiplierAttackSpeed = value;
    }
}
