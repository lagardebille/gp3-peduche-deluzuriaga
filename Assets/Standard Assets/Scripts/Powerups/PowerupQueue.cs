using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PowerupQueue : MonoBehaviour {

	GameObject player,prompt;
	public List<GameObject> powerUpQueue = new List<GameObject>();
	public GameObject[] pointers = new GameObject[5];
	public Sprite[] zaex_ak_sprites = new Sprite[8];
	void Start() {
		/*PlayerPrefs.SetInt("pow_1", 5);//invu
		PlayerPrefs.SetInt("pow_2", 5);//freeze
		PlayerPrefs.SetInt("pow_3", 5);//pac
		PlayerPrefs.SetInt("pow_4", 5);//pass
		PlayerPrefs.SetInt("pow_5", 5);//radius*/
		PlayerPrefs.DeleteKey ("ActivePowerupSlot1");
		PlayerPrefs.DeleteKey ("ActivePowerupSlot2");
		player = GameObject.FindGameObjectWithTag ("Player");
		prompt = GameObject.FindGameObjectWithTag ("Prompt");
	//	prompt.SetActive (false);
	}

	void Update() {
		if (powerUpQueue.Count > 1) {
			if (powerUpQueue [1] != null){
				PlayerPrefs.SetInt ("ActivePowerupSlot2", powerUpQueue [1].GetComponent<SelectedPowerUp> ().type);
				if (powerUpQueue [1].transform.GetChild(0).GetComponent<PowerupCount> ().count >=2) PlayerPrefs.SetInt ("ActivePowerupSlot2Count", 2);
				else PlayerPrefs.SetInt ("ActivePowerupSlot2Count", 1);
	
			} else if (powerUpQueue [1] == null) {
				PlayerPrefs.SetInt ("ActivePowerupSlot2", 0);
				PlayerPrefs.SetInt ("ActivePowerupSlot2Count", 0);
			}
			if (powerUpQueue [0] != null){
				PlayerPrefs.SetInt ("ActivePowerupSlot1", powerUpQueue [0].GetComponent<SelectedPowerUp> ().type);
				if (powerUpQueue [0].transform.GetChild(0).GetComponent<PowerupCount> ().count >=2) PlayerPrefs.SetInt ("ActivePowerupSlot1Count", 2);
				else PlayerPrefs.SetInt ("ActivePowerupSlot1Count", 1);
			} else if (powerUpQueue [0] == null){
				PlayerPrefs.SetInt ("ActivePowerupSlot1", 0);
				PlayerPrefs.SetInt ("ActivePowerupSlot1Count", 0);
			}
		} 
		else if (powerUpQueue.Count > 0) {
			if (powerUpQueue [0] != null){
				PlayerPrefs.SetInt ("ActivePowerupSlot1", powerUpQueue [0].GetComponent<SelectedPowerUp> ().type);
				if (powerUpQueue [0].transform.GetChild(0).GetComponent<PowerupCount> ().count >=2) PlayerPrefs.SetInt ("ActivePowerupSlot1Count", 2);
				else PlayerPrefs.SetInt ("ActivePowerupSlot1Count", 1);
			} else if (powerUpQueue [0] == null){
				PlayerPrefs.SetInt ("ActivePowerupSlot1", 0);
				PlayerPrefs.SetInt ("ActivePowerupSlot1Count", 0);
			}
			PlayerPrefs.SetInt ("ActivePowerupSlot2", 0);
			PlayerPrefs.SetInt ("ActivePowerupSlot2Count", 0);
		} 

		if (powerUpQueue.Count <= 0) {
			PlayerPrefs.SetInt ("ActivePowerupSlot1", 0);
			PlayerPrefs.SetInt ("ActivePowerupSlot1Count", 0);
			PlayerPrefs.SetInt ("ActivePowerupSlot2", 0);
			PlayerPrefs.SetInt ("ActivePowerupSlot2Count", 0);
		}
	}

	public void AddPowerUp(GameObject b) {
		if (powerUpQueue.Count <2 && b.GetComponent<SelectedPowerUp> ().isSelected != 1 && b.transform.GetChild(0).GetComponent<PowerupCount>().count > 0) {
			b.GetComponent<SelectedPowerUp> ().isSelected = 1;
			powerUpQueue.Add (b);
		} else if (powerUpQueue.Count >= 2 && b.GetComponent<SelectedPowerUp> ().isSelected != 1) {
			powerUpQueue [0].GetComponent<SelectedPowerUp> ().isSelected = 0;
			powerUpQueue.RemoveAt (0);
			b.GetComponent<SelectedPowerUp> ().isSelected = 1;
			powerUpQueue.Add (b);
		} else if (b.GetComponent<SelectedPowerUp> ().isSelected == 1) {
			if (powerUpQueue.Count > 0) {
				for (int i = 0; i < powerUpQueue.Count; i++) {
					if (powerUpQueue[i] == b) {
						powerUpQueue.RemoveAt(i);
						b.GetComponent<SelectedPowerUp> ().isSelected = 0;
					}
				}
			}
		} 
		for (int i = 0; i<5; i++) {
			pointers[i].SetActive(false);
		}
		if (powerUpQueue.Count>0)pointers [powerUpQueue [0].GetComponent<SelectedPowerUp> ().type-1].SetActive (true);
		if (powerUpQueue.Count>1)pointers [powerUpQueue [1].GetComponent<SelectedPowerUp> ().type-1].SetActive (true);
	}
	

	/*public void Activate(GameObject g) {
		int i = g.GetComponent<PowerupBehaviour> ().whatPowerup;
		if (g.GetComponent<PowerupBehaviour> ().numUse > 0) {
			g.GetComponent<PowerupBehaviour> ().numUse--;
			switch (i) {
			case 1:
				if (PlayerPrefs.GetInt ("pow_1") > 0)
					PlayerPrefs.SetInt ("pow_1", PlayerPrefs.GetInt ("pow_1") - 1);//invu
				player.GetComponent<DataHolder> ().radiusTimer += 15;
				break;
			case 2:
				if (PlayerPrefs.GetInt ("pow_2") > 0)
					PlayerPrefs.SetInt ("pow_2", PlayerPrefs.GetInt ("pow_2") - 1);
				player.GetComponent<DataHolder> ().freezeTimer += 15;
				break;
			case 3:
				if (PlayerPrefs.GetInt ("pow_3") > 0)
					PlayerPrefs.SetInt ("pow_3", PlayerPrefs.GetInt ("pow_3") - 1);
				player.GetComponent<DataHolder> ().invuTimer += 15;
				break;
			case 4:
				if (PlayerPrefs.GetInt ("pow_4") > 0)
					PlayerPrefs.SetInt ("pow_4", PlayerPrefs.GetInt ("pow_4") - 1);
				player.GetComponent<DataHolder> ().lifes += 8;
				break;
			case 5:
				if (PlayerPrefs.GetInt ("pow_5") > 0)
					PlayerPrefs.SetInt ("pow_5", PlayerPrefs.GetInt ("pow_5") - 1);
				player.GetComponent<DataHolder> ().passTimer += 10;
				break;
			default:
				g.GetComponent<PowerupBehaviour> ().whatPowerup = 0;
				break;
			}
		}
		else if (g.GetComponent<PowerupBehaviour> ().numUse == 0) g.GetComponent<PowerupBehaviour> ().whatPowerup = 0;
	}*/
}
