using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RepositionObjs : MonoBehaviour {

	[SerializeField]
	private float xPosition = 0;
	[SerializeField]
	private float yPosition = 0;

	// Use this for initialization
	void Start () {
		Vector3 pos = new Vector3 (xPosition, yPosition, -6.36499f);
		transform.position = pos;
	}
}
