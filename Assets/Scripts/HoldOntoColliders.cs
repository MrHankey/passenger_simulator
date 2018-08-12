using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldOntoColliders : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		col.transform.parent.position = gameObject.transform.position;
	}

	void OnTriggerExit(Collider col) {
		col.transform.parent = null;
	}

}
