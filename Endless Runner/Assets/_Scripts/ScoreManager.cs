using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	[SerializeField]
	Text score;
	[SerializeField]
	float pointsPerSecond = 5.0f;

	public static ScoreManager instance;

	//public access to current score
	public float Score {
		get { return currentScore; }
		private set { }
	}

	float currentScore = 0;
	bool addToScore = true;

	private void Awake() {
		if(instance == null) {
			instance = this;
		}
	}

	private void Update() {
		if(addToScore == true) {
			currentScore += pointsPerSecond * Time.deltaTime;
			score.text = "Score: " + Mathf.Round(currentScore);
		}
	}

	public void AddPoints(int addedPoints) {
		currentScore += addedPoints;
	}

	public void isScoreCounting(bool counting) {
		addToScore = counting;
	}

	public void ResetScore() {
		currentScore = 0;
		score.text = "Score: " + Mathf.Round(currentScore);
	}
}
