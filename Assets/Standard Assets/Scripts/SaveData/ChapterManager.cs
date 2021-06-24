using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChapterManager : MonoBehaviour {

	public int ChapterNumber;
	public int novice, pro, master;
	public Sprite[] sprites = new Sprite[5];
	int totalScore, totalStars;
	// Use this for initialization
	void Start () {
		for (int i = 1; i <5; i++) {
			totalScore+=PlayerPrefs.GetInt("C"+ChapterNumber.ToString()+"Level"+i.ToString()+"Score");
			totalStars=+PlayerPrefs.GetInt("C"+ChapterNumber.ToString()+"Level"+i.ToString()+"Star");
		}
		totalStars = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("C" + ChapterNumber.ToString () + "Finished") == 1) {
			if (totalScore < pro) 
				GetComponent<Image> ().sprite = sprites [1];
			else if (totalScore >= pro && totalScore < master)
				GetComponent<Image> ().sprite = sprites [2];
			else if (totalScore >= master) 
				GetComponent<Image> ().sprite = sprites [3];
		} else if (PlayerPrefs.GetInt ("C" + ChapterNumber.ToString () + "Finished") != 1) {
			if (ChapterNumber == 1) GetComponent<Image> ().sprite = sprites [0];
			else if (ChapterNumber>1) {
				if (PlayerPrefs.GetInt ("C" + (ChapterNumber-1).ToString () + "Finished") == 1 && totalStars >= 10) {
					GetComponent<Button>().interactable = true;
					GetComponent<Image> ().sprite = sprites [0];
				}
				else if (PlayerPrefs.GetInt ("C" + (ChapterNumber-1).ToString () + "Finished") != 1 || totalStars < 10) {
					GetComponent<Button>().interactable = false;
					GetComponent<Image> ().sprite = sprites [4];
				}
			}
		}
	}
}