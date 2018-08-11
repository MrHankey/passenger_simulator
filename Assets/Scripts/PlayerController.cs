using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float rotateHorizontal = Input.GetAxis("Mouse X") * 5.0f;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Vector3 movement_scaled = transform.TransformDirection(movement);

        //transform.rotation.y += rotateHorizontal;
        transform.Rotate(new Vector3(0.0f, rotateHorizontal, 0.0f));

        rb.AddForce(movement_scaled.normalized * speed);
    }
}
