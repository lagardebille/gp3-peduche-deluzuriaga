using UnityEngine;
using System.Collections;

public class SpawnNK : MonoBehaviour {

	public GameObject nkPrefab;
	GameObject snakeHead;
	// Use this for initialization
	void Start () {
		snakeHead = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	public void Spawn () {
		Debug.Log("SPAWN NK");
		float x = 0;
		float y = (int)Random.Range (-11, 7);
		if (y > 0) 
			y += 0.25f;
		else
			y -= 0.75f;
		float ratio = (float)Screen.width / (float)Screen.height;
		//Debug.Log (ratio);
		if (ratio >= 1.77) {//169
			x = (int)Random.Range(-20,20) + 0.5f;
		} else if (ratio < 1.77 && ratio >= 1.59f) { //1610
			x = (int)Random.Range(-18,18) + 0.5f;
		} else if (ratio < 1.59f && ratio >= 1.49f) { //32
			x = (int)Random.Range(-17,17) + 0.5f;
		} else if (ratio < 1.49f) { //43
			x = (int)Random.Range(-15,15) + 0.5f;
		} 
		Vector3 pos = new Vector3(x,y,-6.36499f);
		// Instantiate the food at (x, y)
		Instantiate(nkPrefab, pos, Quaternion.identity); // default rotation
	}
}
