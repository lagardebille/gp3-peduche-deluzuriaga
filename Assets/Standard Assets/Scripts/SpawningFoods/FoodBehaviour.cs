using UnityEngine;
using System.Collections;

public class FoodBehaviour : MonoBehaviour {

	GameObject snakeHead;
	public Sprite type1, type2, type3, type4;
	public bool isStoned = false;
	// Use this for initialization
	void Start () {
		snakeHead = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		snakeHead = GameObject.FindGameObjectWithTag("Player");
		if (snakeHead != null) {
			if (snakeHead.GetComponent<DataHolder> ().radiusTimer > 0 && !isStoned) {
				GetComponent<Animator> ().SetTrigger ("Radius");
				GetComponent<SpriteRenderer> ().sprite = type2;
			} else if (snakeHead.GetComponent<DataHolder> ().radiusTimer <= 0 && !isStoned) {
				GetComponent<Animator> ().SetTrigger ("Normal");
				GetComponent<SpriteRenderer> ().sprite = type1;
			} else if (snakeHead.GetComponent<DataHolder> ().radiusTimer > 0 && isStoned) {
				GetComponent<Animator> ().SetTrigger ("StonedRad");
				GetComponent<SpriteRenderer> ().sprite = type1;
			} else if (snakeHead.GetComponent<DataHolder> ().radiusTimer <= 0 && isStoned) {
				GetComponent<Animator> ().SetTrigger ("Stoned");
				GetComponent<SpriteRenderer> ().sprite = type3;
			}

			if (isStoned) GetComponent<CircleCollider2D>().enabled = false;
			else if (!isStoned) GetComponent<CircleCollider2D>().enabled = true;
		}
	}
	void OnTriggerEnter2D(Collider2D coll) {
		//Debug.Log (coll.name);
		if (coll.tag == "Blocks" || coll.tag == "Body" || coll.tag == "Invincible" || coll.tag == "Radius" || 
		    coll.tag == "Freeze" || coll.tag == "PassThru")Destroy(gameObject);
		else if (coll.name.StartsWith("Heng") || coll.name.StartsWith("Alpha") || coll.name.StartsWith("Burner1") 
		         || coll.name.StartsWith("Burner2") || coll.name.StartsWith("Spike3")) Destroy(gameObject);
	}
}
