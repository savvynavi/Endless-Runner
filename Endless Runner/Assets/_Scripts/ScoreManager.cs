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
		//if you can curently add to the score, adds the points/sec to the current score and then rounds it for display
		if(addToScore == true) {
			currentScore += pointsPerSecond * Time.deltaTime;
			score.text = "Score: " + Mathf.Round(currentScore);
		}
	}

	//takes in the points to add and adds them to the current score
	public void AddPoints(int addedPoints) {
		currentScore += addedPoints;
	}

	//sets if the score is counting
	public void isScoreCounting(bool counting) {
		addToScore = counting;
	}

	//resets the score to 0 and resets the text
	public void ResetScore() {
		currentScore = 0;
		score.text = "Score: " + Mathf.Round(currentScore);
	}
}
