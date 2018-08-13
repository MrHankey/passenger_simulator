using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerAudio : MonoBehaviour {

	public float audioCooldown = 2f;
	public float idleAudioChance = 0.2f;
	public float bumpAudioChance = 0.1f;
	public float aggroAudioChance = 0.3f;

	private float timeOfLastAudio = 0;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		timeOfLastAudio = 2f * Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timeOfLastAudio > audioCooldown) {
			if (Random.value < idleAudioChance) {
				source.clip = GameObject.Find("GameMaster")
					.GetComponent<ClipPool>()
					.GetRandomIdleClip();
				source.Play();
			}

			timeOfLastAudio = Time.time;
		}
	}

	public void CheckBump() {
		if (source.isPlaying || Time.time - timeOfLastAudio < audioCooldown) {
			return;
		}

		if (Random.value < bumpAudioChance) {
			source.clip = GameObject.Find("GameMaster")
				.GetComponent<ClipPool>()
				.GetRandomBumpClip();
			source.Play();
			timeOfLastAudio = Time.time;
		}
	}

	public void CheckAggro() {
		if (source.isPlaying || Time.time - timeOfLastAudio < audioCooldown) {
			return;
		}

		if (Random.value < aggroAudioChance) {
			source.clip = GameObject.Find("GameMaster")
				.GetComponent<ClipPool>()
				.GetRandomAggroClip();
			source.Play();
			timeOfLastAudio = Time.time;
		}
	}
}
