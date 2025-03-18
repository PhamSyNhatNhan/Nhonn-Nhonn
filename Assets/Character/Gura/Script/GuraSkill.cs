using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;

public class GuraSkill : PlayerSkill
{
    private Gura gura;
    private GuraController gc;

    [Header("Generic")] 
    private Hitbox hitbox;

    [Header("Attack")] 
    [SerializeField] private float attackDelay = 0.1f;
    private int numberAttack = 0;
    private Coroutine coroutineResetAttack;
    [SerializeField] private Transform transformNorAttack;
    [SerializeField] private GameObject prefabAttack1;
    [SerializeField] private GameObject prefabAttack2;
    [SerializeField] private GameObject prefabAttack3;
     
    //[Header("Skill")]
    [Header("Ulti")]
    [SerializeField] private GameObject prefabUltiEnd;
    [SerializeField] private Transform transformUltiEnd;
    private bool isDive = false;
    private Coroutine CrUltiActive;
    
    //[Header("Dash")]


    private void OnEnable()
    {
        EventManager.Player.OnAttackEnd.Get().AddListener((component, data) => OnEndNormalAttack());
        EventManager.Player.OnPlayerAttackSpeedChange.Get("").AddListener((component, data) => OnAttackSpeedChange());
        EventManager.Player.PlayerFlipCall.Get("Gura").AddListener((component) => FlipCall());
    }

    private void OnDisable()
    {
        EventManager.Player.OnAttackEnd.Get().RemoveListener((component, data) => OnEndNormalAttack());
        EventManager.Player.OnPlayerAttackSpeedChange.Get("").RemoveListener((component, data) => OnAttackSpeedChange());
        EventManager.Player.PlayerFlipCall.Get("Gura").RemoveListener((component) => FlipCall());
    }

    protected override void Start()
    {
        base.Start();
        gura = GetComponent<Gura>();
        gc = GetComponent<GuraController>();
        hitbox = GetComponent<Hitbox>();
        SetUpObject();
    }

    private void SetUpObject()
    {
        //Attack
        GameObject dmpAttackNor1 = Instantiate(prefabAttack1, transformNorAttack);
        skillObjectsMap.Add("Attack1", dmpAttackNor1);
        GameObject dmpAttackNor2 = Instantiate(prefabAttack2, transformNorAttack);
        skillObjectsMap.Add("Attack2", dmpAttackNor2);
        GameObject dmpAttackNor3 = Instantiate(prefabAttack3, transformNorAttack);
        skillObjectsMap.Add("Attack3", dmpAttackNor3);
        
        skillCdMap.Add("Attack",new SkillCd(SkillType.Attack, attackDelay));
        
        
        //Ulti
        GameObject dmpUltiEnd = Instantiate(prefabUltiEnd, transformUltiEnd);
        skillObjectsMap.Add("UltiEnd",dmpUltiEnd);
        skillCdMap.Add("Ulti",new SkillCd(SkillType.Ulti, 10.0f));


        foreach (var skillObject in skillObjectsMap)
        {
            skillObject.Value.SetActive(false);
        }
    }
    
    private void OnAttackSpeedChange()
    {
        attackDelay /= (attackDelay / 100);
        skillCdMap["Attack"].CurSkillCd = attackDelay;
    }

    private void FlipCall()
    {
        List<GameObject> Enemy = hitbox.detectObject(enemyLayer);
        if (Enemy.Count != 0)
        {
            gc.Flipping(Enemy[0].transform);
        }
    }

    protected override void TapAttack()
    {
        if (canInput)
        {
            if (canAttack && !isAttack && !isSkill && !isUlti && !isDash)
            {
                if (isDive)
                {
                    
                }
                else if (skillCdMap["Attack"].SkillCdLeft == 0)
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

                    string attackName = "Attack" + (numberAttack + 1).ToString();
                    
                    skillObjectsMap[attackName].GetComponent<ProjectileObject>().SetUp(DamageType.Magic, new List<float>(){1.0f}, gura.CurCritRate, gura.CurCritDamage, gura.CurAttackSpeed);
                    skillObjectsMap[attackName].SetActive(true);
                    
                    numberAttack += 1;
                    if (numberAttack > 2) numberAttack = 0;
                }
            }
        }
    }

    private void OnEndNormalAttack()
    {
        canAttack = true;
        skillCdMap["Attack"].SkillCdLeft = skillCdMap["Attack"].CurSkillCd;
        gc.CanFlip = true;
    }

    private IEnumerator IEResetAttack()
    {
        yield return new WaitForSeconds(1.0f);

        numberAttack = 0;
    }

    protected override void TapUlti()
    {
        if (canInput)
        {
            if (canUlti && skillCdMap["Ulti"].SkillCdLeft == 0 && !isAttack && !isSkill && !isUlti && !isDash)
            {
                if (!isDive)
                {
                    if (EventManager.Player.OnPlayerUlti != null)
                    {
                        EventManager.Player.OnPlayerUlti.Get("").Invoke(this, null);
                    }
                    
                    isDive = true;
                    gura.CanDamge = false;
                    
                    if (CrUltiActive == null)
                    {
                        CrUltiActive = StartCoroutine(IEEndUlti());
                    }
                    else
                    {
                        StopCoroutine(CrUltiActive);
                        CrUltiActive = StartCoroutine(IEEndUlti());
                    }
                }
                else
                {
                    StopCoroutine(CrUltiActive);

                    isUlti = true;
                    
                    isDive = false;
                    gc.CanMove = false;
                    skillObjectsMap["UltiEnd"].GetComponent<ProjectileObject>().SetUp(DamageType.Magic, new List<float>(){1.0f}, gura.CurCritRate, gura.CurCritDamage);
                    skillObjectsMap["UltiEnd"].SetActive(true);
                    
                    StartCoroutine(UltiEndAnimation(skillObjectsMap["UltiEnd"].GetComponent<Animator>()
                        .GetCurrentAnimatorStateInfo(0).length * 0.75f));
                    
                    skillCdMap["Ulti"].SkillCdLeft = skillCdMap["Ulti"].CurSkillCd;
                }
            }
        }
    }

    private IEnumerator IEEndUlti()
    {
        yield return new WaitForSeconds(5.0f);
        
        TapUlti();
    }

    private IEnumerator UltiEndAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isUlti = false;
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
