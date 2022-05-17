using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterChildBehavior : MonoBehaviour
{
    public float MaxAllowedDistance;
    public float PullSpeed;
    private GameObject PlayerParent;
    // Start is called before the first frame update
    void Start()
    {
        PlayerParent = transform.parent.gameObject;      
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, PlayerParent.transform.position, PullSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, PlayerParent.transform.position) >= MaxAllowedDistance)
        {
            transform.position = PlayerParent.transform.position;
        }
    }
}
