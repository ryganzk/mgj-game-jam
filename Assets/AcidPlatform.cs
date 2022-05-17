using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPlatform : MonoBehaviour
{

    public float breakawayTime = 1.5f;

    private float currentTimer = 0.0f;

    private bool Contact = false;

    void Update()
    {
        if(Contact)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer >= breakawayTime)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Acid"))
        {
            Contact = true;
        }
    }
}
