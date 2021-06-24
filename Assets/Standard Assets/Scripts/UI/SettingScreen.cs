using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingScreen : MonoBehaviour {

	public Slider speed;
	public Sprite[] cSpr = new Sprite[4];
	public Sprite[] dSpr = new Sprite[6];
	public Button[] controls = new Button[2];
	public Button[] difficulty = new Button[3];
	// Use this for initialization
	void Start () {
		speed.value = (PlayerPrefs.GetFloat ("speed")) / 8f;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("controls") == 1) {
			controls[0].image.sprite = cSpr[0];
			controls[1].image.sprite = cSpr[3];
		} else if (PlayerPrefs.GetInt ("controls") == 2) {
			controls[0].image.sprite = cSpr[1];
			controls[1].image.sprite = cSpr[2];
		}
		if (PlayerPrefs.GetInt ("diff") == 1) {
			difficulty[0].image.sprite = dSpr[0];
			difficulty[1].image.sprite = dSpr[3];
			difficulty[2].image.sprite = dSpr[5];
		} else if (PlayerPrefs.GetInt ("diff") == 2) {
			difficulty[0].image.sprite = dSpr[1];
			difficulty[1].image.sprite = dSpr[2];
			difficulty[2].image.sprite = dSpr[5];
		} else if (PlayerPrefs.GetInt ("diff") == 3) {
			difficulty[0].image.sprite = dSpr[1];
			difficulty[1].image.sprite = dSpr[3];
			difficulty[2].image.sprite = dSpr[4];
		}
		PlayerPrefs.SetFloat ("speed", speed.value * 8f);
	}

	public void ChangeCtrl(int i) {
		PlayerPrefs.SetInt ("controls", i);
	}
	
	public void ChangeDff(int i) {
		PlayerPrefs.SetInt ("diff", i);
	}


}
