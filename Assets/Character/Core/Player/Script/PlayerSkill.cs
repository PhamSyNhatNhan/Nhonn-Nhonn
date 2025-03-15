using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    [Header("Skill CD")]
    private PlayerControls pc;  
    protected List<SkillCd> listSkillCds = new List<SkillCd>();
    [SerializeField] protected float multiplierCdAttack = 1.0f;
    [SerializeField] protected float multiplierCdSkill = 1.0f;
    [SerializeField] protected float multiplierCdBurst = 1.0f;
    [SerializeField] protected float multiplierCdDash = 1.0f;
    
    [Header("Modify Button")]
    [SerializeField] private bool canHoldAttack = false;
    [SerializeField] private bool canHoldSkill = false;
    [SerializeField] private bool canHoldBurst = false;
    [SerializeField] private bool canHoldDash = false;
    
    [Header("Debug log")]
    [SerializeField] private bool debugAttack = true;
    [SerializeField] private bool debugSkill = true;
    [SerializeField] private bool debugBurst = true;
    [SerializeField] private bool debugDash = true;
    
    [Header("Setup")]
    [SerializeField] protected LayerMask enemyLayer;
    protected bool canInput = true;
    
    [Header("Attack")]
    protected bool isAttack = false;
    protected bool canAttack = true;
    protected List<GameObject> listAttack = new List<GameObject>();
    
    [Header("Skill")]
    protected bool isSkill = false;
    protected bool canSkill = true;
    protected List<GameObject> listSkill = new List<GameObject>();
    
    [Header("Burst")]
    protected bool isBurst = false;   
    protected bool canBurst = true;
    protected List<GameObject> listBurst = new List<GameObject>();
    
    [Header("Dash")]
    protected bool isDash = false;
    protected bool canDash = true;
    protected List<GameObject> listDash = new List<GameObject>();
    
    protected virtual void Start()
    {
        try
        {
            pc = GetComponent<PlayerController>().PC;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        pc.Controller.Attack.started += context => { StartedAttack(context); };
        pc.Controller.Attack.performed += context => { PerformedAttack(context); };
        pc.Controller.Attack.canceled += context => { CanceledAttack(context); };
        
        pc.Controller.Skill.started += context => { StartedSkill(context); };
        pc.Controller.Skill.performed += context => { PerformedSkill(context); };
        pc.Controller.Skill.canceled += context => { CanceledSkill(context); };
        
        pc.Controller.Burst.started += context => { StartedBurst(context); };
        pc.Controller.Burst.performed += context => { PerformedBurst(context); };
        pc.Controller.Burst.canceled += context => { CanceledBurst(context); };
        
        pc.Controller.Dash.started += context => { StartedDash(context); };
        pc.Controller.Dash.performed += context => { PerformedDash(context); };
        pc.Controller.Dash.canceled += context => { CanceledDash(context); };
    }

    protected void Update()
    {
        foreach (var var in listSkillCds)
        {
            var.Update();
        }
    }

    // _____________________________________ATTACK_____________________________________
    #region Các hàm của Attack
    // Gọi khi Input được kích hoạt
    protected virtual void StartedAttack(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            StartTapAttack();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldAttack)
        {
            StartHoldAttack();
        }
    }

    protected virtual void StartTapAttack()
    {
        if(debugAttack) Debug.Log("Start Tap Attack");
    }
    protected virtual void StartHoldAttack()
    {
        if(debugAttack) Debug.Log("Start Hold Attack");
    }
    
    // Gọi khi Input kích hoạt thành công
    protected virtual void PerformedAttack(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            EnterTapAttack();
            TapAttack();
            ExitTapAttack();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldAttack)
        {
            EnterHoldAttack();
            HoldAttack();
            ExitHoldAttack();
        }
    }
    
    protected virtual void EnterTapAttack()
    {
        if(debugAttack) Debug.Log("Enter Tap Attack");
    }
    protected virtual void EnterHoldAttack()
    {
        if(debugAttack) Debug.Log("Enter Hold Attack");
    }
    
    protected virtual void TapAttack()
    {
        if(debugAttack) Debug.Log("Tap Attack");
        EventManager.Player.OnPlayerAttack.Get("").Invoke(this, null);
    }
    protected virtual void HoldAttack()
    {
        if(debugAttack) Debug.Log("Hold Attack");
        EventManager.Player.OnPlayerAttack.Get("").Invoke(this, null);
    }
    
    protected virtual void ExitTapAttack()
    {
        if(debugAttack) Debug.Log("Exit Tap Attack");
    }
    protected virtual void ExitHoldAttack()
    {
        if(debugAttack) Debug.Log("Exit Hold Attack");
    }
    
    // Gọi khi Input thất bại
    protected virtual void CanceledAttack(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            CanceledTapAttack();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldAttack)
        {
            CanceledHoldAttack();
        }
    }

    protected virtual void CanceledTapAttack()
    {
        if(debugAttack) Debug.Log("Canceled Tap Attack");
    }
    protected virtual void CanceledHoldAttack()
    {
        if(debugAttack) Debug.Log("Canceled Hold Attack");
    }
    #endregion
    
    // _____________________________________SKILL_____________________________________
    #region Các hàm của Skill
    // Gọi khi Input được kích hoạt
    protected virtual void StartedSkill(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            StartTapSkill();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldSkill)
        {
            StartHoldSkill();
        }
    }

    protected virtual void StartTapSkill()
    {
        if(debugSkill) Debug.Log("Start Tap Skill");
    }
    protected virtual void StartHoldSkill()
    {
        if(debugSkill) Debug.Log("Start Hold Skill");
    }
    
    // Gọi khi Input kích hoạt thành công
    protected virtual void PerformedSkill(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            EnterTapSkill();
            TapSkill();
            ExitTapSkill();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldSkill)
        {
            EnterHoldSkill();
            HoldSkill();
            ExitHoldSkill();
        }
    }
    
    protected virtual void EnterTapSkill()
    {
        if(debugSkill) Debug.Log("Enter Tap Skill");
    }
    protected virtual void EnterHoldSkill()
    {
        if(debugSkill) Debug.Log("Enter Hold Skill");
    }
    
    protected virtual void TapSkill()
    {
        if(debugSkill) Debug.Log("Tap Skill");
        EventManager.Player.OnPlayerSkill.Get("").Invoke(this, null);
    }
    protected virtual void HoldSkill()
    {
        if(debugSkill) Debug.Log("Hold Skill");
        EventManager.Player.OnPlayerSkill.Get("").Invoke(this, null);
    }
    
    protected virtual void ExitTapSkill()
    {
        if(debugSkill) Debug.Log("Exit Tap Skill");
    }
    protected virtual void ExitHoldSkill()
    {
        if(debugSkill) Debug.Log("Exit Hold Skill");
    }
    
    // Gọi khi Input thất bại
    protected virtual void CanceledSkill(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            CanceledTapSkill();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldSkill)
        {
            CanceledHoldSkill();
        }
    }

    protected virtual void CanceledTapSkill()
    {
        if(debugSkill) Debug.Log("Canceled Tap Skill");
    }
    protected virtual void CanceledHoldSkill()
    {
        if(debugSkill) Debug.Log("Canceled Hold Skill");
    }
    #endregion
    
    // _____________________________________BURST_____________________________________
    #region Các hàm của Burst
    // Gọi khi Input được kích hoạt
    protected virtual void StartedBurst(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            StartTapBurst();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldBurst)
        {
            StartHoldBurst();
        }
    }

    protected virtual void StartTapBurst()
    {
        if(debugBurst) Debug.Log("Start Tap Burst");
    }
    protected virtual void StartHoldBurst()
    {
        if(debugBurst) Debug.Log("Start Hold Burst");
    }
    
    // Gọi khi Input kích hoạt thành công
    protected virtual void PerformedBurst(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            EnterTapBurst();
            TapBurst();
            ExitTapBurst();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldBurst)
        {
            EnterHoldBurst();
            HoldBurst();
            ExitHoldBurst();
        }
    }
    
    protected virtual void EnterTapBurst()
    {
        if(debugBurst) Debug.Log("Enter Tap Burst");
    }
    protected virtual void EnterHoldBurst()
    {
        if(debugBurst) Debug.Log("Enter Hold Burst");
    }
    
    protected virtual void TapBurst()
    {
        if(debugBurst) Debug.Log("Tap Burst");
        EventManager.Player.OnPlayerBurst.Get("").Invoke(this, null);
    }
    protected virtual void HoldBurst()
    {
        if(debugBurst) Debug.Log("Hold Burst");
        EventManager.Player.OnPlayerBurst.Get("").Invoke(this, null);
    }
    
    protected virtual void ExitTapBurst()
    {
        if(debugBurst) Debug.Log("Exit Tap Burst");
    }
    protected virtual void ExitHoldBurst()
    {
        if(debugBurst) Debug.Log("Exit Hold Burst");
    }
    
    // Gọi khi Input thất bại
    protected virtual void CanceledBurst(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            CanceledTapBurst();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldBurst)
        {
            CanceledHoldBurst();
        }
    }

    protected virtual void CanceledTapBurst()
    {
        if(debugBurst) Debug.Log("Canceled Tap Burst");
    }
    protected virtual void CanceledHoldBurst()
    {
        if(debugBurst) Debug.Log("Canceled Hold Burst");
    }
    #endregion
    
    // _____________________________________DASH_____________________________________
    #region Các hàm của Dash
    // Gọi khi Input được kích hoạt
    protected virtual void StartedDash(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            StartTapDash();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldDash)
        {
            StartHoldDash();
        }
    }

    protected virtual void StartTapDash()
    {
        if(debugDash) Debug.Log("Start Tap Dash");
    }
    protected virtual void StartHoldDash()
    {
        if(debugDash) Debug.Log("Start Hold Dash");
    }
    
    // Gọi khi Input kích hoạt thành công
    protected virtual void PerformedDash(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            EnterTapDash();
            TapDash();
            ExitTapDash();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldDash)
        {
            EnterHoldDash();
            HoldDash();
            ExitHoldDash();
        }
    }
    
    protected virtual void EnterTapDash()
    {
        if(debugDash) Debug.Log("Enter Tap Dash");
    }
    protected virtual void EnterHoldDash()
    {
        if(debugDash) Debug.Log("Enter Hold Dash");
    }
    
    protected virtual void TapDash()
    {
        if(debugDash) Debug.Log("Tap Dash");
        EventManager.Player.OnPlayerDash.Get("").Invoke(this,null);
    }
    protected virtual void HoldDash()
    {
        if(debugDash) Debug.Log("Hold Dash");
        EventManager.Player.OnPlayerDash.Get("").Invoke(this,null);
    }
    
    protected virtual void ExitTapDash()
    {
        if(debugDash) Debug.Log("Exit Tap Dash");
    }
    protected virtual void ExitHoldDash()
    {
        if(debugDash) Debug.Log("Exit Hold Dash");
    }
    
    // Gọi khi Input thất bại
    protected virtual void CanceledDash(InputAction.CallbackContext context)
    {
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            CanceledTapDash();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && canHoldDash)
        {
            CanceledHoldDash();
        }
    }

    protected virtual void CanceledTapDash()
    {
        if(debugDash) Debug.Log("Canceled Tap Dash");
    }
    protected virtual void CanceledHoldDash()
    {
        if(debugDash) Debug.Log("Canceled Hold Dash");
    }
    #endregion
}

