using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    public float gravity;

    public bool grounded;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected Vector2 lastPos;

    void OnEnable ()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    // Start is called before the first frame update
    void Start ()
    {
        grounded = false;
    }

    // Update is called once per frame
    void Update ()
    {

    }

    void FixedUpdate ()
    {
        velocity += gravity * Physics2D.gravity * Time.deltaTime;
        Vector2 position = velocity * Time.deltaTime;
        Vector2 move = Vector2.up * position.y;
        Movement (move);
    }

    void Movement (Vector2 move)
    {
        rb2d.position += move;
        if (lastPos.y == rb2d.position.y)
        {
            grounded = true;
        }
        lastPos = rb2d.position;
    }
}
