using UnityEngine;
using System.Collections;

public class UnlockAll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("C1Level0Finished", 1);
		PlayerPrefs.SetInt ("C1Level1Finished", 1);
		PlayerPrefs.SetInt ("C1Level2Finished", 1);
		PlayerPrefs.SetInt ("C1Level3Finished", 1);
		PlayerPrefs.SetInt ("C1Level4Finished", 1);
		PlayerPrefs.SetInt ("C1Finished", 1);

		PlayerPrefs.SetInt ("C2Level0Finished", 1);
		PlayerPrefs.SetInt ("C2Level1Finished", 1);
		PlayerPrefs.SetInt ("C2Level2Finished", 1);
		PlayerPrefs.SetInt ("C2Level3Finished", 1);
		PlayerPrefs.SetInt ("C2Level4Finished", 1);
		PlayerPrefs.SetInt ("C2Finished", 1);

		PlayerPrefs.SetInt ("C3Level0Finished", 1);
		PlayerPrefs.SetInt ("C3Level1Finished", 1);
		PlayerPrefs.SetInt ("C3Level2Finished", 1);
		PlayerPrefs.SetInt ("C3Level3Finished", 1);
		PlayerPrefs.SetInt ("C3Level4Finished", 1);
		PlayerPrefs.SetInt ("C3Finished", 1);

		PlayerPrefs.SetInt ("C4Level0Finished", 1);
		PlayerPrefs.SetInt ("C4Level1Finished", 1);
		PlayerPrefs.SetInt ("C4Level2Finished", 1);
		PlayerPrefs.SetInt ("C4Level3Finished", 1);
		PlayerPrefs.SetInt ("C4Level4Finished", 1);
		PlayerPrefs.SetInt ("C4Finished", 1);

		PlayerPrefs.SetInt ("C5Level0Finished", 1);
		PlayerPrefs.SetInt ("C5Level1Finished", 1);
		PlayerPrefs.SetInt ("C5Level2Finished", 1);
		PlayerPrefs.SetInt ("C5Level3Finished", 1);
		PlayerPrefs.SetInt ("C5Level4Finished", 1);
		PlayerPrefs.SetInt ("C5inished", 1);

		PlayerPrefs.SetInt ("C6Level0Finished", 1);
		PlayerPrefs.SetInt ("C6Level1Finished", 1);
		PlayerPrefs.SetInt ("C6Level2Finished", 1);
		PlayerPrefs.SetInt ("C6Level3Finished", 1);
		PlayerPrefs.SetInt ("C6Level4Finished", 1);
		PlayerPrefs.SetInt ("C6Finished", 1);

		PlayerPrefs.SetInt("Phantom", 1);
		PlayerPrefs.SetInt("Zaex_Ak", 1);
		PlayerPrefs.SetInt("perfect_plays", 8);
		PlayerPrefs.SetInt("NKCount", 65);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
