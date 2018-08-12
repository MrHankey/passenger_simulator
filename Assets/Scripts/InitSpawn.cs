using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpawn : MonoBehaviour {

    public GameObject enemy;
    public int count;
    private const float scale_factor = 5f; // standart size of plane is 10x10

	void Start () {
        Transform trans = GetComponent<Transform>();
        Vector3 pos = trans.position;
        Vector3 scale = trans.localScale * scale_factor;
        Quaternion rot = trans.rotation;
        for (int i = 0; i < count; i++) {
            float x = pos.x + (Random.value * 2 - 1) * scale.x;
            float y = pos.y + 1.0f;
            float z = pos.z + (Random.value * 2 - 1) * scale.z;
            GameObject o = Instantiate(enemy, new Vector3(x,y,z), rot);
        }
	}
}
