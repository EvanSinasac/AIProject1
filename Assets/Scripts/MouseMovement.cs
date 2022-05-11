using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 10.0f;
    public Transform playerTransform;  //reference to the parent game object for looking left/right
    public float xRotation;

    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate around y-axis when mouse moves left or right
        float xMovement = Input.GetAxis("Mouse X") * mouseSensitivity;
        playerTransform.Rotate(Vector3.up, xMovement);
        //rotate around y-axis when mouse moves up or down
        float yMovement = Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRotation += yMovement;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
        transform.localRotation = Quaternion.Euler(-xRotation, 0.0f, 0.0f);

        if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity, mask))
        {
            var obj = hit.collider.gameObject;
            //Debug.Log($"looking at {obj.name}", this);
            if (obj.CompareTag("Enemy Type A"))
            {
                obj.GetComponent<EnemyTypeA>().seeking = false;
            }
        }

    }
}
