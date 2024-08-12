using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GuraSkill : PlayerSkill
{
    private Gura gura;
    private GuraController gc;

    [Header("Attack")] 
    [SerializeField] private float attackDelay = 0.1f;
    private int numberAttack = 0;
    private Coroutine coroutineResetAttack;
    [SerializeField] private Transform transformNorAttack;
    [SerializeField] private GameObject prefabAttack1;
    [SerializeField] private GameObject prefabAttack2;
    [SerializeField] private GameObject prefabAttack3;
     
    //[Header("Skill")]
    
    [Header("Burst")]
    [SerializeField] private GameObject prefabBurstEnd;
    [SerializeField] private Transform transformBurstEnd;
    private bool isDive = false;
    private Coroutine CrBurstActive;
    
    //[Header("Dash")]


    private void OnEnable()
    {
        EventManager.Player.OnPlayerAttack.Get("endAttack").AddListener((component, data) => OnEndNormalAttack());
        EventManager.Player.OnPlayerAttackSpeedChange.Get("").AddListener((component, data) => OnSpeedChange());
    }

    private void OnDisable()
    {
        EventManager.Player.OnPlayerAttack.Get("endAttack").RemoveListener((component, data) => OnEndNormalAttack());
        EventManager.Player.OnPlayerAttackSpeedChange.Get("").RemoveListener((component, data) => OnSpeedChange());
    }

    protected override void Start()
    {
        base.Start();
        gura = GetComponent<Gura>();
        gc = GetComponent<GuraController>();
        SetUpObject();
        SetUpCD();
    }

    private void SetUpObject()
    {
        //Attack
        GameObject dmpAttackNor1 = Instantiate(prefabAttack1, transformNorAttack);
        listAttack.Add(dmpAttackNor1);
        GameObject dmpAttackNor2 = Instantiate(prefabAttack2, transformNorAttack);
        listAttack.Add(dmpAttackNor2);
        GameObject dmpAttackNor3 = Instantiate(prefabAttack3, transformNorAttack);
        listAttack.Add(dmpAttackNor3);
        
        listSkillCds.Add(new SkillCd("Attack", SkillType.Attack, attackDelay));
        
        for (int i = 0; i < listAttack.Count; i++)
            listAttack[i].SetActive(false);
        
        //Burst
        GameObject dmpBurstEnd = Instantiate(prefabBurstEnd, transformBurstEnd);
        listBurst.Add(dmpBurstEnd);
        listSkillCds.Add(new SkillCd("Burst", SkillType.Burst, 10.0f));
        
        for (int i = 0; i < listBurst.Count; i++)
            listBurst[i].SetActive(false);
    }
    private void SetUpCD()
    {
        for (int i = 0; i < listSkillCds.Count; i++)
            if (listSkillCds[i].SkillType == SkillType.Attack)
                listSkillCds[i].CurSkillCd = listSkillCds[i].BaseSkillCd * multiplierCdAttack;
            else if (listSkillCds[i].SkillType == SkillType.Skill)
                listSkillCds[i].CurSkillCd = listSkillCds[i].BaseSkillCd * multiplierCdSkill;
            else if (listSkillCds[i].SkillType == SkillType.Burst)
                listSkillCds[i].CurSkillCd = listSkillCds[i].BaseSkillCd * multiplierCdBurst;
            else if (listSkillCds[i].SkillType == SkillType.Dash)
                listSkillCds[i].CurSkillCd = listSkillCds[i].BaseSkillCd * multiplierCdDash;
    }
    
    private void OnSpeedChange()
    {
        attackDelay /= (attackDelay / 100);
        listSkillCds[0].CurSkillCd = attackDelay;
    }

    protected override void TapAttack()
    {
        if (canInput)
        {
            if (canAttack && !isAttack && !isSkill && !isBurst && !isDash)
            {
                if (isDive)
                {
                    
                }
                else if (listSkillCds[0].SkillCdLeft == 0)
                {
                    if (EventManager.Player.OnPlayerAttack != null)
                    {
                        EventManager.Player.OnPlayerAttack.Get("").Invoke(this, null);
                    }
                    
                    canAttack = false;
                    gc.CanFlip = false;
                    
                    if (coroutineResetAttack == null)
                    {
                        coroutineResetAttack = StartCoroutine(IEResetAttack());
                    }
                    else
                    {
                        StopCoroutine(coroutineResetAttack);
                        coroutineResetAttack = StartCoroutine(IEResetAttack());
                    }
                    
                    listAttack[numberAttack].GetComponent<ProjectileObject>().SetUp(DamageType.Magic, new List<float>(){1.0f}, gura.CurCritRate, gura.CurCritDamage, gura.CurAttackSpeed);
                    listAttack[numberAttack].SetActive(true);
                    
                    numberAttack += 1;
                    if (numberAttack > 2) numberAttack = 0;
                }
            }
        }
    }

    private void OnEndNormalAttack()
    {
        canAttack = true;
        listSkillCds[0].SkillCdLeft = listSkillCds[0].CurSkillCd;
        gc.CanFlip = true;
    }

    private IEnumerator IEResetAttack()
    {
        yield return new WaitForSeconds(1.0f);

        numberAttack = 0;
    }

    protected override void TapBurst()
    {
        if (canInput)
        {
            if (canBurst && listSkillCds[1].SkillCdLeft == 0 && !isAttack && !isSkill && !isBurst && !isDash)
            {
                if (!isDive)
                {
                    if (EventManager.Player.OnPlayerBurst != null)
                    {
                        EventManager.Player.OnPlayerBurst.Get("").Invoke(this, null);
                    }
                    
                    isDive = true;
                    gura.CanDamge = false;
                    
                    if (CrBurstActive == null)
                    {
                        CrBurstActive = StartCoroutine(IEEndBurst());
                    }
                    else
                    {
                        StopCoroutine(CrBurstActive);
                        CrBurstActive = StartCoroutine(IEEndBurst());
                    }
                }
                else
                {
                    StopCoroutine(CrBurstActive);

                    isBurst = true;
                    
                    isDive = false;
                    gc.CanMove = false;
                    listBurst[0].GetComponent<ProjectileObject>().SetUp(DamageType.Magic, new List<float>(){1.0f}, gura.CurCritRate, gura.CurCritDamage);
                    listBurst[0].SetActive(true);
                    
                    StartCoroutine(BurstEndAnimation(listBurst[0].GetComponent<Animator>()
                        .GetCurrentAnimatorStateInfo(0).length * 0.75f));
                    
                    listSkillCds[1].SkillCdLeft = listSkillCds[1].CurSkillCd;
                }
            }
        }
    }

    private IEnumerator IEEndBurst()
    {
        yield return new WaitForSeconds(5.0f);
        
        TapBurst();
    }

    private IEnumerator BurstEndAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isBurst = false;
        gura.CanDamge = true;
        gc.CanMove = true;
    }

    private void OnDrawGizmos()
    {
        
    }

    public bool IsDive
    {
        get => isDive;
        set => isDive = value;
    }
}
