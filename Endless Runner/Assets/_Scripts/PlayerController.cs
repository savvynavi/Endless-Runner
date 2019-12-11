using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	float speed = 5.0f;
	[SerializeField]
	float jumpDist = 5.0f;
	[SerializeField]
	float airTime = 2.0f;
	[SerializeField]
	LayerMask platformLayers;

	Rigidbody2D rigidbody;
	BoxCollider2D collider;
	bool jumping = false;
	bool jumpBtnDown = false;

	private void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
	}

	private void Update() {
		if(isGrounded() && Input.GetKeyDown(KeyCode.Space) && jumping == false) {
			Debug.Log("jump button pressed");
			//rigidbody.AddForce(Vector2.up * jumpDist);
			jumping = true;
			jumpBtnDown = true;
			StartCoroutine(jump());
		}
		if(Input.GetKeyUp(KeyCode.Space)) {
			Debug.Log("jump button released");
			jumpBtnDown = false;
		}
		
	}

	private void FixedUpdate() {
		float move = Input.GetAxisRaw("Horizontal");
		rigidbody.velocity = new Vector2(speed * move, rigidbody.velocity.y);
	}

	bool isGrounded() {
		//RaycastHit2D hit;
		//RaycastHit2D ray = Physics2D.BoxCast(collider.bounds.center, transform.localScale, 0.0f, Vector2.down, platformLayers);
		RaycastHit2D ray = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, platformLayers);
		Debug.Log(ray.collider);
		return ray.collider != null;
	}

	IEnumerator jump() {
		Debug.Log("jump coroutine started");
		rigidbody.velocity = Vector2.zero;
		float timer = 0;

		while(jumpBtnDown == true && timer < airTime) {
			float percentJumpCompleted = timer / airTime;
			Vector2 jumpVectorThisFrame = Vector2.Lerp((Vector2.up + new Vector2(0, jumpDist)), Vector2.zero, percentJumpCompleted);
			//Debug.Log("jumpVectorThisFrame : " + jumpVectorThisFrame);
			rigidbody.AddForce(jumpVectorThisFrame);
			timer += Time.deltaTime;
			yield return null;
		}

		jumping = false;
		Debug.Log("jump coroutine ended");
	}
}
