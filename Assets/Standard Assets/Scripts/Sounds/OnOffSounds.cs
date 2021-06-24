using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnOffSounds : MonoBehaviour {
	
	int temp = 0;
	public Button on, off, sounds;
	public Sprite [] soundSpr = new Sprite[2];
	public Sprite [] onSpr = new Sprite[2];
	public Sprite [] offSpr = new Sprite[2];
	// Use this for initialization
	void Start () {
		temp = PlayerPrefs.GetInt ("Sound");
	}
	
	// Update is called once per frame
	void Update () {
		on.GetComponent<Image> ().sprite = onSpr [temp];
		off.GetComponent<Image> ().sprite = offSpr [temp];
		sounds.GetComponent<Image> ().sprite = soundSpr [temp];
		PlayerPrefs.SetInt("Sound", temp);
	}

	public void SetSound() {
		if (temp == 0)
			temp = 1;
		else if (temp == 1)
			temp = 0;
	}

	public void SetSound(int i) {
		temp = i;
	}
}
