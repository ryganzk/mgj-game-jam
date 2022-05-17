using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPlatform : MonoBehaviour
{

    public float BreakawayTime = 1.5f;

    private float CurrentTimer = 0.0f;

    private bool Contact = false;

    void Update()
    {
        if(Contact)
        {
            CurrentTimer += Time.deltaTime;
            if(CurrentTimer >= BreakawayTime)
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
