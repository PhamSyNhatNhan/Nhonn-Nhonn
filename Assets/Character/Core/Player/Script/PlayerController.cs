using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected PlayerControls pc;
    protected Stat stat;

    [Header("Movement")]
    [SerializeField] protected float speed = 200.0f;
    protected Vector2 _Move;
    protected int flipDirect = 1;
    private bool canMove = true;
    private bool canFlip = true;
    
    private bool isMoveTo = false;
    [SerializeField]private float velocityMoveToDefault = 1200.0f;
    [SerializeField]private float timeMoveToDefault = 0.1f;
    private float velocityMoveTo = 500.0f;
    private float timeMoveTo = 0.15f;
    private Vector2 moveSnap = Vector2.right;
    

    protected virtual void Awake()
    {
        pc = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        stat = GetComponent<Stat>();
        AwakeSetUp();
    }
    protected virtual void AwakeSetUp(){}

    protected void OnEnable()
    {
        pc.Enable();
    }

    protected void OnDisable()
    {
        pc.Disable();
    }

    protected virtual void Start()
    {
        StartSetUp();
    }
    protected virtual void StartSetUp(){}
    
    protected virtual void Update()
    {
        CheckInput();
        CheckFlip();
        UpdateSetUp();
    }
    protected virtual void UpdateSetUp(){}

    protected virtual void FixedUpdate()
    {
        Movement();
        FixedUpdateSetUp();
    }
    protected virtual void FixedUpdateSetUp(){}

    

    protected virtual void Movement()
    {
        if (canMove && !isMoveTo)
        {
            rb.linearVelocity = new Vector2(_Move.x * speed * Time.deltaTime, _Move.y * speed * Time.deltaTime);
            if (_Move != Vector2.zero)
            {
                moveSnap.x = _Move.x;
                moveSnap.y = _Move.y;
            }
        }
        else if (canMove && isMoveTo)
        {
            timeMoveTo -= Time.deltaTime;
            rb.linearVelocity = new Vector2(moveSnap.x * velocityMoveTo * Time.deltaTime, moveSnap.y * velocityMoveTo * Time.deltaTime);
            if (timeMoveTo <= 0)
            {
                isMoveTo = false;
                EventManager.Player.OnMoveToEnd.Get(stat.NameCharacter).Invoke(this, null);
            }
        }
        else 
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public virtual void MoveTo()
    {
        rb.linearVelocity = Vector2.zero;
        isMoveTo = true;
        velocityMoveTo = velocityMoveToDefault;
        timeMoveTo = timeMoveToDefault;
    }
    
    public virtual void MoveTo(float velocity, float time)
    {
        rb.linearVelocity = Vector2.zero;
        isMoveTo = true;
        velocityMoveTo = velocity;
        timeMoveTo = time;
    }
    

    public void stopMovement()
    {
        rb.linearVelocity = Vector2.zero;
    }

    protected virtual void CheckInput()
    {
        _Move = pc.Controller.Move.ReadValue<Vector2>();
    }

    protected virtual void CheckFlip()
    {
        if (canFlip && flipDirect > 0 && _Move.x < 0.0f)
        {
            Flipping();
        }
        else if (canFlip && flipDirect < 0 && _Move.x > 0.0f)
        {
            Flipping();
        }
        
    }
    
    protected virtual void Flipping()
    {
        flipDirect *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    
    public virtual void Flipping(Transform enemy)
    {
        if (transform.position.x < enemy.position.x && flipDirect == -1)
        {
            Flipping();
        }
        else if (transform.position.x > enemy.position.x && flipDirect == 1)
        {
            Flipping();
        }
    }

    public PlayerControls PC
    {
        get => pc;
        private set => pc = value;
    }

    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    public bool CanFlip
    {
        get => canFlip;
        set => canFlip = value;
    }

    public Rigidbody2D Rb
    {
        get => rb;
        set => rb = value;
    }
    
}
