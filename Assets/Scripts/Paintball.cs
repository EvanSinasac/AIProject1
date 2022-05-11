using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintball : MonoBehaviour
{
    float lifespan = 5.0f;
    float deathClock;

    // Start is called before the first frame update
    void Start()
    {
        deathClock = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathClock > lifespan)
        {
            Destroy(gameObject);
        }
        deathClock += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Bullet hit");
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy Type A"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // seek
        Destroy(gameObject);
    }

    private void OnBecameVisible()
    {
        // flee
    }
}
