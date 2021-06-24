using UnityEngine;
using System.Collections;

public class DelayBehaviour : MonoBehaviour {

	void Start () {
		GetComponent<CircleCollider2D> ().enabled = false;
		GetComponent<SpriteRenderer> ().enabled = false;
		StartCoroutine (DelayAll ());
	}
	
	IEnumerator DelayAll() {
		yield return new WaitForSeconds (0.25f);
		GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (1f);
		GetComponent<CircleCollider2D> ().enabled = true;
	}
}
