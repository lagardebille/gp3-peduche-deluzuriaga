using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Prime31;
using System.Collections.Generic;

public class IAPBuy : MonoBehaviour {

	public Sprite[] IAPPopups = new Sprite[12];
	public Sprite[] IAPDesc = new Sprite[12];

	public Text gold_ui, nk_ui;

	public int[] coint_amnt;
	public int[] item_amnt;
	int coin_to_be_added;
	int current_item_index;
	string current_sku;
	public string[] skus;
	public string pubId;

	#if UNITY_IOS
	private List<StoreKitProduct> _products;
	List<StoreKitTransaction> transactionList;
	public List<string> purchases = new List<string>();
	#endif

	public StartMenu sm;
	// Use this for initialization
	void Start() {
	#if UNITY_ANDROID
		GoogleIAB.init (pubId);
		GoogleIAB.enableLogging( true );
		GoogleIAB.queryInventory (skus);
	#elif UNITY_IOS
		StoreKitBinding.requestProductData( skus );
		StoreKitManager.productListReceivedEvent += allProducts => {
			Debug.Log( "received total products: " + allProducts.Count );
			_products = allProducts;
		};
		transactionList = StoreKitBinding.getAllSavedTransactions();
		foreach (StoreKitTransaction skt in transactionList) {
			if (!purchases.Contains(skt.productIdentifier))
				purchases.Add(skt.productIdentifier);
		}
	#endif
	}

	void Update() {
		gold_ui.text = PlayerPrefs.GetInt ("CoinCount").ToString ();
		nk_ui.text = PlayerPrefs.GetInt ("NKCount").ToString ();
	}

	public void ShowItem(int i) {
		foreach (Transform child in transform) 
			child.gameObject.SetActive(true);
		transform.GetChild (1).GetComponent<Image> ().sprite = IAPPopups [i];
		transform.GetChild (2).GetComponent<Image> ().sprite = IAPDesc [i];
		current_item_index = i;
	}

	public void HideIten() {
		foreach (Transform child in transform) 
			child.gameObject.SetActive(false);
	}

	public void PurchaseItem(int i) {
	#if UNITY_ANDROID
		GoogleIAB.queryInventory (skus);
		GoogleIAB.purchaseProduct (skus[i]);
	#elif UNITY_IOS
		StoreKitBinding.purchaseProduct(skus[i], 1);
		StoreKitBinding.requestProductData( skus );
	#endif
		current_sku = skus [i];
		coin_to_be_added = coint_amnt [i];
	}

	public void AddCoin() {
		#if UNITY_ANDROID
			GoogleIAB.consumeProduct (current_sku);
		#endif
			PlayerPrefs.SetInt ("CoinCount", PlayerPrefs.GetInt ("CoinCount") + coin_to_be_added);
	}
	
	public void PurchaseFromCoins() {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}
		int current_gold = PlayerPrefs.GetInt ("CoinCount");
		int current_nk = PlayerPrefs.GetInt ("NKCount");
		if (current_item_index != 10 && item_amnt [current_item_index] <= current_gold) {
			PlayerPrefs.SetInt ("CoinCount", PlayerPrefs.GetInt ("CoinCount") - item_amnt [current_item_index]);
			switch (current_item_index) {
			case 0:
				PlayerPrefs.SetInt ("Continue", PlayerPrefs.GetInt ("Continue") + 1);
				break;
			case 1:
				PlayerPrefs.SetInt ("pow_3", PlayerPrefs.GetInt ("pow_3") + 1);
				break;
			case 2:
				PlayerPrefs.SetInt ("pow_2", PlayerPrefs.GetInt ("pow_2") + 1);
				break;
			case 3:
				PlayerPrefs.SetInt ("pow_1", PlayerPrefs.GetInt ("pow_1") + 1);
				break;
			case 4:
				PlayerPrefs.SetInt ("pow_5", PlayerPrefs.GetInt ("pow_5") + 1);
				break;
			case 5:
				PlayerPrefs.SetInt ("pow_4", PlayerPrefs.GetInt ("pow_4") + 1);
				break;
			case 6:
				PlayerPrefs.SetInt ("NoAds", 1);
				break;
			case 7:
				PlayerPrefs.SetInt ("pow_3", PlayerPrefs.GetInt ("pow_3") + 1);
				PlayerPrefs.SetInt ("pow_2", PlayerPrefs.GetInt ("pow_2") + 1);
				break;
			case 8:
				PlayerPrefs.SetInt ("pow_4", PlayerPrefs.GetInt ("pow_4") + 1);
				PlayerPrefs.SetInt ("pow_1", PlayerPrefs.GetInt ("pow_1") + 1);
				break;
			case 9:
				PlayerPrefs.SetInt ("Continue", PlayerPrefs.GetInt ("Continue") + 1);
				PlayerPrefs.SetInt ("NoAds", 1);
				break;
			case 11:
				PlayerPrefs.SetInt ("Phantom", 1);
				break;
			}
		} else if (current_item_index == 11 && item_amnt [current_item_index] <= current_nk) {
			PlayerPrefs.SetInt ("NKCount", PlayerPrefs.GetInt ("NKCount") - item_amnt [current_item_index]);
			PlayerPrefs.SetInt("Zaex_Ak", 1);
		} else {
			sm.SwitchTabs (2);
		}
	}
	
}
