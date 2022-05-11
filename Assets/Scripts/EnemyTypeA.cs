using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyTypeA Seeks the player if the player is facing away from this enemy
// and Flees from the player if the player is facing towards this enemy
// I will handle this by using a RayCast from the camera and if it hits this type of enemy they will begin to
// flee from the player (this isn't exact, or rather it's almost too exact, but figuring out orientation another way just sounds painful lol)

public class EnemyTypeA : Enemy
{
    public float arriveRadius = 0.0f;
    public float targetRadius = 0.0f;
    public bool seeking = true;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("FPS Player").transform;
        if (playerTransform != null /*&& seekUnit != null*/)
        {
            SetTarget(playerTransform);
        }
    }

    public override Vector3 CalculateSteeringForce()
    {
        if (seeking)
        {
            // Set the material
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[0];
            return Seek();
        }
        else
        {
            // Set the material
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[1];
            seeking = true;
            return Flee();
        }
    }

    public Vector3 Seek()
    {
        // Calculate the direction
        // Seek uses target position - current position
        Vector3 direction = target.position - transform.position;
        Vector3 desiredVelocity = direction.normalized * maxSpeed;

        // Approach Behaviour
        // Calculate the distance
        float distance = direction.magnitude;

        // If we are in the stopping radius then stop
        if (distance < targetRadius)
        {
            rb.velocity = Vector3.zero;
            return Vector3.zero;
        }

        // Apply approach behaviour
        if (distance < arriveRadius)
        {
            desiredVelocity *= (distance / arriveRadius);
        }

        Vector3 steer = Vector3.ClampMagnitude(desiredVelocity - rb.velocity, maxForce);

        steer = steer / mass;

        return steer;
    }

    public Vector3 Flee()
    {
        // Calculate the direction
        // Flee uses current position - target position
        Vector3 direction = transform.position - target.position;
        Vector3 desiredVelocity = direction.normalized * maxSpeed;

        Vector3 steer = Vector3.ClampMagnitude(desiredVelocity - rb.velocity, maxForce);

        steer = steer / mass;

        return steer;
    }
}
