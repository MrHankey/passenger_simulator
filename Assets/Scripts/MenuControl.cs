using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

	public float changeChance = 0.01f;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	public Sprite sprite5;
	public Sprite sprite6;
	public Sprite sprite7;
	public Sprite sprite8;

	private int spriteState = 1;

	// Use this for initialization
	void Start () {
		GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = sprite1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
			LoadLevel();
		}

		if (Random.value < changeChance) {
			spriteState += 1;

			switch(spriteState) {
				case 2:
					GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = sprite2;
					break;
				case 3:
					GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = sprite3;
					break;
				case 4:
					GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = sprite4;
					break;
				case 5:
					GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = sprite5;
					break;
				case 6:
					GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = sprite6;
					break;
				case 7:
					GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = sprite7;
					break;
				case 8:
					GameObject.Find("Canvas/Image").GetComponent<Image>().sprite = sprite8;
					break;
				default:
					break;
			}
		}
	}

	public void LoadLevel() {
		SceneManager.LoadScene("MainScene");
	}
}
