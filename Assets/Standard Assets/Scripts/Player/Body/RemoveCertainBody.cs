using UnityEngine;
using System.Collections;

public class RemoveCertainBody : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RemoveOnIndex(int i) {
		GetComponent<Snake> ().segments [i].gameObject.SetActive (false);
		GetComponent<Snake> ().segments.RemoveAt (i);
		foreach (Transform t in GetComponent<Snake>().segments) {
			t.gameObject.GetComponent<Animator>().SetTrigger("Sleep");
		}
		GetComponent<DataHolder> ().miniCount--;
		GetComponent<DataHolder> ().score -= 100;
		GetComponent<DataHolder> ().isCollided = false;
	}

	public void RemoveAll() {
		int j = GetComponent<Snake> ().segments.Count;
		for (int i = 0; i < GetComponent<Snake> ().segments.Count; i++) {
			GetComponent<Snake> ().segments [i].gameObject.SetActive(false);
		}

		GetComponent<Snake> ().segments.Clear ();
		foreach (Transform t in GetComponent<Snake>().segments) {
			t.gameObject.GetComponent<Animator>().SetTrigger("Sleep");
		}
		GetComponent<DataHolder> ().miniCount = 0;
		GetComponent<DataHolder> ().score -= (100*j);
		GetComponent<DataHolder> ().isCollided = false;
	}

	public void RemoveFrom(int i) {
		int range = GetComponent<Snake> ().segments.Count - i;
		for (int a = i; a < GetComponent<Snake> ().segments.Count; a++) {
			GetComponent<Snake> ().segments [a].gameObject.SetActive(false);
		}
		GetComponent<Snake> ().segments.RemoveRange (i, range);
		foreach (Transform t in GetComponent<Snake>().segments) {
			t.gameObject.GetComponent<Animator>().SetTrigger("Sleep");
		}
		GetComponent<DataHolder> ().miniCount -= range;
		GetComponent<DataHolder> ().score -= (100*range);
		GetComponent<DataHolder> ().isCollided = false;
	}

	public void RemoveRange(int i, int r) {
		if (r <= GetComponent<Snake> ().segments.Count) {
			for (int a = i; a < r; a++) {
				GetComponent<Snake> ().segments [a].gameObject.SetActive(false);
			}
			GetComponent<Snake> ().segments.RemoveRange (i, r);
			foreach (Transform t in GetComponent<Snake>().segments) {
				t.gameObject.GetComponent<Animator> ().SetTrigger ("Sleep");
			}
			GetComponent<DataHolder> ().miniCount -= r;
			GetComponent<DataHolder> ().score -= (100 * r);
			GetComponent<DataHolder> ().isCollided = false;
		} else {
			int j = GetComponent<Snake> ().segments.Count;
			for (int k = 0; k < GetComponent<Snake> ().segments.Count; k++) {
				Destroy (GetComponent<Snake> ().segments [k].gameObject);
			}
			GetComponent<Snake> ().segments.Clear ();
			foreach (Transform t in GetComponent<Snake>().segments) {
				t.gameObject.GetComponent<Animator>().SetTrigger("Sleep");
			}

			GetComponent<DataHolder> ().miniCount = 0;
			GetComponent<DataHolder> ().score -= (100 * j);
			GetComponent<DataHolder> ().isCollided = false;
		}
	}
}
