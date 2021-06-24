using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectedPowerUp : MonoBehaviour {

	public int isSelected;
	public Sprite[] sheets = new Sprite[2];
	public int type;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		GetComponent<Image> ().sprite = sheets [isSelected];
	}
}
