using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Facebook.Unity;
using Facebook.Unity.Mobile;

public class ShareApp : MonoBehaviour {

	void Awake ()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init (InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp ();

		}
	}

	public void Share() {
		FB.Mobile.ShareDialogMode = ShareDialogMode.AUTOMATIC;
		FB.ShareLink(
			new Uri("https://www.facebook.com/snakeRedemption/"),
			callback: ShareCallback
			);
	}
	
	private void InitCallback () {
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}
	
	private void OnHideUnity (bool isGameShown) {
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	private void ShareCallback (IShareResult result) {
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
			Debug.Log("ShareLink Error: "+result.Error);
		} else if (!String.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} else {
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
	}
}
