using UnityEngine;
using System.Collections;

public class TapControls : MonoBehaviour {

	Transform snakeHead;
	private string direction = "none";

	public AudioClip tap;
	AudioSource audio;


	void Start() {
		snakeHead = GameObject.FindGameObjectWithTag("Player").transform;
		audio = Camera.main.GetComponent<AudioSource> ();
	}
	
	void OnMouseDown() {
		snakeHead = GameObject.FindGameObjectWithTag("Player").transform;
		//snakeHead.GetComponent<Animator>().SetTrigger("Idle");
		if (snakeHead != null && Time.timeScale != 0) {
			audio.PlayOneShot (tap, PlayerPrefs.GetInt ("Sound"));
			Vector3 mPos = Input.mousePosition;
			mPos.z = 10;
			mPos = Camera.main.ScreenToWorldPoint (mPos);
			float distX = Mathf.Abs (mPos.x - snakeHead.position.x);
			float distY = Mathf.Abs (mPos.y - snakeHead.position.y);

			if (distX > distY || distX < distY) {
				if (mPos.x > snakeHead.position.x && snakeHead.GetComponent<Snake> ().dirWhere != 4)
				{
					direction = "right";
				} 
				else if (mPos.x < snakeHead.position.x && snakeHead.GetComponent<Snake> ().dirWhere != 3)
				{
					direction = "left";
				}
				else {
					if (mPos.y > snakeHead.position.y && snakeHead.GetComponent<Snake> ().dirWhere != 2) 
					{
						direction = "up";
					}
					else if (mPos.y < snakeHead.position.y && snakeHead.GetComponent<Snake> ().dirWhere != 1)
					{
						direction = "down";
					}
				}
            } 
        }
	}

	public string getDir() {
		return direction;
	}
}
