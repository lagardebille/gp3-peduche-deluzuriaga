using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

	public bool isStoned = false;
	Sprite thisSprite;
	public Sprite stone;
	public float timer = 0;
	BoxCollider2D[] myColliders ;
	public GameObject snake;
	// Use this for initialization
	void Start () {
		myColliders = gameObject.GetComponents<BoxCollider2D>();
		thisSprite = GetComponent<SpriteRenderer> ().sprite;
		snake = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update() {
		if (snake == null) snake = GameObject.FindGameObjectWithTag ("Player");
		if (snake != null) timer = snake.GetComponent<DataHolder> ().passTimer;	
		if (timer > 0 && gameObject.tag == "Blocks") {
			foreach(BoxCollider2D bc in myColliders) bc.enabled = false;
		} else if (timer <= 0 && gameObject.tag == "Blocks") {
			GetComponent<SpriteRenderer>().enabled = true;
			foreach(BoxCollider2D bc in myColliders) bc.enabled = true;
		}

		if (!isStoned)
			GetComponent<SpriteRenderer> ().sprite = thisSprite;
		else if (isStoned)
			GetComponent<SpriteRenderer> ().sprite = stone;
	}
}
