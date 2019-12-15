using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	[SerializeField]
	int points;

	Animator anim;
	AudioSource audio;

	private void Start() {
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}

	//if the collectable collides with a player it plays the collected anim/plays sound
	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			if(anim != null) {
				anim.SetBool("collected", true);
			}
			if(audio != null) {
				audio.Play();
			}
		}
	}

	public void collectAnim() {
		//once collection anim is over, turn object off and reset to idle animation
		ScoreManager.instance.AddPoints(points);
		gameObject.SetActive(false);
		anim.SetBool("collected", false);
	}
}
