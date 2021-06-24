using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class DataHolder : MonoBehaviour {

	public int score;
	public int cNum;
	public int lNum;
	public int miniCount;
	public int nkCount = 0;
	public int nkTemp = 0;
	public float timer;
	public float startTimer;
	public float radiusTimer = 0f;
	public float passTimer = 0f;
	public float invuTimer = 0f;
	public float freezeTimer = 0f;
	public bool lifeActive = false;
	public int lifes = 0;
	public int HighScore;
	public bool isSwiping = false;//for the meanwhile 

	public int[] targetMini = new int[3];
	public int[] targetNK = new int[3];
	public GameObject finish;
	public Image[] stars = new Image[3];
	public int starCount = 0;
	public GameObject gameOver;
	public GameObject[] blocks;
	GameObject runtimeUI;
	public GameObject timesUp;
	public int lbLevel;

	public bool hasIncremented = false;
	public bool hasBeenHit = false;

	//gold 255 238 2  or 1 0.93 0.01
	//silver 237 237 237 or 0.93 0.93 0.93
	//bronze 237 196 90 or  0.93 0.77 0.35
	Color g = new Color(1.0f, 0.93f, 0.01f), s = new Color(0.61f, 0.61f, 0.61f), b = new Color(0.93f, 0.77f, 0.35f);

	Text nk, mini;

	int maxPowerupTimer = 0;

	bool hasAnimationPlayed = false;
	bool hasAudioPlayed = false;
	bool hasPromptPlayed = false;
	

	public bool isCollided = false;
	// Use this for initialization
	void Start() {
		startTimer = timer;
		HighScore = PlayerPrefs.GetInt ("C" + cNum.ToString () + "Level" + lNum.ToString () + "Score");
		runtimeUI = GameObject.FindGameObjectWithTag("RuntimeUI");
		mini = GameObject.FindGameObjectWithTag ("miniUI").GetComponent<Text>();
		nk = GameObject.FindGameObjectWithTag ("NKUI").GetComponent<Text>();
	}
	void Update() {
		SetCounterColor ();
		runtimeUI = GameObject.FindGameObjectWithTag("RuntimeUI");
		if (PlayerPrefs.GetInt ("ActiveMiniPac") == 1) {
			lifeActive = true;
			PlayerPrefs.SetInt("ActiveMiniPac", 0);
			PlayerPrefs.SetInt("pow_4", PlayerPrefs.GetInt("pow_4")-1);
		}
		if (GetComponent<Snake> ().isPlaying) {
			if (PlayerPrefs.GetInt ("CharActive") == 1) {
				maxPowerupTimer = 17;
			} else maxPowerupTimer = 15;

			if (lifes <= 0) lifes = 0;
			if (miniCount <= 0) miniCount = 0;

			timer -= Time.deltaTime;
			//collision radius
			if (radiusTimer > 0)
				radiusTimer -= Time.deltaTime;
			if (radiusTimer > maxPowerupTimer)
				radiusTimer = maxPowerupTimer;
			else if (radiusTimer <= 0)
				radiusTimer = 0;
			//pass thru timer
			if (passTimer > 0) {
				if (!hasAnimationPlayed) {
					GetComponent<Animator> ().SetTrigger ("Activate");
					StartCoroutine(DelayActivate("Pass"));
					hasAnimationPlayed = true;
				}
				passTimer -= Time.deltaTime;
				blocks = GameObject.FindGameObjectsWithTag ("Blocks");
				if (blocks.Length > 0) {
					for (int i = 0; i<blocks.Length; i++) {
						if (blocks[i] != null){
							//Debug.Log("PASSTRHU");
							BoxCollider2D[] myColliders = blocks[i].GetComponents<BoxCollider2D>();
							foreach(BoxCollider2D bc in myColliders) bc.enabled = false;
							//blocks [i].GetComponent<BoxCollider2D> ().enabled = false;
						}
					}
				}
				if (passTimer > maxPowerupTimer)
					passTimer = maxPowerupTimer;
			} else if (passTimer <= 0) {
				passTimer = 0;
				blocks = GameObject.FindGameObjectsWithTag ("Blocks");
				if (blocks.Length > 0) {
					for (int i = 0; i<blocks.Length; i++) {
						if (blocks[i] != null){
							BoxCollider2D[] myColliders = blocks[i].GetComponents<BoxCollider2D>();
							foreach(BoxCollider2D bc in myColliders) bc.enabled = true;
							//blocks [i].GetComponent<BoxCollider2D> ().enabled = false;
						}
					}
				}
			}
			//freeze
			if (freezeTimer > 0) {
				if (!hasAnimationPlayed) {
					GetComponent<Animator> ().SetTrigger ("Activate");
					StartCoroutine(DelayActivate("Freeze"));
					hasAnimationPlayed = true;
				}
				freezeTimer -= Time.deltaTime;
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				if (enemies != null) {
					for (int i = 0; i<enemies.Length; i++) {
						if (enemies[i] != null)enemies [i].GetComponent<EnemySpeed> ().isTraitEnabled = false;
					}
				}
				if (freezeTimer > maxPowerupTimer)
					freezeTimer = maxPowerupTimer;
			} else if (freezeTimer <= 0) {
				freezeTimer = 0;
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				//Debug.Log(enemies.Length);
				if (enemies.Length > 0) {
					foreach (GameObject g in enemies) {
						if (g.GetComponent<EnemySpeed> ()) g.GetComponent<EnemySpeed> ().isTraitEnabled = true;
					}
				/*	for (int i = 0; i<enemies.Length; i++) {
						if (enemies[i] != null)enemies [i].GetComponent<EnemySpeed> ().isTraitEnabled = true;
					}*/
				}
			}

			//invincible
			if (invuTimer > 0) {
				if (!hasAnimationPlayed) {
					GetComponent<Animator> ().SetTrigger ("Activate");
					StartCoroutine(DelayActivate("Invu"));
					hasAnimationPlayed = true;
				}
				invuTimer -= Time.deltaTime;
				for (int j = 0; j<GetComponent<Snake>().segments.Count; j++) {
					GetComponent<Snake> ().segments [j].GetComponent<Animator> ().SetTrigger ("Invul");
				}
				if (invuTimer > maxPowerupTimer)
					invuTimer = maxPowerupTimer;
			} else if (invuTimer <= 0) {
				invuTimer = 0;
				for (int j = 0; j<GetComponent<Snake>().segments.Count; j++) {
					GetComponent<Snake> ().segments [j].GetComponent<Animator> ().SetTrigger ("Idle");
				}
				GetComponent<AudioSource> ().Stop();
			} if (GetComponent<Snake>().frozenTimer > 0) {
				GetComponent<Animator> ().SetTrigger ("Stone");
			} else if (GetComponent<Snake>().frozenTimer <= 0) {
				if (freezeTimer > 0)
					GetComponent<Animator> ().SetTrigger ("Freeze");
				else if (invuTimer > 0)
					GetComponent<Animator> ().SetTrigger ("Invu");
				else if (passTimer > 0) {
					GetComponent<Animator> ().SetTrigger ("Pass");
				} else GetComponent<Animator> ().SetTrigger ("Idle"); 
			}
			if (invuTimer <= 0 && passTimer <= 0 && freezeTimer <= 0 && GetComponent<Snake>().frozenTimer <= 0 && hasAnimationPlayed) {
				GetComponent<Animator> ().SetTrigger ("Idle");
				hasAnimationPlayed = false;
			}

			//game finish
			//if (miniCount >= 2 && nkCount >= 2) GetComponent<Snake> ().currentSpeed = 0;
			if (miniCount >= targetMini [0]) {
				FastestTime.times_hard[lbLevel-1] = (int)timer;
				FastestTime.TotalIt();
				switch (PlayerPrefs.GetInt("diff")) {
					case 1:
						Social.ReportScore(lbLevel, GameConstants.leaderboard_highest_level_achieved_easy, (bool success) => {});
						break;
					case 2:
						Social.ReportScore(lbLevel, GameConstants.leaderboard_highest_level_achieved_normal, (bool success) => {});
						if (!hasBeenHit){
							PlayerPrefs.SetInt("LevelNoTouchCount", PlayerPrefs.GetInt("LevelTouchNoCount")+1);
							Social.ReportScore(PlayerPrefs.GetInt("LevelTouchNoCount"), GameConstants.leaderboard_highest_level_achieved__no_damage_normal, (bool success) => {});
						}
						break;
					case 3:
						Social.ReportScore(lbLevel, GameConstants.leaderboard_highest_level_achieved_hard, (bool success) => {});
						if (!hasBeenHit){
							PlayerPrefs.SetInt("LevelNoTouchCount", PlayerPrefs.GetInt("LevelTouchNoCount")+1);
							Social.ReportScore(PlayerPrefs.GetInt("LevelTouchNoCount"), GameConstants.leaderboard_highest_level_achieved_hard, (bool success) => {});
						}
						break;
				}
				if (PlayerPrefs.GetInt ("ActivePowerupSlot1") == 0 && PlayerPrefs.GetInt ("ActivePowerupSlot1") == 0) {
					Social.ReportProgress (GameConstants.achievement_nohelp_droid, 100.0f, (bool success) => {});
				}
				if (nkCount >= targetNK [0]) {
					GetComponent<Snake> ().currentSpeed = 0;
					starCount = 3;
					GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
					for (int a = 0; a<enemies.Length; a++) {
						Destroy (enemies [a]);
					}
					finish.SetActive (true);
					if (!hasAudioPlayed){
						Camera.main.GetComponent<LevelSounds> ().PlayGameFinished ();
						hasAudioPlayed = true;
					}
					for (int i = 0; i<starCount; i++) {
						StartCoroutine(PlayStar(i,i));
					}
				} else if (nkCount < targetNK [0]) {
					GetComponent<Snake> ().currentSpeed = 0;
					starCount = 2;
					GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
					for (int a = 0; a<enemies.Length; a++) {
						Destroy (enemies [a]);
					}
					finish.SetActive (true);
					if (!hasAudioPlayed){
						Camera.main.GetComponent<LevelSounds> ().PlayGameFinished ();
						hasAudioPlayed = true;
					}
					for (int i = 0; i<starCount; i++) {
						StartCoroutine(PlayStar(i,i));
					}
				}
				PlayerPrefs.SetInt ("Chapter"+cNum.ToString()+"Level"+lNum.ToString()+"Star", starCount);
				PlayerPrefs.SetInt ("C"+cNum.ToString()+"Level"+lNum.ToString()+"Finished",1);
				if (score > HighScore){
					PlayerPrefs.SetInt ("C"+cNum.ToString()+"Level"+lNum.ToString()+"Score", score);
					finish.transform.GetChild(0).gameObject.SetActive(true);
				}
				if (lNum == 4){
					PlayerPrefs.SetInt ("C"+cNum.ToString()+"Finished", 1);
					Social.ReportScore(FastestTime.GetTimes(cNum, PlayerPrefs.GetInt("diff")), 
					                   GameConstants.GetCode(cNum, PlayerPrefs.GetInt("diff")), 
					                   (bool success) => {});
					if (cNum == 6) {
						if (PlayerPrefs.GetInt("diff") == 2) Social.ReportProgress (GameConstants.achievement_the_game_is_on, 100.0f, (bool success) => {});
						else if (PlayerPrefs.GetInt("diff") == 3) Social.ReportProgress (GameConstants.achievement_the_conqueror, 100.0f, (bool success) => {});
						if (PlayerPrefs.GetInt("NKCount") >= 115) Social.ReportProgress(GameConstants.achievement_nk_coin_collector, 100.0f, (bool success) => {});
						if (PlayerPrefs.GetInt("MiniCount") >= 60) Social.ReportProgress(GameConstants.achievement_mini_maniac, 100.0f, (bool success) => {});
					}
				}
				GetComponent<Snake> ().isPlaying = false;
				GetComponent<Snake>().isMoving = false;
				if (!hasIncremented) {
					AddResources(nkCount, miniCount);
					hasIncremented = true;
				}
			} else if (timer <= 0) {
				if ((miniCount < targetMini [0]) &&( miniCount >= targetMini [1] && nkCount >= targetNK [1])) {
					GetComponent<Snake> ().currentSpeed = 0;
					starCount = 2;
					GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
					for (int a = 0; a<enemies.Length; a++) {
						Destroy (enemies [a]);
					}
					finish.SetActive (true);
					for (int i = 0; i<starCount; i++) {
						StartCoroutine(PlayStar(i,i));
					}
					PlayerPrefs.SetInt ("Chapter"+cNum.ToString()+"Level"+lNum.ToString()+"Star", starCount);
					if (score > HighScore){
						PlayerPrefs.SetInt ("C"+cNum.ToString()+"Level"+lNum.ToString()+"Score", score);
						finish.transform.GetChild(0).gameObject.SetActive(true);
					}
					if (lNum == 4)PlayerPrefs.SetInt ("C"+cNum.ToString()+"Finished", 1);
					PlayerPrefs.SetInt ("C"+cNum.ToString()+"Level"+lNum.ToString()+"Finished",1);
					GetComponent<Snake> ().isPlaying = false;
					GetComponent<Snake>().isMoving = false;
				} else if ((miniCount < targetMini [1]) && (miniCount >= targetMini[2] && nkCount >= targetNK[2])) {
					GetComponent<Snake> ().currentSpeed = 0;
					starCount = 1;
					GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
					for (int a = 0; a<enemies.Length; a++) {
						Destroy (enemies [a]);
					}
					finish.SetActive (true);
					GetComponent<Snake> ().isPlaying = false;
					if (!hasAudioPlayed){
						Camera.main.GetComponent<LevelSounds> ().PlayGameFinished ();
						hasAudioPlayed = true;
					}
					for (int i = 0; i<starCount; i++) {
						StartCoroutine(PlayStar(i,i));
					}
					PlayerPrefs.SetInt ("Chapter"+cNum.ToString()+"Level"+lNum.ToString()+"Star", starCount);
					if (score > HighScore){
						PlayerPrefs.SetInt ("C"+cNum.ToString()+"Level"+lNum.ToString()+"Score", score);
						finish.transform.GetChild(0).gameObject.SetActive(true);
					}
					if (lNum == 4)PlayerPrefs.SetInt ("C"+cNum.ToString()+"Finished", 1);
					PlayerPrefs.SetInt ("C"+cNum.ToString()+"Level"+lNum.ToString()+"Finished",1);
					GetComponent<Snake> ().isPlaying = false;
					GetComponent<Snake>().isMoving = false;
				}else {
					GetComponent<Animator> ().SetTrigger ("Die");
					GetComponent<Snake> ().currentSpeed = 0;
					GetComponent<Snake> ().isPlaying = false;
					GetComponent<Snake>().isMoving = false;
					timesUp.SetActive(true);
					StartCoroutine (ShowScreen ());
				}
				if (!hasIncremented) {
					AddResources(nkCount, miniCount);
					hasIncremented = true;
				}
			}
		}
	}

	void SetCounterColor() {
		if (miniCount >= targetMini [0] && nkCount >= targetNK [0]) {
			nk.color = g;
			mini.color = g;
		} else if ((miniCount < targetMini [0]) && (miniCount >= targetMini [1] && nkCount >= targetNK [1])) {
			nk.color = s;
			mini.color = s;
		} else if  ((miniCount < targetMini [1]) && (miniCount >= targetMini [2])) {
			nk.color = b;
			mini.color = b;
		} else if (miniCount < targetMini [2]) {
			nk.color = Color.white;
			mini.color = Color.white;
		}
	}

	void AddResources(int nk, int mini) {
		PlayerPrefs.SetInt ("NKCount", PlayerPrefs.GetInt ("NKCount") + nkCount);
		PlayerPrefs.SetInt ("MiniCount", PlayerPrefs.GetInt ("MiniCount") + nkCount);
	}

	public IEnumerator ShowScreen() {
		yield return new WaitForSeconds (2f);
		if (!hasAudioPlayed){
			Camera.main.GetComponent<LevelSounds> ().PlayGameOver ();
			hasAudioPlayed = true;
		}
		gameOver.SetActive (true);
		timesUp.SetActive (false);
		if (PlayerPrefs.GetInt ("goScreen") >= 9) {
			PlayerPrefs.SetInt ("goScreen", 1);
		} else PlayerPrefs.SetInt ("goScreen", PlayerPrefs.GetInt ("goScreen") + 1);
		StartCoroutine (DelayMenu ());
		//Destroy (transform.root.gameObject);
	}


	IEnumerator DelayActivate(string s) {
		yield return new WaitForSeconds (0.2f);
		GetComponent<Animator> ().SetTrigger (s);
	}

	IEnumerator PlayStar(int i, float f) {
		yield return new WaitForSeconds(f);
		stars [i].GetComponent<Animator> ().SetTrigger ("Star");
		yield return new WaitForSeconds(1f);
		Camera.main.GetComponent<LevelSounds> ().PlayStar ();
		StartCoroutine (DelayMenu ());
	}

	IEnumerator DelayMenu() {
		yield return new WaitForSeconds(20f);
		runtimeUI.GetComponent<GameplayUI> ().RestartLevel ("MainMenu");
	}
}