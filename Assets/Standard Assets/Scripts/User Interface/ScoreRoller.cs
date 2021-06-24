using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreRoller : MonoBehaviour {

	/*public Sprite[] digits = new Sprite[10];*/
/*	public Image[] scoreBoard = new Image[6];
	public Image[] scoreBoardFinished = new Image[6];*/
	/*public Image[] miniBoard = new Image[2];
	public Image[] nkBoard = new Image[2];*/
	public Slider timerSlider;
	Text scoreUI, miniUI, NKUI, finalScoreUI;
	public GameObject snakeHead;
	int tempScore, tempMini, tempNK;
	float tempTimer, tempStartTimer;
	// Use this for initialization
	void Start () {	
		snakeHead = GameObject.FindGameObjectWithTag ("Player");
		tempScore = snakeHead.GetComponent<DataHolder> ().score;
		scoreUI = GameObject.FindGameObjectWithTag ("scoreUI").GetComponent<Text>();
		miniUI = GameObject.FindGameObjectWithTag ("miniUI").GetComponent<Text>();
		NKUI = GameObject.FindGameObjectWithTag ("NKUI").GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
		snakeHead = GameObject.FindGameObjectWithTag ("Player");
		if (snakeHead != null) {
			int score = snakeHead.GetComponent<DataHolder> ().score;
		
			//Debug.Log(score);
			tempMini = snakeHead.GetComponent<DataHolder> ().miniCount;
			tempNK = snakeHead.GetComponent<DataHolder> ().nkCount;
			tempTimer = snakeHead.GetComponent<DataHolder> ().timer;
			tempStartTimer = snakeHead.GetComponent<DataHolder> ().startTimer;
			if (score != tempScore) {
				if (score > tempScore) {
					tempScore+=5;
					scoreUI.color = Color.green;
				} else if (score < tempScore) {
					tempScore-=5;
					scoreUI.color = Color.red;
				}
			} else scoreUI.color = Color.white;

			int[] scoreDivisions = new int[6];

			scoreUI.text = tempScore.ToString();
			if (tempMini < 10 && tempMini > 1) miniUI.text = "0" + tempMini.ToString();
			else if (tempMini == 1) miniUI.text = "0 " + tempMini.ToString();
			else if (tempMini == 0) miniUI.text = "00";
			else miniUI.text = tempMini.ToString();

			if (tempNK < 10) NKUI.text = "0" + tempNK.ToString();
			else NKUI.text = tempNK.ToString();

			timerSlider.value = tempTimer / tempStartTimer;

			if (GameObject.FindGameObjectWithTag ("finalScoreUI") != null)finalScoreUI = GameObject.FindGameObjectWithTag ("finalScoreUI").GetComponent<Text>();
			if (finalScoreUI != null)finalScoreUI.text = score.ToString();

		}
	}
}
