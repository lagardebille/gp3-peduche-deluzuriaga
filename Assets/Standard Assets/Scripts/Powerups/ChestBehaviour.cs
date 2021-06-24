using UnityEngine;
using System.Collections;

public class ChestBehaviour : MonoBehaviour {
	
	public int type = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Blocks" || coll.tag == "Body" || coll.tag == "Invincible" || coll.tag == "Radius" || 
		    coll.tag == "Freeze" || coll.tag == "PassThru" || coll.tag == "Enemy") {
			int x = (int)Random.Range(1,38);
			int y = (int)Random.Range(1,18);
			Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3((x+0.5f )/40f,(y+0.85f)/22.5f,3f));
			transform.position = pos;
		}
	}
}
