using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {

	Text text;

	//changes the jump control instructions depending on build
	private void Start() {

		text = GetComponent<Text>();
#if UNITY_ANDROID
		text.text = "Tap to jump!";
#else
		text.text = "Space to jump!";
#endif

	}
}
