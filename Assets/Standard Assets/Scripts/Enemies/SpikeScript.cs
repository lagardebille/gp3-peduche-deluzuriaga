using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {
	
	Rigidbody2D myRigidbody;
	Vector2 oldVel;
	public float timer  = 10f;
	public Vector2 savedVelocity;
	public float savedAngularVelocity;
	GameObject snake;
	float speed = 4f;
	bool paused = false;
	
	void Start () {
		snake = GameObject.FindGameObjectWithTag("Player");
		myRigidbody = GetComponent<Rigidbody2D>();
		//myRigidbody.velocity = new Vector2 (1, 0.5f)*4f;
	}

	void FixedUpdate() {
		if (snake.GetComponent<DataHolder> ().freezeTimer <= 0) {
			if (myRigidbody.isKinematic) {
				myRigidbody.velocity = savedVelocity;
				myRigidbody.angularVelocity = savedAngularVelocity;
				myRigidbody.WakeUp();
				paused = false;
				myRigidbody.isKinematic = false;
			}
			if (PlayerPrefs.GetInt ("diff") <= 1)
				gameObject.SetActive(false);
			timer -= Time.deltaTime;
			if (timer < 0)
				gameObject.SetActive(false);
			if (GetComponent<EnemySpeed> ().isTraitEnabled)
				oldVel = myRigidbody.velocity;
		} else if (snake.GetComponent<DataHolder> ().freezeTimer > 0){
			if (!paused) {
				savedVelocity = myRigidbody.velocity;
				savedAngularVelocity = myRigidbody.angularVelocity;
				myRigidbody.isKinematic = true;
				paused = true;
			}
		}
	}

	public void EnableColl() {
		foreach(Collider2D c in GetComponents<Collider2D> ()) {
			c.enabled = true;
		}
	}

	void OnCollisionEnter2D (Collision2D c) {
		if (c.gameObject.name != "Snake_1") {
			ContactPoint2D cp = c.contacts [0];
			// calculate with addition of normal vector
			// myRigidbody.velocity = oldVel + cp.normal*2.0f*oldVel.magnitude;
		
			// calculate with Vector3.Reflect
			myRigidbody.velocity = Vector3.Reflect (oldVel, cp.normal);
		
			// bumper effect to speed up ball
			//myRigidbody.velocity += cp.normal;
		}
	}
}