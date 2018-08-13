using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerAudio : MonoBehaviour {

	public float audioCooldown = 2f;
	public float audioChance = 0.2f;

	private float timeOfLastAudio = 0;
	private AudioClip[] clips;

	// Use this for initialization
	void Start () {
		timeOfLastAudio = 2f * Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timeOfLastAudio > audioCooldown) {
			if (Random.value < audioChance) {
				AudioSource s = GetComponent<AudioSource>();
				s.clip = GameObject.Find("GameMaster")
					.GetComponent<ClipPool>()
					.GetRandomClip();
				s.Play();
			}

			timeOfLastAudio = Time.time;
		}
	}
}
