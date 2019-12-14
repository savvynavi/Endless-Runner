using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundManager : MonoBehaviour {

	[SerializeField]
	float buffer;
	[SerializeField]
	List<ObjectPool> pools;

	float horizontalCamExtant;
	Camera cam;



	private void Start() {
		cam = Camera.main;
	}

}
