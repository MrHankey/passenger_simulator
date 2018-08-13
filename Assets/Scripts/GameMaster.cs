using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	bool doorWarning = false;

	// Use this for initialization
	void Start () {
		status = TrainStatus.Incoming;
		startTime = Time.time;

		GameObject canvas = GameObject.FindGameObjectWithTag("UICanvas");
		if (StaticContainer.origin == "menu") {
			canvas.GetComponent<Canvas>().enabled = false;
		} else {
			canvas.GetComponent<Canvas>().enabled = true;
			GameObject.Find("Canvas/Result").GetComponent<Text>().enabled = false;
		}

		playerGoals = GameObject.FindGameObjectsWithTag("ZonePlayerGoal");
		foreach (GameObject goal in playerGoals) {
			goal.GetComponent<MeshRenderer>().enabled = false;
		}

		train = GameObject.FindGameObjectWithTag("Train");

		GameObject.Find("AmbientSound").GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float vel;

		switch (status)
		{
			case TrainStatus.Incoming:
				if (Time.time - startTime > 3f) {
					GameObject.Find("Canvas/NextDay").GetComponent<Text>().enabled = false;
				}

				vel = System.Math.Max(minVelocity, System.Math.Abs(train.transform.position.x));
                vel = System.Math.Min(vel, 30.0f);
				train.transform.position += new Vector3(Time.deltaTime * vel * velocityFactor, 0f, 0f);

				if (train.transform.position.x >= 0f) {
					startTime = Time.time;
					status = TrainStatus.DoorsOpening;
					foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door")) {
						door.GetComponent<DoorController>().OpenDoor();
						door.GetComponent<DoorAudio>().DoorOpening();
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
					foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door")) {
						door.GetComponent<DoorAudio>().DoorShutUp();
					}
				}
				break;

			case TrainStatus.Waiting:
				if (!doorWarning && Time.time - startTime > departureTime - 3f) {
					foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door")) {
						door.GetComponent<DoorAudio>().DoorWarning();
					}
					doorWarning = true;
				}

				if (Time.time - startTime > departureTime) {
					startTime = Time.time;
					status = TrainStatus.DoorsClosing;
					foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door")) {
						door.GetComponent<DoorController>().CloseDoor();
						door.GetComponent<DoorAudio>().DoorClosing();
					}
				}
				break;

			case TrainStatus.DoorsClosing:
				if (Time.time - startTime > 3f) {
					startTime = Time.time;
					status = TrainStatus.Outgoing;
					foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door")) {
						door.GetComponent<DoorAudio>().DoorShutUp();
					}
				}
				break;

			case TrainStatus.Outgoing:
				vel = System.Math.Max(minVelocity, 2f * System.Math.Abs(train.transform.position.x));
                vel = System.Math.Min(vel, 30.0f);
                train.transform.position += new Vector3(Time.deltaTime * vel * velocityFactor, 0f, 0f);

				if (Time.time - startTime > 7f) {
					GameObject canvas = GameObject.FindGameObjectWithTag("UICanvas");
					canvas.GetComponent<Canvas>().enabled = true;
					
					GameObject.Find("Canvas/NextDay")
						.GetComponent<Text>().enabled = false;

					GameObject text = GameObject.Find("Canvas/Result");
					text.GetComponent<Text>().enabled = true;
					text.GetComponent<Text>().text = "Better luck tomorrow";

					GameObject player = GameObject.FindGameObjectWithTag("Player");
					foreach (GameObject zone in playerGoals) {
						if (player.GetComponent<Collider>().bounds.Intersects(
							zone.GetComponent<Collider>().bounds
						)) {
							text.GetComponent<Text>().text = "Hooray, you made it";
							break;
						}
					}
				}

				if (Time.time - startTime > 10f) {
					StaticContainer.origin = "game";
					SceneManager.LoadScene("MainScene");
				}
				break;

			default:
				break;
		}
	}
}
