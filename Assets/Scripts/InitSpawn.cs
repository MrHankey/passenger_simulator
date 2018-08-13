using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpawn : MonoBehaviour {

    public GameObject enemy;
    public int count_x = 2;
    public int count_z = 2;
    private const float scale_factor = 0.5f; // standart size of plane is 10x10

	void Start () {
        Transform trans = GetComponent<Transform>();
        Vector3 pos = trans.position;
        Vector3 scale = trans.localScale * scale_factor;
        Quaternion rot = trans.rotation;
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

        Vector3 size = mesh.bounds.max - mesh.bounds.min;
        size = Vector3.Scale(size, transform.localScale * scale_factor);
        for (int i = 0; i < count_x; i++)
        {
            for (int j = 0; j < count_z; j++)
            {
                float x = pos.x + i * (size.x / count_x);// + (Random.value * 2 - 1);
                float y = pos.y + 1.0f;
                float z = pos.z + j * (size.z / count_z);// + (Random.value * 2 - 1);
                Instantiate(enemy, new Vector3(x, y, z), rot);
            }
        }
	}
}
