using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private PlayerControls controls;
    public float lightCooldown = 0.3f;
    public float heavyCooldown = 0.8f;

    [SerializeField]
    protected GameObject lightProjectile;
    [SerializeField]
    protected GameObject heavyProjectile;

    private bool lightPressed;
    private bool heavyPressed;
    private bool inCooldown;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.LightAttack.performed += ctx => {
            lightPressed = true;
        };

        controls.Gameplay.LightAttack.canceled += ctx => {
            lightPressed = false;
        };

        controls.Gameplay.HeavyAttack.performed += ctx => {
            heavyPressed = true;
        };

        controls.Gameplay.HeavyAttack.canceled += ctx => {
            heavyPressed = false;
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
        lightPressed = false;
        inCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(lightPressed && !inCooldown)
        {
            LightAttack ();
            inCooldown = true;
            StartCoroutine(Cooldown(lightCooldown));
        }

        if(heavyPressed && !inCooldown)
        {
            HeavyAttack ();
            inCooldown = true;
            StartCoroutine(Cooldown(heavyCooldown));
        }
    }

    void LightAttack ()
    {
        Instantiate(lightProjectile, transform.position, Quaternion.Euler(0, ShootDirection(), 0));
    }

    void HeavyAttack ()
    {
        Instantiate(heavyProjectile, transform.position, Quaternion.Euler(0, ShootDirection(), 0));
    }

    IEnumerator Cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        inCooldown = false;
    }

    protected float ShootDirection()
    {
        return -transform.localRotation.y * 180;
    }
}
