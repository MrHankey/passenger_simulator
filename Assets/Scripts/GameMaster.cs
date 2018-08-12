﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	public float minVelocity = 0.3f;
	public float velocityFactor = 0.2f;
	public float startPos = -100f;
	public float departureTime = 30f;

	GameObject train;
	GameObject[] playerGoals;
	float startTime;

	// Use this for initialization
	void Start () {
		startTime = -1f;

		GameObject canvas = GameObject.FindGameObjectWithTag("UICanvas");
		canvas.GetComponent<Canvas>().enabled = false;

		playerGoals = GameObject.FindGameObjectsWithTag("ZonePlayerGoal");
		foreach (GameObject goal in playerGoals) {
			goal.GetComponent<MeshRenderer>().enabled = false;
		}

		train = GameObject.FindGameObjectWithTag("Train");
		train.transform.position += new Vector3(startPos, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		float vel = System.Math.Max(minVelocity, System.Math.Abs(train.transform.position.x));

		if (startTime < 0f) {
			train.transform.position += new Vector3(
				Time.deltaTime * vel * velocityFactor,
				0f,
				0f
			);

			if (train.transform.position.x >= 0f) {
				startTime = Time.time;
			}
		}

		if (Time.time - startTime > departureTime) {
			train.transform.position += new Vector3(
				Time.deltaTime * 2 * vel * velocityFactor,
				0f,
				0f
			);
		}

		if (Time.time - startTime > departureTime + 10f) {
			GameObject canvas = GameObject.FindGameObjectWithTag("UICanvas");
			canvas.GetComponent<Canvas>().enabled = true;

			GameObject text = GameObject.Find("Canvas/Result");
			text.GetComponent<Text>().text = "Loss";

			GameObject player = GameObject.FindGameObjectWithTag("Player");
			foreach (GameObject zone in playerGoals) {
				if (player.GetComponent<Collider>().bounds.Intersects(
					zone.GetComponent<Collider>().bounds
				)) {
					text.GetComponent<Text>().text = "Win";
					break;
				}
			}
		}
	}
}
