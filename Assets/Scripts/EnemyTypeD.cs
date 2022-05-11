using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy Type D wanders around for 6 seconds using the method covered in class before switching to Idle
// Idles at a postion for 3 seconds before returning to wander

public class EnemyTypeD : Enemy
{
    public bool wandering = true;
    public float wanderTimer = 6.0f;
    public float idleTimer = 3.0f;
    public float timer = 0.0f;

    public float distanceToCircle = 1.0f;
    public float circleRadius = 0.75f;

    public override Vector3 CalculateSteeringForce()
    {
        timer += Time.deltaTime;
        if (wandering)
        {
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[0];
            if (timer >= wanderTimer)
            {
                timer = 0.0f;
                wandering = false;
            }
            return Wander();
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[1];
            if (timer >= idleTimer)
            {
                timer = 0.0f;
                wandering = true;
            }
            return Idle();
        }
    }

    public Vector3 Wander()
    {
        float vehicleOrientation = rb.rotation.eulerAngles.y * Mathf.Deg2Rad;
        // calculate the cirle's center point
        Vector3 circlePoint = transform.position + new Vector3(Mathf.Cos(-vehicleOrientation), 0, Mathf.Sin(-vehicleOrientation)) * distanceToCircle;

        // calcualte a random spot on the circle's circumference
        float angle = Random.Range(0, Mathf.PI * 2);
        float x = Mathf.Sin(angle) * circleRadius;
        float z = Mathf.Cos(angle) * circleRadius;

        // the target point the wandering vehicle will seek towards
        Vector3 targetPosition = new Vector3(circlePoint.x + x, 0.5f, circlePoint.z + z);

        Debug.DrawLine(targetPosition, circlePoint);

        Vector3 direction = targetPosition - transform.position;

        Vector3 desiredVelocity = direction.normalized * maxSpeed;

        Vector3 steer = Vector3.ClampMagnitude(desiredVelocity - rb.velocity, maxForce);

        steer = steer / mass;

        return steer;
    }

    public Vector3 Idle()
    {
        rb.velocity = Vector3.zero;
        return Vector3.zero;
    }
}
