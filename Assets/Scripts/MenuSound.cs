using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour {
    public AudioClip tickingSound;
    public AudioClip theme;
    private AudioSource audioSource;
    public bool startTheme = false;
    private bool themeStarted = false;

    private float tickingLength;

    public static class AudioFadeOut
    {

        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

    }

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        tickingLength = tickingSound.length;
        audioSource.clip = tickingSound;
        audioSource.loop = true;
        audioSource.Play();
	}

    void StartTheme()
    {
        startTheme = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (startTheme && !themeStarted)
        {
            if (audioSource.time >= tickingLength - 0.1f)
            {
                audioSource.clip = theme;
                audioSource.Play();
                themeStarted = false;
                
            }
        }
	}
}
