using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddPowerupChest : MonoBehaviour {

	public GameObject PowerupPopup;
	public GameObject ChestPrefab;
	public Sprite[] popupSprites = new Sprite[4];
	bool hasDisplayed = false;
	GameObject snake;
	int type;
	// Use this for initialization
	void Start () {
		type = (int)Random.Range (0, 4);
		PowerupPopup.transform.GetChild (0).GetChild (0).GetComponent<Image> ().sprite = popupSprites [type];
		//PlayerPrefs.SetInt ("NKCount", 16);
	}
	
	// Update is called once per frame
	void Update () {
		snake = GameObject.FindGameObjectWithTag("Player");
		if (PlayerPrefs.GetInt ("NKCount") > 0 && PlayerPrefs.GetInt ("NKCount") % 20 == 0 && snake.GetComponent<Snake>().isPlaying && PlayerPrefs.GetInt("diff") > 1) {
			if (!hasDisplayed){
				snake.GetComponent<Snake>().isPlaying = false;
				snake.GetComponent<Snake>().currentSpeed = 0;
				PowerupPopup.SetActive(true);
				StartCoroutine(DelayPause());
				hasDisplayed = true;
			}
		} else if (PlayerPrefs.GetInt ("NKCount") % 20 != 0) hasDisplayed = false;
	}

	public void InstantiateChest() {
		int x = (int)Random.Range(3,35);
		
		// y position between top & bottom border
		int y = (int)Random.Range(3,15);
		Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3((x+0.5f )/40f,(y+0.5f)/22.5f,3f));
		// Instantiate the food at (x, y)
		GameObject clone = Instantiate (ChestPrefab, pos, Quaternion.identity) as GameObject;
		clone.GetComponent<ChestBehaviour> ().type = type;

		PowerupPopup.SetActive (false);
		Time.timeScale = 1;
	}
	public void RemovePopup() {
		PowerupPopup.SetActive (false);
		Time.timeScale = 1;
	}

	IEnumerator DelayPause() {
		yield return new WaitForSeconds(0.85f);
		Time.timeScale = 0;
	}
}
