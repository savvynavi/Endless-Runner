using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReset : MonoBehaviour{

	[SerializeField]
	GameObject DestroyPoint;

	//finds the destruction point in the world
	private void Start() {
		DestroyPoint = GameObject.Find("platformDestroyPoint");
	}


	//if platform falls behind the point, then it is turned off
	private void Update() {
		if(transform.position.x < DestroyPoint.transform.position.x) {
			gameObject.SetActive(false);
		}
	}

}
