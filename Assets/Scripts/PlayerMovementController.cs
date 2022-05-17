using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovementController : MonoBehaviour
{

    private PlayerControls controls;

    private Vector2 move = new Vector2();

    public float speed;
    public float bigJumpVelocity;
    public float smallJumpVelocity;
    private Rigidbody2D rb; 
    private Collider2D playerCollider;
    [SerializeField]
    private LayerMask groundLayerMask;

    private void Awake() {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => {
            float jumpPower = smallJumpVelocity;
            Jump(jumpPower);
        };

        controls.Gameplay.Jump.canceled += ctx => {
            float jumpPower = smallJumpVelocity;
            Jump(jumpPower);
        };

        controls.Gameplay.Move.performed += ctx => {
            move = ctx.ReadValue<Vector2>();
        };

        controls.Gameplay.Move.canceled += ctx => {
            move = Vector2.zero;
        };
    }

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    private void OnDisable() {
        controls.Gameplay.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
        float x = move.x * speed  * Time.deltaTime * 80;
        rb.velocity = new Vector2(x, rb.velocity.y);
    }

    private void Jump(float jumpPower)
    {
        if(isGrounded())
            rb.velocity = rb.velocity + Vector2.up * jumpPower;
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayerMask);
        return hit.collider != null;
    }
}
