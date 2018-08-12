using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float maxSpeed = 5.0f;
    public float maxForce = 10.0f;
    public float maxAccel = 5.0f;
    public float sensitivity = 2.5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= maxSpeed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        //velocityChange.x = Mathf.Clamp(velocityChange.x, -maxAccel, maxAccel);
        //velocityChange.z = Mathf.Clamp(velocityChange.z, -maxAccel, maxAccel);
        //velocityChange.y = 0;

        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        float rotateHorizontal = Input.GetAxis("Mouse X") * sensitivity;

        //transform.rotation.y += rotateHorizontal;
        transform.Rotate(new Vector3(0.0f, rotateHorizontal, 0.0f));

    }
}
