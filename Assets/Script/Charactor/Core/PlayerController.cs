using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected PlayerControls pc;

    [Header("Movement")]
    [SerializeField] protected float speed = 200.0f;
    protected Vector2 _Move;
    protected int flipDirect = 1;
    private bool canMove = true;
    private bool canFlip = true;

    protected void Awake()
    {
        pc = new PlayerControls();
    }

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
        rb = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Update()
    {
        CheckInput();
        CheckFlip();
    }

    protected virtual void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(_Move.x * speed * Time.deltaTime, _Move.y * speed * Time.deltaTime);
        }
        else 
        {
            rb.linearVelocity = Vector2.zero;
        }
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
