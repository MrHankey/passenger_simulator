using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	enum DoorStatus {
		Closed,
		Opening,
		Opened,
		Closing
	}

	public bool isLeft;
	public float openingTime;

	DoorStatus status;
	Vector3 target;

	// Use this for initialization
	void Start () {
		status = DoorStatus.Closed;
	}
	
	// Update is called once per frame
	void Update () {
		switch (status)
		{
			case DoorStatus.Closed:
				break;

			case DoorStatus.Opening:
				gameObject.transform.position += new Vector3(
					(isLeft ? -1f : 1f) * Time.deltaTime / openingTime, 0f, 0f
				);
				if (System.Math.Abs(gameObject.transform.position.x - target.x) < 0.05) {
					gameObject.transform.position = target;
					status = DoorStatus.Opened;
				}
				break;

			case DoorStatus.Opened:
				break;

			case DoorStatus.Closing:
				gameObject.transform.position += new Vector3(
					(isLeft ? 1f : -1f) * Time.deltaTime / openingTime, 0f, 0f
				);
				if (System.Math.Abs(gameObject.transform.position.x - target.x) < 0.05) {
					gameObject.transform.position = target;
					status = DoorStatus.Closed;
				}
				break;

			default:
				break;
		}
	}

	public void OpenDoor() {
		target = gameObject.transform.position + new Vector3(isLeft ? -1f : 1f, 0, 0);
		status = DoorStatus.Opening;
	}

	public void CloseDoor() {
		target = gameObject.transform.position + new Vector3(isLeft ? 1f : -1f, 0, 0);
		status = DoorStatus.Closing;
	}
}
