using UnityEngine;
using System.Collections;
using ChartboostSDK;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChartboostCaller : MonoBehaviour {

	public List<Button> buttonList = new List<Button>();
	
	// Use this for initialization
	void Start() {
		Chartboost.setAutoCacheAds(true);

		if (Chartboost.hasInterstitial (CBLocation.Default)) {
			if (PlayerPrefs.GetInt ("LiekUs") == 1) {
				Debug.Log("CHARTBOOST INTERSTITIAL");
				Chartboost.showInterstitial(CBLocation.Default);
			}
		} else {
			Chartboost.cacheInterstitial(CBLocation.Default);
		}
	}

	public void ShowVideoAds(int c) {
		if (Chartboost.hasInterstitial (CBLocation.Default)) {
			Debug.Log ("CHARTBOOST REWARDED VADS");
			Chartboost.showInterstitial (CBLocation.Default);
			PlayerPrefs.SetInt ("CoinCount", PlayerPrefs.GetInt ("CoinCount") + c);
		} 
	}

	void Update() {
		if (Chartboost.hasInterstitial (CBLocation.Default)) {
			foreach (Button b in buttonList) {
				b.interactable = true;
			}
			//Chartboost.cacheRewardedVideo (CBLocation.Default);
		} else if (!Chartboost.hasInterstitial (CBLocation.Default)){
			foreach (Button b in buttonList) {
				b.interactable = false;
			}
		}
	}
}
