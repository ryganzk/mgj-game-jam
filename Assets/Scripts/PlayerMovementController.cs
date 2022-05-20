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

    protected Rigidbody2D rb; 
    private Collider2D playerCollider;
    [SerializeField]
    private LayerMask groundLayerMask;
    private float gravity;
    private float jumpTimer;
    private bool jumpPress;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => {
            jumpPress = true;
        };

        controls.Gameplay.Jump.canceled += ctx => {
            StopJump();
            jumpPress = false;
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
        jumpPress = false;
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

        if (jumpPress)
        {
            Jump();
        }

        DetermineDirection();
    }

    private void Jump()
    {
        if(IsGrounded())
        {
            rb.gravityScale = 0f;
            rb.velocity = rb.velocity + Vector2.up * jumpVelocity;
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

    protected float DetermineDirection()
    {
        if (rb.velocity.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            return -1f;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            return 1f;
        }
        return -((transform.localRotation.y - 90) / 90);
    }

    protected Rigidbody2D GetPlayerRigidbody()
    {
        Debug.Log("ok!");
        return rb;
    }
}
