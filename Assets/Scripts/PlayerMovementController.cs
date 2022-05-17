using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovementController : MonoBehaviour
{

    private PlayerControls Controls;

    private Vector2 Move = new Vector2();

    public float Speed;
    public float BigJumpVelocity;
    public float SmallJumpVelocity;
    private Rigidbody2D Rb; 
    private Collider2D PlayerCollider;
    [SerializeField]
    private LayerMask GroundLayerMask;

    private void Awake() {
        Controls = new PlayerControls();

        Controls.Gameplay.Jump.performed += ctx => {
            float jumpPower = SmallJumpVelocity;
            Jump(jumpPower);
        };

        Controls.Gameplay.Jump.canceled += ctx => {
            float jumpPower = SmallJumpVelocity;
            Jump(jumpPower);
        };

        Controls.Gameplay.Move.performed += ctx => {
            Move = ctx.ReadValue<Vector2>();
        };

        Controls.Gameplay.Move.canceled += ctx => {
            Move = Vector2.zero;
        };
    }

    private void OnEnable() {
        Controls.Gameplay.Enable();
    }

    private void OnDisable() {
        Controls.Gameplay.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
        float x = Move.x * Speed  * Time.deltaTime * 80;
        Rb.velocity = new Vector2(x, Rb.velocity.y);
    }

    private void Jump(float jumpPower)
    {
        if(isGrounded())
            Rb.velocity = Rb.velocity + Vector2.up * jumpPower;
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(PlayerCollider.bounds.center, PlayerCollider.bounds.size, 0f, Vector2.down, 0.1f, GroundLayerMask);
        return hit.collider != null;
    }
}
