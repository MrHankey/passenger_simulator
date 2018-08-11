using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        //offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //transform.position = player.transform.position + offset;
        float rotationY = Input.GetAxis("Mouse Y") * 5.0f;
        rotationY *= -1.0f;
        //rotation = Mathf.Clamp(rotationY, -90, 90);

        transform.Rotate(new Vector3(rotationY, 0.0f, 0.0f));
    }
}
