using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]
	PlayerController player;
	[SerializeField]
	GameObject pauseMenu;
	[SerializeField]
	GameObject deathMenu;
	[SerializeField]
	string mainMenuSceneName = "MainMenu";
	[SerializeField]
	string gameScene = "SampleScene";
	[SerializeField]
	float deathThreashhold = -10.0f;
	[SerializeField]
	Text pauseText;

	Text finalScore;


	private void Start() {
		//if the menus are active in the scene when it starts, it turns them off
		if(deathMenu != null) {
			deathMenu.SetActive(false);
			foreach(Transform child in deathMenu.transform) {
				if(child.name == "FinalScore") {
					finalScore = child.GetComponent<Text>();
				}
			}
		}

		if(pauseMenu != null) {
			pauseMenu.SetActive(false);
		}
	}

	private void Update() {
		//kills player if they go below scree
		if(player != null && player.transform.position.y < deathThreashhold) {
			player.isDead = true;
			PlayerDeath();
		}
	}

	//called when the player dies, turns off player controls and brings up menu + final score
	public void PlayerDeath() {
		StartCoroutine(DeathState());
		if(ScoreManager.instance != null) {
			ScoreManager.instance.StopScore();
		}
	}

	IEnumerator DeathState() {
		//wait until player death anim has finished playing before popping menu up
		yield return new WaitForSeconds(3.0f);
		if(deathMenu != null) {
			deathMenu.SetActive(true);
			finalScore.text = "" + Mathf.Round(ScoreManager.instance.Score);
		}
	}

	//brings up the pause menu and stops gameplay if isPaused is true, otherwise resumes gameplay
	public void Pause(bool isPaused) {
		if(pauseMenu != null) {
			pauseMenu.SetActive(isPaused);

			if(isPaused == true) {
				pauseText.text = "resume";
			} else {
				pauseText.text = "pause";
			}

		}
	}

	//restarts the scene
	public void Restart() {
		SceneManager.LoadScene(gameScene);
	}

	//loads main menu
	public void LoadMainMenu() {
		SceneManager.LoadScene(mainMenuSceneName);
	}

	//quits the game
	public void Quit() {
		Application.Quit();
	}
}
