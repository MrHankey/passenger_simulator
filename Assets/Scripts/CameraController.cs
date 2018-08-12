using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    private float rotY;
    public float sensitivity = 2.5f;

	// Use this for initialization
	void Start () {
        //offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //transform.position = player.transform.position + offset;
        rotY += Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -89, 89);

        float rotX = transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(-rotY, rotX, 0.0f);
        //transform.rotation = Quaternion.Euler(rot);
    }
}
