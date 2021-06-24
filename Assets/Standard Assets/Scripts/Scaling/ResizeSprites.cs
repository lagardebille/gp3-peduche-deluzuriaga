using UnityEngine;
using System.Collections;

public class ResizeSprites : MonoBehaviour {
	
	public float x = 0;
	public float y = 0;
	public float z = 0;
	public float ratio = 0;

	// Use this for initialization
	void Start () {
	#if UNITY_IOS
		float newObjectHeigth = (transform.localScale.y * 7f) / ((12f * Screen.height) / Screen.width);
		float newObjectWidth = (transform.localScale.x * 6.7f) / ((12f * Screen.height) / Screen.width);
		/*if (gameObject.tag != "Enemy" && gameObject.tag != "Player") {
			transform.localScale = new Vector2 (newObjectWidth,transform.localScale.y);
			transform.position = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.5f) / 22.5f, z));
		} else */transform.position = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.5f) / 22.5f, z));
	#else
		float newObjectHeigth, newObjectWidth;
		ratio = (float)Screen.width / (float)Screen.height;
		if (ratio < 1.7) {
			newObjectHeigth = (transform.localScale.y * 10.0f) / ((16.0f * Screen.height) / Screen.width);
			newObjectWidth = (transform.localScale.x * 10.0f) / ((16.0f * Screen.height) / Screen.width);

		} else {
			newObjectHeigth = (transform.localScale.y * 9.0f) / ((16.0f * Screen.height) / Screen.width);
			newObjectWidth = (transform.localScale.x * 9.0f) / ((16.0f * Screen.height) / Screen.width);

		}
		if (gameObject.tag == "Enemy" || gameObject.tag == "Food" || gameObject.tag == "NK") {
			transform.localScale = new Vector2 (newObjectWidth, newObjectHeigth);
		} else if (gameObject.tag == "OuterBlocks"){
			transform.position = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.5f) / 22.5f, z));
		} else {
			transform.localScale = new Vector2 (newObjectWidth, newObjectHeigth);
			transform.position = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.5f) / 22.5f, z));
		}


	#endif


	}
}

/*using UnityEngine;
using System.Collections;

public class ResizeSprites : MonoBehaviour {
	
	public float x = 0;
	public float y = 0;
	public float z = 0;
	
	// Use this for initialization
	void Start () {
		float newObjectHeigth = (transform.localScale.y * 9.0f) / ((16.0f * Screen.height) / Screen.width);
		float newObjectWidth = (transform.localScale.x * 9.0f) / ((16.0f * Screen.height) / Screen.width);
		if (gameObject.tag != "Enemy") {
			transform.localScale = new Vector2 (newObjectWidth, newObjectHeigth);
			transform.position = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.5f) / 22.5f, z));
		}
	}
}*/

