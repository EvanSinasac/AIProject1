using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaintballGun : MonoBehaviour
{
    public float range = 100.0f;
    public Camera fpsCamera;
    private Vector3 destination;

    public GameObject paintballPrefab;
    public Transform spawnPoint;
    public float projectileSpeed = 5.0f;
    public float rateOfFire = 2.0f;
    private float fireTime;

    private void Start()
    {
        fireTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && rateOfFire < fireTime)
        {
            FirePaintball();
            fireTime = 0.0f;
        }
        fireTime += Time.deltaTime;
    }

    void FirePaintball()
    {
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, range))
        {
            destination = hitInfo.point;
            Debug.Log("You have hit a target");
        }
        else
        {
            destination = ray.GetPoint(range);
            Debug.Log("You have missed the targets");
        }
        // spawn paintball
        GameObject projectile = Instantiate(paintballPrefab, spawnPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = (destination - spawnPoint.position).normalized * projectileSpeed;
    }
}
