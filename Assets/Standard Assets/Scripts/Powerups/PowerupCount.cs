using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerupCount : MonoBehaviour {

	/*public Image[] digits = new Image[2];
	public Sprite[] dig = new Sprite[10];*/
	public string type;
	public int count = 0;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		count = PlayerPrefs.GetInt ("pow_" + type);
		/*int tens = count / 10;
		int ones = count % 10;

		digits [0].sprite = dig [tens];
		digits [1].sprite = dig [ones];*/

		if (count < 10)
			GetComponent<Text> ().text = 0 + count.ToString ();
		else if (count >= 10)
			GetComponent<Text> ().text = count.ToString ();
		SetInteractable();
	}

	void SetInteractable() {
		if (count <= 0)
			transform.parent.gameObject.GetComponent<Button>().interactable = false;
	}
}
