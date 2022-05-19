using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovementController : MonoBehaviour
{
    private PlayerControls controls;

    private Vector2 move = new Vector2();

    public float speed = 5f;
    public float jumpVelocity = 7f;
    public float jumpLength = 0.3f;

    private Rigidbody2D rb; 
    private Collider2D playerCollider;
    [SerializeField]
    private LayerMask groundLayerMask;
    private float gravity;
    private float jumpTimer;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => {
            float jumpPower = jumpVelocity;
            Jump(jumpPower);
        };

        controls.Gameplay.Jump.canceled += ctx => {
            StopJump();
        };

        controls.Gameplay.Move.performed += ctx => {
            move = ctx.ReadValue<Vector2>();
        };

        controls.Gameplay.Move.canceled += ctx => {
            move = Vector2.zero;
        };
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        gravity = rb.gravityScale;
        jumpTimer = jumpLength;
    }

    private void Update()
    {
        if (!IsGrounded())
        {
            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0f)
            {
                StopJump();
            }
        }
    }

    private void FixedUpdate()
    {
        float x = move.x * speed  * Time.deltaTime * 80;
        rb.velocity = new Vector2(x, rb.velocity.y);
    }

    private void Jump(float jumpPower)
    {
        if(IsGrounded())
        {
            rb.gravityScale = 0f;
            rb.velocity = rb.velocity + Vector2.up * jumpPower;
        }
    }

    private void StopJump()
    {
        rb.gravityScale = gravity;
    }

    private bool IsGrounded()
    {  
        if (rb.velocity.y == 0)
        {
            jumpTimer = jumpLength;
            return true;
        }
        return false;
    }
}
