using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enemy Type B pursues the player and evades the closest bullet fired by the player
// The way I will handle this is by checking all the currently existing paintballs fired by the player
// If the closest one is within, let's say 10 units, the unit will begin to evade
// Any closer and it will only try to evade when the bullet is about to hit it, and any farther it might try to avoid every bullet

public class EnemyTypeB : Enemy
{
    public float evadeDistance = 10.0f;
    private float closestDistance;
    private Transform evadeTarget;

    public float T_maximumsteps = 10.0f;

    private Transform playerTransform;
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
        closestDistance = 10000.0f;
        foreach (GameObject paintball in GameObject.FindGameObjectsWithTag("Paintball"))
        {
            float dist = (paintball.transform.position - transform.position).magnitude;
            if (dist < closestDistance)
            {
                evadeTarget = paintball.transform;
                closestDistance = dist;
            }
        }
        if (closestDistance < evadeDistance)
        {
            // Set the material
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[1];
            SetTarget(evadeTarget);
            return Evade();
        }
        else
        {
            // Set the material
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[0];
            SetTarget(playerTransform);
            return Pursue();
        }
    }

    public Vector3 Pursue()
    {
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;

        float speed = rb.velocity.magnitude;

        float T;        // = distance / speed   // No limit, very accurate predictions

        if (speed <= distance / T_maximumsteps)
        {
            T = T_maximumsteps;     // target is far away
        }
        else
        {
            T = distance / speed;
        }

        //float T = T_maximumsteps;
        // calculate the future position the vehicle will pursue towards
        Vector3 futurePosition = target.position + target.gameObject.GetComponent<Rigidbody>().velocity * T;

        // Now we seek towards this future position
        Vector3 directionToFuturePosition = futurePosition - transform.position;

        Vector3 desiredVelocity = directionToFuturePosition.normalized * maxSpeed;

        Vector3 steer = Vector3.ClampMagnitude(desiredVelocity - rb.velocity, maxForce);

        steer = steer / mass;

        return steer;
    }

    public Vector3 Evade()
    {
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;

        float speed = rb.velocity.magnitude;

        float T;        // = distance / speed   // No limit, very accurate predictions

        if (speed <= distance / T_maximumsteps)
        {
            T = T_maximumsteps;     // target is far away
        }
        else
        {
            T = distance / speed;
        }

        //float T = T_maximumsteps;
        // calculate the future position the vehicle will pursue towards
        Vector3 futurePosition = target.position + target.gameObject.GetComponent<Rigidbody>().velocity * T;

        // Now we seek towards this future position
        Vector3 directionToFuturePosition = transform.position - futurePosition;

        Vector3 desiredVelocity = directionToFuturePosition.normalized * maxSpeed;

        Vector3 steer = Vector3.ClampMagnitude(desiredVelocity - rb.velocity, maxForce);

        steer = steer / mass;

        return steer;
    }
}
