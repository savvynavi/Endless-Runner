using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReset : MonoBehaviour{

	[SerializeField]
	float buffer;

	Camera cam;

	private void Start() {
		cam = Camera.main;
	}


	//if platform falls behind the point, it is turned off
	private void Update() {

		//gets camera width and then sets the destruction point to be the cameras position minus the cams half width minus a buffer
		float aspect = (float)Screen.width / (float)Screen.height;
		float camHalfHeight = cam.orthographicSize;
		float camHalfWidth = aspect * camHalfHeight;

		float destroyPoint = cam.transform.position.x - camHalfWidth - buffer;
		if(transform.position.x < destroyPoint) {
			gameObject.SetActive(false);
		}
	}

}
