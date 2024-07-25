using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GuraSkill : PlayerSkill
{
    private Gura gura;
    private GuraController gc;
    
    [Header("Setup")]
    [SerializeField] private LayerMask enemyLayer;
    private bool canInput = true;
    
    [Header("Attack")]
    private bool isAttack = false;
    private bool canAttack = true;
    private List<GameObject> listAttack = new List<GameObject>();
    [SerializeField] private float attackRadius1;
    [SerializeField] private Transform attackTransform1;
    
    [Header("Skill")]
    private bool isSkill = false;
    private bool canSkill = true;
    private List<GameObject> listSkill = new List<GameObject>();
    
    [Header("Burst")]
    private bool isBurst = false;   
    private bool canBurst = true;
    private List<GameObject> listBurst = new List<GameObject>();
    private bool isDive = false;
    [SerializeField] private GameObject prefabBurstEnd;
    [SerializeField] private Transform transformBurstEnd;
    private Coroutine CrBurstActive;
    
    [Header("Dash")]
    private bool isDash = false;
    private bool canDash = true;
    private List<GameObject> listDash = new List<GameObject>();
    
    
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

    protected override void TapAttack()
    {
        if (canInput)
        {
            if (canAttack)
            {
                if (isDive)
                {
                    
                }
                else
                
                {
                    if (EventManager.Player.OnPlayerAttack != null)
                    {
                        EventManager.Player.OnPlayerAttack.Get("").Invoke(this, null);
                    }
                    
                    Collider2D[] DetectObject = Physics2D.OverlapCircleAll(attackTransform1.position, attackRadius1, enemyLayer);

                    List<GameObject> Enemy = new List<GameObject>();

                    foreach (Collider2D collider in DetectObject)
                    {
                        Enemy.Add(collider.gameObject);
                    }

                    Enemy.Sort((a, b) => Vector2.Distance(transform.position, a.transform.position).CompareTo(Vector2.Distance(transform.position, b.transform.position)));

                    if (Enemy.Count != 0)
                    {
                        Enemy[0].GetComponent<Stat>().TakeDamage(DamageType.Magic, gura.CaculateDamage(DamageType.Magic, 1.0f), gura.CurCritRate, gura.CurCritDamage);
                    }
                }
            }
        }
    }

    protected override void TapBurst()
    {
        if (canInput)
        {
            if (canBurst && listSkillCds[0].SkillCdLeft == 0)
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
                    
                    isDive = false;
                    gc.Rb.velocity = Vector2.zero;
                    gc.CanMove = false;
                    listBurst[0].GetComponent<SkillObject>().SetUp(DamageType.Magic, new List<float>(){1.0f}, gura.CurCritRate, gura.CurCritDamage, gura.CurAttackSpeed);
                    listBurst[0].SetActive(true);
                    StartCoroutine(BurstEndAnimation(listBurst[0].GetComponent<Animator>()
                        .GetCurrentAnimatorStateInfo(0).length));
                    
                    listSkillCds[0].SkillCdLeft = listSkillCds[0].CurSkillCd;
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
        gura.CanDamge = true;
        gc.CanMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackTransform1.position, attackRadius1);
    }

    public bool IsDive
    {
        get => isDive;
        set => isDive = value;
    }
}
