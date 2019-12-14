using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]
	PlayerController player;
	[SerializeField]
	GameObject startPlatforms;
	[SerializeField]
	PlatformManager platformManager;
	[SerializeField]
	Toggle pauseToggle;
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
	float deathWaitTime = 0.5f;
	[SerializeField]
	Text pauseText;

	Text finalScore;
	Vector3 playerStartPos;


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

		//get players starting position
		if(player != null) {
			playerStartPos = player.transform.position;
		}
	}

	private void Update() {
		//kills player if they go below scree
		if(player != null) {
			if(player.transform.position.y < deathThreashhold || player.hitObstacle == true) {
				player.isDead = true;
				PlayerDeath();
			} else {
				player.isDead = false;
				deathMenu.SetActive(false);
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
		float waitTime = deathWaitTime;
		if(player.hitObstacle == true) {
			waitTime = player.Anim.GetCurrentAnimatorStateInfo(0).length + player.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime + deathWaitTime;
		}
		yield return new WaitForSeconds(waitTime);
		if(deathMenu != null) {
			deathMenu.SetActive(player.isDead);
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

		//turn death screen off
		player.isDead = false;
		player.isPaused = false;
		if(pauseToggle != null) {
			pauseToggle.onValueChanged.Invoke(false);
			pauseToggle.isOn = false;
		}
		//Pause(player.isPaused);
		player.hitObstacle = false;

		//resets player position
		player.transform.position = playerStartPos;
		player.Anim.SetBool("isDead", false);
		//player.transform.rotation = playerStartPos.rotation;

		//resets platform generation so game can start over
		foreach(Transform obj in startPlatforms.transform) {
			if(obj.gameObject.activeInHierarchy == false) {
				obj.gameObject.SetActive(true);
			}
		}

		if(platformManager != null) {
			for(int i = 0; i < platformManager.PlatformObjectPools.Count; i++) {
				platformManager.PlatformObjectPools[i].ResetAll();
			}

			platformManager.CollectableManager.Pool.ResetAll();
			platformManager.ObstacleManager.Pool.ResetAll();

			platformManager.ResetPosition();
		}

	}

	//loads the game
	public void LoadGame() {
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
