using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	float speed = 5.0f;
	[SerializeField]
	float jumpDist = 5.0f;
	[SerializeField]
	float fallMultiplier = 2.5f;
	[SerializeField]
	float lowJumpMultiplier = 2.0f;
	[SerializeField]
	LayerMask platformLayers;

	public bool isDead = false;

	Rigidbody2D rigidbody;
	BoxCollider2D collider;
	Animator anim;
	bool jumping = false;
	bool jumpBtnDown = false;

	private void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
	}

	private void Update() {

		if(isGrounded() && isDead == false && Input.GetKeyDown(KeyCode.Space)) {
			rigidbody.velocity = Vector2.up * jumpDist;
			//anim.SetBool("isJumping", true);
		}

		//setting the jump velocity so it falls faster
		if(rigidbody.velocity.y < 0) {
			rigidbody.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
		}else if(rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
			rigidbody.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
		}

	}

	private void FixedUpdate() {
		//if dead, stops the player auto-moving forward
		if(isDead == false) {
			float move = Input.GetAxisRaw("Horizontal");
			anim.SetFloat("speed", Mathf.Abs(move * speed));
			rigidbody.velocity = new Vector2(speed * move, rigidbody.velocity.y);
		} else {
			rigidbody.velocity = Vector2.zero;
		}
	}

	bool isGrounded() {
		RaycastHit2D ray = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, platformLayers);
		Debug.Log(ray.collider);
		return ray.collider != null;
	}
}
