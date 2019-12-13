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
		if(player != null) {
			if(player.transform.position.y < deathThreashhold || player.hitObstacle == true) {
				player.isDead = true;
				PlayerDeath();
			}
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
		//wait until player death anim (if died to obstacle) has finished playing before popping menu up
		float waitTime = 1.0f;
		if(player.hitObstacle == true) {
			waitTime = player.Anim.GetCurrentAnimatorStateInfo(0).length + player.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f;
		}
		yield return new WaitForSeconds(waitTime);
		if(deathMenu != null) {
			deathMenu.SetActive(true);
			finalScore.text = "" + Mathf.Round(ScoreManager.instance.Score);
		}
	}

	//brings up the pause menu and stops gameplay if isPaused is true, otherwise resumes gameplay
	public void Pause(bool isPaused) {
		if(pauseMenu != null && deathMenu.activeInHierarchy == false) {
			pauseMenu.SetActive(isPaused);
			player.isPaused = isPaused;
			Physics2D.autoSimulation = !isPaused;

			if(isPaused == true) {
				pauseText.text = "resume";
			} else {
				pauseText.text = "pause";
			}

		}
	}

	//restarts the scene
	public void Restart() {
		turnPhysicsOn();
		SceneManager.LoadScene(gameScene);
	}

	//loads main menu
	public void LoadMainMenu() {
		turnPhysicsOn();
		SceneManager.LoadScene(mainMenuSceneName);
	}

	//quits the game
	public void Quit() {
		Application.Quit();
	}

	//checks if the physics is off from the pause menu and if so, turns it on
	void turnPhysicsOn() {
		if(Physics2D.autoSimulation == false) {
			Physics2D.autoSimulation = true;
		}
	}
}
