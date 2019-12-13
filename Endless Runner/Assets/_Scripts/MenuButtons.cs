using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
	[SerializeField]
	Scene scene;
	[SerializeField]
	GameObject HighScoreMenu;

	string button;

	//starts the game
	public void StartGame() {
		SceneManager.LoadScene("SampleScene");
	}

	//brings up highscore menu
	public void ViewHighScores() {

	}

	//quits the game
	public void onQuit() {
		Application.Quit();
	}
}
