﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	enum TrainStatus {
		Incoming,
		DoorsOpening,
		Waiting,
		DoorsClosing,
		Outgoing
	}

	public float minVelocity = 0.3f;
	public float velocityFactor = 0.2f;
	public float startPos = -100f;
	public float departureTime = 30f;

	GameObject train;
	TrainStatus status;
	GameObject[] playerGoals;
	float startTime;

	// Use this for initialization
	void Start () {
		status = TrainStatus.Incoming;

		GameObject canvas = GameObject.FindGameObjectWithTag("UICanvas");
		canvas.GetComponent<Canvas>().enabled = false;

		playerGoals = GameObject.FindGameObjectsWithTag("ZonePlayerGoal");
		foreach (GameObject goal in playerGoals) {
			goal.GetComponent<MeshRenderer>().enabled = false;
		}

		train = GameObject.FindGameObjectWithTag("Train");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float vel;

		switch (status)
		{
			case TrainStatus.Incoming:
				vel = System.Math.Max(minVelocity, System.Math.Abs(train.transform.position.x));
                vel = System.Math.Min(vel, 30.0f);
				train.transform.position += new Vector3(Time.deltaTime * vel * velocityFactor, 0f, 0f);

				if (train.transform.position.x >= 0f) {
					startTime = Time.time;
					status = TrainStatus.DoorsOpening;
					foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door")) {
						door.GetComponent<DoorController>().OpenDoor();
					}
                    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
                    {
                        enemy.GetComponent<passenger>().SearchDoor();
                    }
				}
				break;

			case TrainStatus.DoorsOpening:
				if (Time.time - startTime > 3f) {
					startTime = Time.time;
					status = TrainStatus.Waiting;
				}
				break;

			case TrainStatus.Waiting:
				if (Time.time - startTime > departureTime) {
					startTime = Time.time;
					status = TrainStatus.DoorsClosing;
					foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door")) {
						door.GetComponent<DoorController>().CloseDoor();
					}
				}
				break;

			case TrainStatus.DoorsClosing:
				if (Time.time - startTime > 3f) {
					startTime = Time.time;
					status = TrainStatus.Outgoing;
				}
				break;

			case TrainStatus.Outgoing:
				vel = System.Math.Max(minVelocity, 2f * System.Math.Abs(train.transform.position.x));
                vel = System.Math.Min(vel, 30.0f);
                train.transform.position += new Vector3(Time.deltaTime * vel * velocityFactor, 0f, 0f);

				if (Time.time - startTime > 10f) {
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
				break;

			default:
				break;
		}
	}
}
