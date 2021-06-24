using UnityEngine;
using System.Collections;

public class DestroyOnTime : MonoBehaviour {

	// Use this for initialization
	public float timer = 0f;
	GameObject snake;
	GameObject runtime;

	void Start() {
		snake = GameObject.FindGameObjectWithTag("Player");
	}
	// Update is called once per frame
	void Update () {
		runtime = GameObject.FindGameObjectWithTag("Runtime");
		snake = GameObject.FindGameObjectWithTag("Player");
		if (snake.GetComponent<DataHolder> ().radiusTimer > 0) {
			GetComponent<Animator>().SetTrigger("Radius");
		} else GetComponent<Animator>().SetTrigger("Idle");
		timer -= Time.deltaTime;
		if (timer <= 0) {
			GetComponent<Animator>().SetTrigger("Die");
			StartCoroutine(DelayDestroy());
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Blocks" || coll.tag == "Body" || coll.tag == "Invincible" || coll.tag == "Radius" || 
		    coll.tag == "Freeze" || coll.tag == "PassThru" || coll.tag == "Enemy") {
			runtime.GetComponent<SpawnNK>().Spawn();
			Destroy(gameObject);
		}
	}

	IEnumerator DelayDestroy() {
		yield return new WaitForSeconds(0.5f);
		Destroy (gameObject);
	}
}
