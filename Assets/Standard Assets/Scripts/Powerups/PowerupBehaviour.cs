using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
public class PowerupBehaviour : MonoBehaviour {

	public int whatPowerup; //0 - no powerup, 1 - invulnerable 2 - freeze 3 - pac 4 - pass 5 - radius
	public Sprite[] sheets = new Sprite[6];
	public int numUse = 0;
	public float timer;
	public string powerupNum;
	public Sprite[] capture = new Sprite[4];
	public Sprite[] freeze = new Sprite[4];
	public Sprite[] invincible = new Sprite[4];
	public Sprite[] passthru = new Sprite[4];
	// Use this for initialization
	void Start () {
		whatPowerup = PlayerPrefs.GetInt ("ActivePowerupSlot" + powerupNum);
		numUse = PlayerPrefs.GetInt ("ActivePowerupSlot" + powerupNum + "Count");
		if (whatPowerup == 4) {
			PlayerPrefs.SetInt ("ActiveMiniPac", 1);
			GetComponent<Button> ().interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0) {
			if (numUse > 0)
				GetComponent<Image> ().sprite = sheets [whatPowerup];
			if (numUse <= 0)
				GetComponent<Image> ().sprite = sheets [0];
		}
		if (timer > 0) {
			timer-=Time.deltaTime;
			if (whatPowerup == 1) {
				if (timer > 11.25f) GetComponent<Image> ().sprite = capture [0];
				else if (timer > 7.5f && timer < 11.25f) GetComponent<Image> ().sprite = capture [1];
				else if (timer > 3.75f && timer < 7.5f ) GetComponent<Image> ().sprite = capture [2];
				else if (timer > 0 && timer < 3.75f) GetComponent<Image> ().sprite = capture [3];
			} else if (whatPowerup == 2) {
				if (timer > 11.25f) GetComponent<Image> ().sprite = freeze [0];
				else if (timer > 7.5f  && timer < 11.25f) GetComponent<Image> ().sprite = freeze [1];
				else if (timer > 3.75f && timer < 7.5f ) GetComponent<Image> ().sprite = freeze [2];
				else if (timer > 0 && timer < 3.75f) GetComponent<Image> ().sprite = freeze [3];
			} else if (whatPowerup == 3) {
				if (timer > 11.25f) GetComponent<Image> ().sprite = invincible [0];
				else if (timer > 7.5f  && timer < 11.25f) GetComponent<Image> ().sprite = invincible [1];
				else if (timer > 3.75f && timer < 7.5f) GetComponent<Image> ().sprite = invincible [2];
				else if (timer > 0 && timer < 3.75f) GetComponent<Image> ().sprite = invincible [3];
			} else if (whatPowerup == 5){
				if (timer > 11.25f) GetComponent<Image> ().sprite = passthru [0];
				else if (timer > 7.5f && timer < 11.25f) GetComponent<Image> ().sprite = passthru [1];
				else if (timer > 3.75f && timer < 7.5f) GetComponent<Image> ().sprite = passthru [2];
				else if (timer > 0 && timer < 3.75f
				         ) GetComponent<Image> ().sprite = passthru [3];
			}
		}
	}
}
