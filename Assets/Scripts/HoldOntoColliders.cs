using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldOntoColliders : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
        //Vector3 scaleInv = new Vector3(1.0f / gameObject.transform.localScale.x, 1.0f / gameObject.transform.localScale.y, 1.0f / gameObject.transform.localScale.z);
        //col.transform.localScale = Vector3.Scale(col.transform.localScale, scaleInv);
        GameObject newObj = new GameObject();
        newObj.transform.parent = gameObject.transform;
        col.transform.parent = newObj.transform;
        
	}

	void OnTriggerExit(Collider col) {
		col.transform.parent = null;
	}

}
