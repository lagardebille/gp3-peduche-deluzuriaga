using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectedCharacter : MonoBehaviour {

	public bool isSelected = false;
	public int pos = 0;
	bool isActive = false;
	public Sprite[] sprites = new Sprite[2];
	public Sprite locked;
	public Sprite[] zaex_ak_sprites = new Sprite[8];

	void Update() {
//		Debug.Log (PlayerPrefs.GetInt ("NKCount"));
		if (PlayerPrefs.GetInt ("perfect_plays") >= 7)
			PlayerPrefs.SetInt ("Zaex_Ak", 1);
		if (pos == 0) {
			if (isSelected)
				GetComponent<Image> ().sprite = sprites [1];
			else
				GetComponent<Image> ().sprite = sprites [0];
		} else if (pos == 1) {
			if (PlayerPrefs.GetInt("Phantom") == 1) {
				GetComponent<Button>().interactable = true;
				if (isSelected)
					GetComponent<Image> ().sprite = sprites [1];
				else
					GetComponent<Image> ().sprite = sprites [0];
			} else {
				GetComponent<Image> ().sprite = locked;
				GetComponent<Button>().interactable = false;
			}
		} else if (pos == 2) {
			if (PlayerPrefs.GetInt("Zaex_Ak") == 1 && PlayerPrefs.GetInt("NKCount") >= 60) {
				Social.ReportProgress(GameConstants.achievement_i_am_zrexak_the_immortal, 100, (bool success) => {});
				GetComponent<Button>().interactable = true;
				if (isSelected)
					GetComponent<Image> ().sprite = sprites [1];
				else
					GetComponent<Image> ().sprite = sprites [0];
			} else {
				GetComponent<Button>().interactable = false;
				int i = PlayerPrefs.GetInt("perfect_plays");
				if (i > 7) i = 7;
				GetComponent<Image> ().sprite = zaex_ak_sprites[i];
			}
		}

	}
}
