using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

	[SerializeField]
	float paralax;

	float length;
	float startPos;
	Camera cam;

	private void Start() {
		startPos = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
		cam = Camera.main;
	}

	private void FixedUpdate() {

		//moves the game object away from the start position x so it scrolls
		float tmpDist = cam.transform.position.x * (1 - paralax);
		float DistFromStart = cam.transform.position.x * paralax;
		transform.position = new Vector3(startPos + DistFromStart, transform.position.y, transform.position.z);

		//resets the starting position of the gameobject if it goes to far away from the start pos
		if(tmpDist > (startPos + length)) {
			startPos += length;
		}else if(tmpDist < (startPos - length)) {
			startPos -= length;
		}
	}
}
