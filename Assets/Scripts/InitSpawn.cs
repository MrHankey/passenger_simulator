using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpawn : MonoBehaviour {

    public GameObject enemy;
    public GameObject aggroEnemy;
    public int count_x = 2;
    public int count_z = 2;
    private const float scale_factor = 1.0f; // standart size of plane is 10x10

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
                GameObject newEnemy;
                float x = pos.x + i * (size.x / count_x) - size.x / 2.0f + (Random.value * 2.0f - 1.0f)*0.5f;
                float y = pos.y + 1.5f;
                float z = pos.z + j * (size.z / count_z) - size.z/2.0f + (Random.value * 2.0f - 1.0f)*0.5f;
                if (Random.value > 0.2)
                {
                    newEnemy = Instantiate(enemy, new Vector3(x, y, z), rot);
                }
                else
                {
                    newEnemy = Instantiate(aggroEnemy, new Vector3(x, y, z), rot);
                }
                newEnemy.transform.localScale += new Vector3(0.0f, Random.value * 0.3f - 0.2f, 0.0f);
            }
        }
	}
}
