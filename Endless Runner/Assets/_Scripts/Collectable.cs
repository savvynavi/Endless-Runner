using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public int points;

	Animator anim;

	private void Start() {
		anim = GetComponent<Animator>();
	}



	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			Debug.Log("strawberry get!");
			if(anim != null) {
				anim.SetBool("collected", true);
			}
		}
	}

	public void collectAnim() {
		//once collection anim is over, turn object off and reset to idle animation
		gameObject.SetActive(false);
		anim.SetBool("collected", false);
	}
}
