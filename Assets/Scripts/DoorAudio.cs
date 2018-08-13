using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudio : MonoBehaviour {

	public AudioClip doorBeeps;
	public AudioClip doorOpening;
	public AudioClip doorClosing;

	public void DoorWarning() {
		GetComponent<AudioSource>().clip = doorBeeps;
		GetComponent<AudioSource>().Play();
	}

	public void DoorOpening() {
		GetComponent<AudioSource>().clip = doorOpening;
		GetComponent<AudioSource>().Play();
	}

	public void DoorClosing() {
		GetComponent<AudioSource>().clip = doorClosing;
		GetComponent<AudioSource>().Play();
	}

	public void DoorShutUp() {
		GetComponent<AudioSource>().Pause();
	}
}
