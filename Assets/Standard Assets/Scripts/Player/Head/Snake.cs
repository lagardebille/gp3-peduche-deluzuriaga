using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {

	//Speed, magnitude, direction variables
	public bool isPlaying = true;
	public float speed = 2f; //snake speed
	public float currentSpeed = 0f;
	public int dirWhere = 3; //1 = up, 2 = down, 3 = right, 4 left
	public int prevDir = 3;
	public Vector3 dirHash;
	public float scale = 0f;
	public int snakeType;

	public bool isMoving = true;

	//disabled directions variables
	public bool isLeft = true;
	public bool isRight = true;
	public bool isUp = true;
	public bool isDown = true;

	public float frozenTimer = 0f;

	//snake body, waypoints and spacing variables
	bool ate = false; //is the snake already eaten one?
	Transform tr;
	int tailAt = 0;
	public Vector3 trPos;
	public List<Vector3> breadcrumbs;//imaginary points that will be followed by the snake parts
	public List<Transform> segments = new List<Transform> (); //snake body
	public float segmentSpacing; //set controls the spacing between the segments,which is always constant.
	private Transform head;//snake head

	//controller variables
	public GameObject tapControl, swipeControl;
	public bool isSwipingControlActive = false;
	bool controller = false;

	public bool isGameOver = false;

	GameObject prompt;
	void Awake() {
		if (PlayerPrefs.GetInt ("CharActive") != snakeType)
			gameObject.SetActive (false);
	}
	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt ("CharActive",0);

		if (PlayerPrefs.GetInt ("CharActive") == 0)
			dirWhere = 3;
		else
			dirWhere = 0;
		Time.timeScale = 1;
		head = transform;
		speed = PlayerPrefs.GetFloat("speed");

		if (PlayerPrefs.GetInt ("controls") == 1) {
			//tap
			tapControl.SetActive(true);
			swipeControl.SetActive(false);
			isSwipingControlActive = false;
		} else if (PlayerPrefs.GetInt ("controls") == 2) {
			//swipe
			tapControl.SetActive(false);
			swipeControl.SetActive(true);
			isSwipingControlActive = true;
		}
		breadcrumbs = new List<Vector3>();
		breadcrumbs.Add(head.position); //add head first, because that's where the segments will be going.
		for (int i = 0; i < segments.Count; i++) // we have an extra-crumb to mark where the last segment was...
			breadcrumbs.Add(segments[i].position);
		
		trPos = transform.position;
		tr = transform;

		scale = transform.localScale.x;

		prompt = GameObject.FindGameObjectWithTag("Prompt");
	}
	
	// Update is called once per frame
	void Update () {
		scale = transform.localScale.x;
		if (frozenTimer > 0 && isPlaying) {
			frozenTimer -= Time.deltaTime;
		} 
		if (segments.Count > 0 && PlayerPrefs.GetInt ("CharActive") == 0) {
			Vector3 diff = segments [0].position - transform.position;
			//1up 2down 3right 4left
			if (Mathf.Abs (diff.x) > Mathf.Abs (diff.y)) {
				if (diff.x > 0) {
					tailAt = 3;
				} else if (diff.x < 0) {
					tailAt = 4;
				}
			} else if (Mathf.Abs (diff.x) < Mathf.Abs (diff.y)) {
				if (diff.y > 0) {
					tailAt = 1;
				} else if (diff.y < 0) {
					tailAt = 2;
				}
			}
		} else if (segments.Count == 0 && PlayerPrefs.GetInt ("CharActive") == 0)
			tailAt = 0;
		else if (PlayerPrefs.GetInt ("CharActive") != 0) tailAt = 0;
		speed = PlayerPrefs.GetFloat("speed");
		if (PlayerPrefs.GetInt ("controls") == 1) {
			//tap
			tapControl.SetActive(true);
			swipeControl.SetActive(false);
			isSwipingControlActive = false;
		} else if (PlayerPrefs.GetInt ("controls") == 2) {
			//swipe
			tapControl.SetActive(false);
			swipeControl.SetActive(true);
			isSwipingControlActive = true;
		}

		if (Input.GetMouseButtonDown (0) && isMoving) {
			isPlaying = true;
			if (isPlaying){
				currentSpeed = speed;
				prompt.SetActive(false);
			}
		}
		//move the snake to the given direction
		transform.position = Vector3.MoveTowards (transform.position, trPos, Time.deltaTime * currentSpeed);

		if (currentSpeed > 0 && frozenTimer <= 0) {
			if (isSwipingControlActive) {
				if (!swipeControl.GetComponent<SwipeControls> ().isVert) {
					//Debug.Log ("hor");
					if (swipeControl.GetComponent<SwipeControls> ().isRight & tr.position == trPos && tailAt != 3) {
						if (dirWhere != 4) {
						//	Debug.Log ("A");
							transform.rotation = Quaternion.Euler (0, 0, 270);
							if (PlayerPrefs.GetInt("CharActive") == 0) dirWhere = 3;
							if (isRight){
								//Debug.Log ("AA");
								trPos += Vector3.right * scale;
								dirHash = Vector3.right * scale;
							}
						} else if (dirWhere == 4) { 
							//Debug.Log ("B");
							if (isLeft) {
							//	Debug.Log ("BB");
								trPos += Vector3.left * scale;
								dirHash = Vector3.left * scale;
							}
						}
					} else if (swipeControl.GetComponent<SwipeControls> ().isLeft && tr.position == trPos && tailAt != 4) {
						if (dirWhere != 3) {
						//	Debug.Log ("C");
							transform.rotation = Quaternion.Euler (0, 0, 90);
							if (PlayerPrefs.GetInt("CharActive") == 0) dirWhere = 4;
							if (isLeft) {
								//Debug.Log ("CC");
								dirHash = Vector3.left * scale;
								trPos += Vector3.left * scale;
							}
						} else if (dirWhere == 3) {
							//Debug.Log ("D");
							if (isRight) {
								//Debug.Log ("DD");
								trPos += Vector3.right * scale;
								dirHash = Vector3.right * scale;
							}
						}
					} else if (tr.position == trPos)
						trPos += dirHash;
				}
			//going vertically
				else if (swipeControl.GetComponent<SwipeControls> ().isVert) {
					//Debug.Log ("vert");
					if (swipeControl.GetComponent<SwipeControls> ().isUp && tr.position == trPos && tailAt != 1) {
						if (dirWhere != 2) {
						//	Debug.Log ("E");
							transform.rotation = Quaternion.Euler (0, 0, 0);
							if (PlayerPrefs.GetInt("CharActive") == 0) dirWhere = 1;
							if (isUp) {
							//	Debug.Log ("EE");
								dirHash = Vector3.up * scale;
								trPos += Vector3.up * scale;
							}
						} else if (dirWhere == 2) {
							//Debug.Log ("F");
							if (isDown){
								//Debug.Log ("FF");
								trPos += Vector3.down * scale;
								dirHash = Vector3.down * scale;
							}
						}
					} else if (swipeControl.GetComponent<SwipeControls> ().isDown && tr.position == trPos && tailAt != 2) {
						if (dirWhere != 1) {
							//Debug.Log ("G");
							transform.rotation = Quaternion.Euler (0, 0, 180);
							if (PlayerPrefs.GetInt("CharActive") == 0) dirWhere = 2;
							if (isDown) {
								//Debug.Log ("GG");
								dirHash = Vector3.down * scale;
								trPos += Vector3.down * scale;
							}
						} else if (dirWhere == 1) {
							//Debug.Log ("H");
							if (isUp) {
								//Debug.Log ("HH");
								trPos += Vector3.up * scale;
								dirHash = Vector3.up * scale;
							}
						}
					} else if (tr.position == trPos)
						trPos += dirHash;
				}
			
			} else if (!isSwipingControlActive) {
				if (tapControl.GetComponent<TapControls> ().getDir ().Equals ("right") && tr.position == trPos && tailAt != 3) {
					transform.rotation = Quaternion.Euler (0, 0, 270);
					if (PlayerPrefs.GetInt("CharActive") == 0)dirWhere = 3;
					if (isRight)trPos += Vector3.right * scale;
				} else if (tapControl.GetComponent<TapControls> ().getDir ().Equals ("left") && tr.position == trPos && tailAt != 4) {
					transform.rotation = Quaternion.Euler (0, 0, 90);
					if (PlayerPrefs.GetInt("CharActive") == 0)dirWhere = 4;
					if (isLeft)trPos += Vector3.left * scale;
				} else if (tapControl.GetComponent<TapControls> ().getDir ().Equals ("up") && tr.position == trPos && tailAt != 1) {
					transform.rotation = Quaternion.Euler (0, 0, 0);
						if (PlayerPrefs.GetInt("CharActive") == 0)dirWhere = 1;
					if (isUp)trPos += Vector3.up * scale;
				} else if (tapControl.GetComponent<TapControls> ().getDir ().Equals ("down") && tr.position == trPos && tailAt != 2) {
					transform.rotation = Quaternion.Euler (0, 0, 180);
					if (PlayerPrefs.GetInt("CharActive") == 0)dirWhere = 2;
					if (isDown)trPos += Vector3.down * scale;
				} else if (tapControl.GetComponent<TapControls> ().getDir ().Equals ("none")) {
				}
			}
			if (breadcrumbs.Count > 1) {
				float headDisplacement = (head.position - breadcrumbs [0]).magnitude;
			
				if (headDisplacement >= segmentSpacing) {
					breadcrumbs.RemoveAt (breadcrumbs.Count - 1); //remove the last breadcrumb
					breadcrumbs.Insert (0, head.position); // add a new one where head is.
					headDisplacement = headDisplacement % segmentSpacing;
				}
			
				if (headDisplacement != 0) {
					Vector2 pos = Vector2.Lerp (breadcrumbs [1], breadcrumbs [0], headDisplacement / segmentSpacing);
					if (segments.Count > 0) {
						segments [0].position = pos;
						Vector3 vectorToTarget = breadcrumbs [1] - breadcrumbs [0];
						float angle = (Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90f;
						Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
						segments [0].rotation = Quaternion.Slerp (segments [0].rotation, q, Time.deltaTime * 8f);
						for (int i = 1; i < segments.Count; i++) {
							pos = Vector2.Lerp (breadcrumbs [i + 1], breadcrumbs [i], headDisplacement / segmentSpacing);
							segments [i].position = pos;
							vectorToTarget = breadcrumbs [i + 1] - breadcrumbs [i];
							angle = (Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90f;
							q = Quaternion.AngleAxis (angle, Vector3.forward);
							segments [i].rotation = Quaternion.Slerp (segments [i].rotation, q, Time.deltaTime * 8f);
						}
					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (Vector2.Distance (transform.position, coll.transform.position) < (scale * 0.75f) && coll.tag.Equals("Blocks")) {
			GetComponent<Animator> ().SetTrigger ("Die");
			GetComponent<Snake> ().currentSpeed = 0;
			GetComponent<Snake> ().isPlaying = false;
			GetComponent<Snake>().isMoving = false;
			StartCoroutine (GetComponent<DataHolder>().ShowScreen ());
		//	Debug.Log (Vector2.Distance (transform.position, coll.transform.position));
		}
	}
}
