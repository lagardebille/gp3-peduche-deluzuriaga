using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IAPChangeDescription : MonoBehaviour {

	public Sprite[] description = new Sprite[5];
	// Use this for initialization

	public void ChangeDescription(int i) {
		GetComponent<Image> ().sprite = description [i];
	}
}
