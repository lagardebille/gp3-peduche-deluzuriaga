using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public int levelNum;
	public string sceneName;
	public int cNum;
	public GameObject scoreText;
	public Sprite[] sprites = new Sprite[3];
	public Sprite[] number = new Sprite[10];
	int score;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("C"+cNum.ToString()+"Level0Finished", 1);
		score = PlayerPrefs.GetInt ("C"+cNum.ToString()+"Level" + levelNum.ToString () + "Score");
		scoreText = transform.GetChild (0).gameObject;
		if (score > 0) {
			scoreText.GetComponent<Text>().text = score.ToString();
			/*transform.GetChild (0).GetComponent<Image> ().sprite = number [(score / 10000) % 10];
			transform.GetChild (1).GetComponent<Image> ().sprite = number [(score / 1000) % 10];
			transform.GetChild (2).GetComponent<Image> ().sprite = number [(score / 100) % 10];
			transform.GetChild (3).GetComponent<Image> ().sprite = number [(score / 10) % 10];
			transform.GetChild (4).GetComponent<Image> ().sprite = number [score % 10];*/
		} else if (score <= 0) {
			scoreText.SetActive(false);
			/*for (int i = 0; i<5; i++) {
				transform.GetChild (i).gameObject.SetActive(false);
			}*/
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("C"+cNum.ToString()+"Level" + (levelNum-1).ToString() + "Finished") == 1) {
			GetComponent<Button> ().interactable = true;
			if (PlayerPrefs.GetInt ("C"+cNum.ToString()+"Level" + levelNum.ToString() + "Score") > 0) {
				GetComponent<Image>().sprite = sprites[2];
			} else if (PlayerPrefs.GetInt ("C"+cNum.ToString()+"Level" + levelNum.ToString() + "Score") == 0) {
				GetComponent<Image>().sprite = sprites[1];
			}
		} else if (PlayerPrefs.GetInt ("C"+cNum.ToString()+"Level" + (levelNum-1).ToString() + "Finished") != 1) {
			GetComponent<Button>().interactable = false;
			GetComponent<Image>().sprite = sprites[0];
		}
	}
}
