using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.GameCenter;
using GooglePlayGames.BasicApi;

public class GPServices : MonoBehaviour {
	// Use this for initialization
	private bool mAuthenticating = false;
	
	static bool sAutoAuthenticate = true;
	static bool sAutoReport = true;

	#if UNITY_IOS
	ILeaderboard leaderboard;
	#endif
	void Start () {
		if (Authenticated)
			SetAchievements (PlayerPrefs.GetInt ("HighestLevel"));
		else
			Authenticate ();
		#if UNITY_IOS
		leaderboard = Social.CreateLeaderboard();
		leaderboard.id = GameConstants.leaderboard_highest_level_achieved_hard;//this will be your game services leaderboard ID
		leaderboard.LoadScores((bool success) => {
			// handle success or failure <-----
		});
		GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
		#endif
	}
	
	void Update() {

	}
	
	public void Authenticate() {
		if (Authenticated || mAuthenticating) {
			return;
		}
		#if UNITY_ANDROID
		// Enable/disable logs on the PlayGamesPlatform
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.EnableSavedGames()
				.Build();
		PlayGamesPlatform.InitializeInstance(config);
		// Activate the Play Games platform. This will make it the default
		// implementation of Social.Active
		PlayGamesPlatform.Activate();
		// Set the default leaderboard for the leaderboards UI
		//((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI(GameConstants.leaderboard_highest_level_achieved_hard);
		// Sign in to Google Play Games
		#endif
		mAuthenticating = true;
		Social.localUser.Authenticate((bool success) => {
			mAuthenticating = false;
		});
	}
	
	public bool Authenticated {
		get {
			return Social.Active.localUser.authenticated;
		}
	}
	
	void SetAchievements(int i) {
		/*switch (i) {
		case 1: 
			if (Authenticated)
				Social.ReportProgress (GameConstants.achievement_stepping_stone, 100.0f, (bool success) => {});
			else Authenticate();
			break;
			case 2: 
			if (Authenticated)
				Social.ReportProgress (GameConstants.achievement_surpassed_level_2, 100.0f, (bool success) => {});
			else Authenticate();
			break;
		case 3: 
			if (Authenticated)
				Social.ReportProgress (GameConstants.achievement_surpassed_level_3, 100.0f, (bool success) => {});
			else Authenticate();
			break;
		case 4: 
			if (Authenticated)
				Social.ReportProgress (GameConstants.achievement_surpassed_level_4, 100.0f, (bool success) => {});
			else Authenticate();
			break; 
		}
		Social.ReportScore(i, GameConstants.leaderboard_highest_level, (bool success) => {
			// handle success or failure <-----
		});*/
	}
	
	public void ShowLeaderBoard() {
		if (Authenticated) {
			/*Social.ReportScore(PlayerPrefs.GetInt("HighestLevel"), GameConstants.leaderboard_highest_level, (bool success) => {
				// handle success or failure
			});*/
			Social.ShowLeaderboardUI ();
		} else
			Authenticate ();
	}
	
	public void ShowAchievements() {
		if (Authenticated) {
			Social.ShowAchievementsUI();
		} else
			Authenticate ();
	}
}