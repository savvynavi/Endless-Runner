using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{

	[SerializeField]
	Transform player;

	Quaternion cameraAngle;
	Vector3 position;

	void Start() {
		position = transform.position;
		cameraAngle = transform.rotation;
	}

	void LateUpdate() {
		//will set the camera up to follow the player around at the distance/angle set in the scene
		if(player != null) {
			transform.position = new Vector3(player.position.x + position.x, position.y, position.z);
		}
	}
}
