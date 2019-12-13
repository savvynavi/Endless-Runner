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
	public bool isPaused = false;
	public bool hitObstacle = false;

	float move;
	bool jumpClick = false;
	bool jumpBtnHeld = false;
	Rigidbody2D rigidbody;
	BoxCollider2D collider;
	Animator anim;

	public Animator Anim {
		get { return anim; }
		private set { }
	}


	private void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
	}

	private void Update() {
		//set imput for the jumps
		move = Input.GetAxisRaw("Horizontal");


#if UNITY_ANDROID
		if(Input.GetMouseButtonDown(0)) {
			jumpClick = true;
		}

		if(Input.GetMouseButton(0)) {
			jumpBtnHeld = true;
		} else {
			jumpBtnHeld = false;
		}

#else

		if(Input.GetButtonDown("Jump")) {
			jumpClick = true;
			anim.SetBool("isJumping", true);
		}

		if(Input.GetButton("Jump")) {
			jumpBtnHeld = true;
		} else {
			jumpBtnHeld = false;
		}
#endif
		anim.SetBool("isJumping", !isGrounded());
		anim.SetFloat("falling", rigidbody.velocity.y);

		//if game paused, freese the animation
		anim.enabled = !isPaused;
	}

	private void FixedUpdate() {

		//if the player is grounded and jumping, while also not dead or paused, the character will jump
		if(isGrounded() && isDead == false && isPaused == false && jumpClick) {
			rigidbody.velocity += Vector2.up * jumpDist;
			jumpClick = false;

		}

		//setting the jump velocity so it falls faster
		if(rigidbody.velocity.y < 0) {
			rigidbody.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
		} else if(rigidbody.velocity.y > 0 && !jumpBtnHeld) {
			rigidbody.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
		}



		//if dead, stops the player auto-moving forward
		if(isDead == false && isPaused == false) {
			//moves the player to the right (currently set to player input for testing)
			anim.SetFloat("speed", Mathf.Abs(speed ));
			rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
		} else {
			rigidbody.velocity = Vector2.zero;
		}
	}

	//returns true if the player is grounded, false otherwise
	bool isGrounded() {
		RaycastHit2D ray = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, platformLayers);
		return ray.collider != null;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		//if the player collides with an obstical, sets hitObstacle to true + plays death anim
		if(collision.gameObject.tag == "Obstacle") {
			Debug.Log("hit buzzsaw!");
			hitObstacle = true;
			anim.SetBool("isDead", true);
		}
	}
}
