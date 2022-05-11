using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy Type C approaches the player and maintains a radius
// If in the specified range, face the player and fire a projectile...
// I was thinking about using the obstacle avoidance to detect when the enemy reaches the player...
// But on second thought, it's going to be much easier to use seek and use the arrive and target radius
// to stop and maintain the distance, and shoot projectiles only when we are within the arrive radius
// Then I realized, this is supposed to use the Approach behaviour, which is just a modification of seek

public class EnemyTypeC : Enemy
{
    public float arriveRadius = 8.0f;
    public float targetRadius = 5.0f;

    public GameObject paintball;
    public Transform spawnPoint;
    public float projectileSpeed = 5.0f;
    public float rateOfFire = 2.0f;
    private float fireTime;

    private Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.Find("FPS Player").transform;
        if (playerTransform != null /*&& seekUnit != null*/)
        {
            SetTarget(playerTransform);
        }
        fireTime = 0.0f;
    }

    public override Vector3 CalculateSteeringForce()
    {
        fireTime += Time.deltaTime;
        return Seek();
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
            FireProjectile(direction);
            return Vector3.zero;
        }

        gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[0];

        // Apply approach behaviour
        if (distance < arriveRadius)
        {
            desiredVelocity *= (distance / arriveRadius);
        }

        Vector3 steer = Vector3.ClampMagnitude(desiredVelocity - rb.velocity, maxForce);

        steer = steer / mass;

        return steer;
    }


    public void FireProjectile(Vector3 direction)
    {
        // First, turn towards player
        LookAtDirection(direction);

        gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[1];

        // Then fire bullet at player
        if (fireTime > rateOfFire)
        {
            fireTime = 0.0f;
            GameObject projectile = Instantiate(paintball, spawnPoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = (direction/* - spawnPoint.position*/).normalized * projectileSpeed;
        }
        return;
    }
}
