using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour {

	public GameObject menu, chapters, settings, characterSelect, controlSelect, credits, iap;
	public GameObject[] stages = new GameObject[6];
	public Button[] controls = new Button[4];
	public Button[] difficulty = new Button[3];
	public Image[] IAPMenus = new Image[4];
	public Sprite[] IAPButtons = new Sprite[4];
	public Sprite[] cSpr = new Sprite[4];
	public Sprite[] dSpr = new Sprite[6];
	public Slider speed;
	public GameObject gameIntro;
	public GameObject liekUs;
	public Text nkCount, coinCount;
	GameObject particles;
	public bool shallIResetItForYouMaster = false;
	bool hasIntroPlayed = false;
	AudioSource aSource;
	public AudioClip mainMenu, story, chapter;
	int activeChapter = 0;

	// Use this for initialization

	void Start() {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		PlayerPrefs.SetInt ("LiekUs", PlayerPrefs.GetInt ("LiekUs") + 1);
		particles = GameObject.FindGameObjectWithTag("Particles");
		GameObject[] sm = GameObject.FindGameObjectsWithTag("SoundManager");
		aSource = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<AudioSource> ();
		if (sm.Length > 1) {
			for (int i = 1; i<sm.Length; i++) {
				DestroyImmediate(sm[i]);
			}
		}
		if (shallIResetItForYouMaster) PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("count", PlayerPrefs.GetInt ("count") + 1);
		if (PlayerPrefs.GetInt ("count") == 1) {
			PlayerPrefs.SetInt("controls", 2);//1 - tap 2 - swipe
			PlayerPrefs.SetFloat ("speed", 6f);
			PlayerPrefs.SetInt("diff", 2);//1 - ez 2 - nm 3 - hd
			PlayerPrefs.SetInt("pow_1", 1);//radius
			PlayerPrefs.SetInt("pow_2", 1);//freeze
			PlayerPrefs.SetInt("pow_3", 1);//invu
			PlayerPrefs.SetInt("pow_4", 1);//pac
			PlayerPrefs.SetInt("pow_5", 1);//pass
			PlayerPrefs.SetInt("Phantom", 0);
			PlayerPrefs.SetInt("Zaex_Ak", 0);
			PlayerPrefs.SetInt("perfect_plays", 0);
			PlayerPrefs.SetInt ("ActiveChapter", 1);
			PlayerPrefs.SetInt ("C1Level0Finished", 1);
			PlayerPrefs.SetInt ("C1Level1Finished", 0);
			PlayerPrefs.SetInt("NKCount", 0);
			PlayerPrefs.SetInt("CoinCount", 0);
			PlayerPrefs.SetInt("Sound", 1);
			PlayerPrefs.SetInt("MiniCount",0);
			PlayerPrefs.SetInt("LevelNoTouchCount", 0);
		}
		if (PlayerPrefs.GetString ("ActivePane").Equals ("chapter1")) {
			stages [0].SetActive (true);
			menu.SetActive (false);
			chapters.SetActive (false);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("chapter2")) {
			stages [1].SetActive (true);
			menu.SetActive (false);
			chapters.SetActive (false);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("chapter3")) {
			stages [2].SetActive (true);
			menu.SetActive (false);
			chapters.SetActive (false);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("chapter4")) {
			stages [3].SetActive (true);
			menu.SetActive (false);
			chapters.SetActive (false);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("chapter5")) {
			stages [4].SetActive (true);
			menu.SetActive (false);
			chapters.SetActive (false);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("chapter6")) {
			stages [5].SetActive (true);
			menu.SetActive (false);
			chapters.SetActive (false);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("iap")) {
			menu.SetActive (false);
			chapters.SetActive (false);
			iap.SetActive(true);
		}

		if (menu.activeSelf) {
			aSource.clip = mainMenu;
			aSource.loop = true;
			aSource.Play ();
		} else {
			aSource.clip = chapter;
			aSource.loop = true;
			aSource.Play ();
		}
		/*else if (PlayerPrefs.GetString ("ActivePane").Equals ("intro2")) {
			menu.SetActive (false);
			ToStages(1);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("intro3")) {
			menu.SetActive (false);
			ToStages(2);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("intro4")) {
			menu.SetActive (false);
			ToStages(3);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("intro5")) {
			menu.SetActive (false);
			ToStages(4);
		} else if (PlayerPrefs.GetString ("ActivePane").Equals ("intro6")) {
			menu.SetActive (false);
			ToStages(5);
		}*/
		Time.timeScale = 1;
		speed.value = (PlayerPrefs.GetFloat ("speed")) / 8f;

		if (PlayerPrefs.GetInt ("LiekUs") >= 3) {
			liekUs.transform.parent.gameObject.SetActive (true);
			//Debug.Log ("LIEK MY ARSE");
			PlayerPrefs.SetInt ("LiekUs", 0);
		}
	}
	public void ToChapters() {
		if (PlayerPrefs.GetInt ("count") <= 1 && !hasIntroPlayed) {
			particles.GetComponent<ParticleSystem>().Stop();
			gameIntro.SetActive(true);
			menu.SetActive(false);
			StartCoroutine(DelayChapters());
			aSource.clip = story;
			aSource.loop = true;
			aSource.Play();
		} else {
			PlayerPrefs.SetString("ActivePane", "chapters");
			chapters.SetActive(true);
			menu.SetActive(false);
			for (int i  = 0; i < stages.Length; i++) {
				stages[i].SetActive(false);
			}
			aSource.clip = chapter;
			aSource.loop = true;
			aSource.Play();
		}
	}

	public void SwitchTabs(int i) {
		switch (i) {
			case 1:
				Debug.Log("Upgrades");
				IAPMenus[2].gameObject.SetActive(true);
				IAPMenus[3].gameObject.SetActive(false);
				IAPMenus[0].sprite = IAPButtons[2];
				IAPMenus[1].sprite = IAPButtons[1];
				break;
			case 2:
				Debug.Log("Coins");
				IAPMenus[2].gameObject.SetActive(false);
				IAPMenus[3].gameObject.SetActive(true);
				IAPMenus[0].sprite = IAPButtons[3];
				IAPMenus[1].sprite = IAPButtons[0];
				break;
		}
	}

	public void ToSettings() {
		PlayerPrefs.SetString("ActivePane", "settings");
		settings.SetActive(true);
		menu.SetActive(false);
	}

	public void ToCredits() {
		PlayerPrefs.SetString("ActivePane", "credits");
		credits.SetActive(true);
		menu.SetActive(false);
	}

	public void ToIAP() {
		PlayerPrefs.SetString("ActivePane", "iap");
		iap.SetActive(true);
		menu.SetActive(false);
	}

	public void ToMain() {
		PlayerPrefs.SetString("ActivePane", "menu");
		menu.SetActive(true);
		chapters.SetActive(false);
		settings.SetActive (false);
		credits.SetActive (false);
		aSource.clip = mainMenu;
		aSource.loop = true;
		aSource.Play();
	}

	public void ToMain2() {
		PlayerPrefs.SetString("ActivePane", "menu");
		menu.SetActive(true);
		chapters.SetActive(false);
		credits.SetActive (false);
		settings.SetActive (false);
		iap.SetActive (false);
	}

	public void ToStages(int i) {
		activeChapter = i;
		int a = i + 1;
		PlayerPrefs.SetString("ActivePane", "chapter"+i.ToString());
		stages [i].SetActive (true);
		chapters.SetActive (false);
	}

	public void ToStages() { 
		int a = activeChapter + 1;
		PlayerPrefs.SetString("ActivePane", "chapter"+activeChapter.ToString());
		stages [activeChapter].SetActive (true);
		characterSelect.SetActive (false);
	}

	public void ToCharacterSelection(GameObject g) {
		stages [activeChapter].SetActive (false);
		PlayerPrefs.SetString ("NextLevel", g.GetComponent<LevelManager>().sceneName);
		PlayerPrefs.SetInt ("ActiveChapter", g.GetComponent<LevelManager>().cNum);
		characterSelect.SetActive (true);
	}

	public void ToCharacterSelection() {
		stages [activeChapter].SetActive (false);
		controlSelect.SetActive (false);
		characterSelect.SetActive (true);
	}

	public void ToControlSelection() {
		characterSelect.SetActive (false);
		controlSelect.SetActive (true);
	}

	public void LoadLevel(GameObject g){
		PlayerPrefs.SetInt ("CharActive", g.GetComponent<SelectedCharacter>().pos);
		Application.LoadLevel ("IAP");
	}

	public void SkipIntro() {
		gameIntro.SetActive (false);
		chapters.SetActive (true);
		hasIntroPlayed = true;
		particles.GetComponent<ParticleSystem>().Play();
		aSource.clip = chapter;
		aSource.loop = true;
		aSource.Play ();
	}

	public void ChangeCtrl(int i) {
		PlayerPrefs.SetInt ("controls", i);
	}

	public void ChangeDff(int i) {
		PlayerPrefs.SetInt ("diff", i);
	}

	IEnumerator DelayActive(GameObject g, GameObject h, int i) {
		yield return new WaitForSeconds (i);
		g.SetActive (true);
		h.SetActive (false);
	}

	public void ShowLikeUs() {
		PlayerPrefs.SetInt ("LiekUs", 0);
		liekUs.transform.parent.gameObject.SetActive (true);
	}

	void Update() {
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
		nkCount.text = PlayerPrefs.GetInt ("NKCount").ToString ();
		coinCount.text = PlayerPrefs.GetInt ("CoinCount").ToString ();

		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (liekUs.transform.parent.gameObject.activeInHierarchy || PlayerPrefs.GetInt ("LiekUs") == 3)Application.Quit();
			else if (PlayerPrefs.GetInt ("LiekUs")!=0) ShowLikeUs();
		}
	}

	public void StoreSpeed() {
		PlayerPrefs.SetFloat ("speed", speed.value * 8f);
	}

	public void OpenLiekUs() {
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.lightbreak.snaker");
		liekUs.GetComponent<Animator>().SetTrigger("Out");
		StartCoroutine (DelayLiek ());
	}

	public void OpenFB() {
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.lightbreak.snaker");
	}

	public void RemoveLiekUs() {
		liekUs.GetComponent<Animator>().SetTrigger("Out");
		StartCoroutine (DelayLiek ());
	}

	void OnApplicationQuit() {
		PlayerPrefs.SetString("ActivePane", "menu");
		PlayerPrefs.SetInt ("LiekUs", 0);
		PlayerPrefs.Save();
	}

	void OnApplicationPause(bool pauseStatus) {
		if (pauseStatus) {
			PlayerPrefs.SetString("ActivePane", "menu");
			PlayerPrefs.Save();
		}
	}

	IEnumerator DelayChapters() {
		yield return new WaitForSeconds (45f);
		gameIntro.SetActive (false);
		particles.GetComponent<ParticleSystem>().Play();
		hasIntroPlayed = true;
		PlayerPrefs.SetString("ActivePane", "chapters");
		chapters.SetActive(true);
		menu.SetActive(false);
		aSource.clip = mainMenu;
		aSource.loop = true;
		aSource.Play ();
		for (int i  = 0; i < stages.Length; i++) {
			stages[i].SetActive(false);
		}
	}

	IEnumerator DelayLiek() {
		yield return new WaitForSeconds(0.5f);
		liekUs.transform.parent.gameObject.SetActive (false);
	}

}
