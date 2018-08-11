using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	GameObject train;

	// Use this for initialization
	void Start () {
		train = GameObject.FindGameObjectWithTag("Train");
		train.transform.position += new Vector3(-100f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (train.transform.position.x < -50f) {
			train.transform.position += new Vector3(Time.deltaTime * 10.0f, 0f, 0f);
		} else if (train.transform.position.x >= -50f && train.transform.position.x < -20f) {
			train.transform.position += new Vector3(Time.deltaTime * 5.0f, 0f, 0f);
		} else if (train.transform.position.x >= -20f && train.transform.position.x < -5f) {
			train.transform.position += new Vector3(Time.deltaTime * 2.5f, 0f, 0f);
		} else if (train.transform.position.x >= -5f && train.transform.position.x < 0f) {
			train.transform.position += new Vector3(Time.deltaTime * 1f, 0f, 0f);
		}
	}
}
