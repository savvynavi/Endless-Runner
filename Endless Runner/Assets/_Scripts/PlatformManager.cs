using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

	[SerializeField]
	float platformGenerationBuffer;
	[SerializeField]
	float minHozirontalDist;
	[SerializeField]
	float maxHorizontalDist;
	[SerializeField]
	float maxDeltaHeight;

	[SerializeField]
	CollectableManager collectableManager;
	[SerializeField]
	ObstacleManager obstacleManager;
	[SerializeField]
	List<ObjectPool> pools;

	Vector3 startPosition;
	float maxHeightPos;
	float minHeightPos;
	Camera cam;
	float aspect;
	float camHalfHeight;
	float camHalfWidth;

	int platformTypeSelected;
	List<float> platformLengths;
	float minHeight;
	float maxHeight;
	float deltaHeight;
	float deltaHorizontalDist;

	public BoxCollider2D playerCollider;

	public List<ObjectPool> PlatformObjectPools{
		get { return pools; }
		private set { }
	}

	public CollectableManager CollectableManager {
		get { return collectableManager; }
		private set { }
	}
	public ObstacleManager ObstacleManager {
		get { return obstacleManager; }
		private set { }
	}

	private void Start() {
		
		cam = Camera.main;

		//get position of manager initially for when game resets
		startPosition = transform.position;

		//find the length of all the platform types in the object pools
		platformLengths = new List<float>();
		for(int i = 0; i < pools.Count; i++) {
			platformLengths.Add(pools[i].Prefab.GetComponent<BoxCollider2D>().size.x);
		}

		//find the min and max height for platform placement
		aspect = (float)Screen.width / (float)Screen.height;
		camHalfHeight = cam.orthographicSize;
		minHeight = cam.transform.position.y - camHalfHeight;
		maxHeight = cam.transform.position.y + camHalfHeight;
	}


	private void Update() {
		//gets the camera width for generation
		camHalfWidth = aspect * camHalfHeight;
		float generationPoint = cam.transform.position.x + camHalfWidth + platformGenerationBuffer;

		//activates platforms and moves them into position if the current position is behind the generationPoint
		if(transform.position.x < generationPoint) {

			//randomly pick the horizontal and vertical distance away for the next platform + the platform type
			deltaHorizontalDist = Random.Range(minHozirontalDist, maxHorizontalDist);
			deltaHeight = transform.position.y + Random.Range(maxDeltaHeight, -maxDeltaHeight);
			platformTypeSelected = Random.Range(0, pools.Count);

			//clamp the platform height between the min and max positions
			if(deltaHeight > (maxHeight - pools[platformTypeSelected].Prefab.GetComponent<BoxCollider2D>().size.y - playerCollider.size.y)) {
				deltaHeight = maxHeight - pools[platformTypeSelected].Prefab.GetComponent<BoxCollider2D>().size.y - playerCollider.size.y;
			}else if(deltaHeight < minHeight + pools[platformTypeSelected].Prefab.GetComponent<BoxCollider2D>().size.y + 0.01f) {
				deltaHeight = minHeight + pools[platformTypeSelected].Prefab.GetComponent<BoxCollider2D>().size.y + 0.01f;
			}

			//set the position of the next platform
			transform.position = new Vector3(transform.position.x + (platformLengths[platformTypeSelected] / 2) + deltaHorizontalDist, deltaHeight, transform.position.z);

			GameObject newPlatform = pools[platformTypeSelected].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive(true);

			//activate collectables and obstacles
			if(Random.Range(0, 100) <= obstacleManager.PercentChance && platformLengths[platformTypeSelected] >= obstacleManager.MinPlatformLength) {
				Vector3 tmpPos = new Vector3(transform.position.x - (platformLengths[platformTypeSelected] / 2), transform.position.y + 0.1f, transform.position.z + 0.2f);
				obstacleManager.PlaceObstacle(tmpPos, platformLengths[platformTypeSelected], playerCollider.size.x);
			} else if(Random.Range(0, 100) <= collectableManager.PercentChance) {
				collectableManager.PlaceCollectable(new Vector3(transform.position.x - (platformLengths[platformTypeSelected] / 2), transform.position.y + 0.3f, transform.position.z), pools[platformTypeSelected].NumItemsOnPlatform, platformLengths[platformTypeSelected]);
			}

			//reset the current transform position to the end of the platform
			transform.position = new Vector3(transform.position.x + (platformLengths[platformTypeSelected] / 2), transform.position.y, transform.position.z);
		}

	}

	public void ResetPosition() {
		transform.position = startPosition;
	}
}
