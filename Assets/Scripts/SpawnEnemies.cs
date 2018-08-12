using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public GameObject enemy;
    public Transform[] spawn_points;
    public float start_time;
    public float interval;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Spawn", start_time, interval);
    }

    // Update is called once per frame
    void Spawn () {
        int index = Random.Range(0, spawn_points.Length);

        Vector3    pos = spawn_points[index].position;
        Quaternion rot = spawn_points[index].rotation;
        pos.x += (Random.value * 2.0f - 1.0f) * GetComponent<Transform>().localScale.x;
        pos.z += (Random.value * 2.0f - 1.0f) * GetComponent<Transform>().localScale.z;
        Instantiate(enemy, pos, rot);
    }
}
