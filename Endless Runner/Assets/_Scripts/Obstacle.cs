using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	Animator anim;

	private void Start() {
		anim = GetComponent<Animator>();
	}

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "") {

		}
	}
}
