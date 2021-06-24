using UnityEngine;
using System.Collections;

public class Spike3Behaviour : MonoBehaviour {

	GameObject snakeHead;
	public int dir;
	int axis;
	Vector3 magDir;
	float borderLine;
	public bool willReverse = true;
	// Use this for initialization
	void Start () {
		snakeHead = GameObject.FindGameObjectWithTag("Player");
		switch (dir) {
			case 1:
				magDir = Vector3.right;
				break;
			case 2:
				magDir = Vector3.left;
				break;
			default:
				return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
		snakeHead = GameObject.FindGameObjectWithTag("Player");
		if (GetComponent<EnemySpeed> ().isTraitEnabled && snakeHead != null) {
			transform.Translate (magDir * Time.deltaTime * GetComponent<EnemySpeed>().speed);
			if (magDir == Vector3.right){
				if (transform.position.x > 28 || transform.position.x < -28 || transform.position.y > 19 || transform.position.y < -19){
					if (willReverse) {
						magDir = Vector3.left;
						GetComponent<EnemySpeed>().speed = 15;
						willReverse = false;
					}
					else if (!willReverse)Destroy(gameObject);
				}
			} else if (magDir == Vector3.left) {
				if (transform.position.x < -28 || transform.position.x > 28 || transform.position.y < -19 || transform.position.y > 19)  {
					if (willReverse) {
						magDir = Vector3.right;
						GetComponent<EnemySpeed>().speed = 15;
						willReverse = false;
					}
					else if (!willReverse)Destroy(gameObject);
				}
			}
		}
	}
	
	/*void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Body") {
			GetComponent<EnemySpeed>().speed = 0;
			//GetComponent<Animator>().SetTrigger("Die");
			StartCoroutine(DelayDeath());
		}
	}*/
	
	/*IEnumerator DelayDeath() {
		yield return new WaitForSeconds (1.5f);
		Destroy (gameObject);
	}*/
}
