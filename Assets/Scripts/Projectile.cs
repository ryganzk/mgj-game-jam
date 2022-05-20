using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 3.0f;
    public float lifespan = 3.0f;

    private GameObject obj; 

    // Start is called before the first frame update
    void Start()
    {
        obj = this.GetComponent<GameObject>();
        StartCoroutine(LifeTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    IEnumerator LifeTime ()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(this.gameObject);
    }
}
