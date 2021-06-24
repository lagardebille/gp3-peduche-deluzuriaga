using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterSelectScript : MonoBehaviour {

	public GameObject[] players = new GameObject[3];

	public List<GameObject> character = new List<GameObject>();
	public GameObject[] pointers = new GameObject[3];
	public Image descStage;
	public Sprite[] desc = new Sprite[3];
	public Button next;
	// Use this for initialization
	void Start () {
		character.Add(players[PlayerPrefs.GetInt ("CharActive")]);
		players[PlayerPrefs.GetInt ("CharActive")].GetComponent<SelectedCharacter> ().isSelected = true;
		pointers [players[PlayerPrefs.GetInt ("CharActive")].GetComponent<SelectedCharacter> ().pos].SetActive (true);
		descStage.sprite = desc[players[PlayerPrefs.GetInt ("CharActive")].GetComponent<SelectedCharacter> ().pos];
	}
	
	// Update is called once per frame
	void Update () {
		if (character.Count > 0)
			next.interactable = true;
		else
			next.interactable = false;
	}

	public void SelectCharacter(GameObject b) {
		if (character.Count <1 && !b.GetComponent<SelectedCharacter> ().isSelected) {
			b.GetComponent<SelectedCharacter> ().isSelected = true;
			character.Add (b);
		} else if (character.Count >= 1 && !b.GetComponent<SelectedCharacter> ().isSelected) {
			character [0].GetComponent<SelectedCharacter> ().isSelected = false;
			character.RemoveAt (0);
			b.GetComponent<SelectedCharacter> ().isSelected = true;
			character.Add (b);
		} else if (b.GetComponent<SelectedCharacter> ().isSelected) {
			if (character.Count > 0) {
				for (int i = 0; i < character.Count; i++) {
					if (character[i] == b) {
						character.RemoveAt(i);
						b.GetComponent<SelectedCharacter> ().isSelected = false;
					}
				}
			}
		} 
		for (int i = 0; i<3; i++) {
			pointers[i].SetActive(false);
		}

		if (character.Count > 0) {
			pointers [character [0].GetComponent<SelectedCharacter> ().pos].SetActive (true);
			descStage.sprite = desc[character [0].GetComponent<SelectedCharacter> ().pos];
			PlayerPrefs.SetInt ("CharActive", b.GetComponent<SelectedCharacter>().pos);
		}

	}

	public void LoadLevel() {
		if (character.Count > 0) Application.LoadLevel ("IAP");
	}
}
