using UnityEngine;
using System.Collections;

public class SwipeControls : MonoBehaviour {

	private float downX, downY;
	private float upX, upY;

	public bool isVert = false;
	public bool isLeft = false;
	public bool isRight = false;
	public bool isUp = false;
	public bool isDown = false;

	void OnMouseDown() {
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Idle");
		GameObject.FindGameObjectWithTag("Player").GetComponent<Snake>().currentSpeed = PlayerPrefs.GetFloat("speed");
		downX = Input.mousePosition.x;
		downY = Input.mousePosition.y;
	}

	void OnMouseUp() {

	/*	isRight = false;
		isLeft = false;
		isUp = false;
		isDown = false;*/

		upX = Input.mousePosition.x;
		upY = Input.mousePosition.y;

		float distX = Mathf.Abs(Mathf.Abs (downX) - Mathf.Abs (upX));
		float distY =  Mathf.Abs(Mathf.Abs (downY) - Mathf.Abs (upY));


		if (distX > distY) {
			isVert = false;
			if (downX - upX < 0 ) {
				isRight = true;
				isLeft = false;
				isUp = false;
				isDown = false;
			}
			else if (downX - upX > 0) {
				isRight = false;
				isLeft = true;
				isUp = false;
				isDown = false;
			}
		} else if (distX < distY) {
			isVert = true;
			if (downY - upY < 0 ) {
				isRight = false;
				isLeft = false;
				isUp = true;
				isDown = false;
			}
			else if (downY - upY > 0 ) {
				isRight = false;
				isLeft = false;
				isUp = false;
				isDown = true;
			}
		}
	}
}
