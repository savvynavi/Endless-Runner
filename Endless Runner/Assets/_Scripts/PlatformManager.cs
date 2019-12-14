using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

	[SerializeField]
	Transform GenerationPoint;
	[SerializeField]
	float minHozirontalDist;
	[SerializeField]
	float maxHorizontalDist;
	[SerializeField]
	CollectableManager collectableManager;
	[SerializeField]
	ObstacleManager obstacleManager;

	[SerializeField]
	List<ObjectPool> pools;

	[SerializeField]
	Transform minHeightPoint;
	[SerializeField]
	float maxDeltaHeight;
	[SerializeField]
	Transform maxHeightPoint;

	Vector3 startPosition;

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


	int platformTypeSelected;
	List<float> platformLengths;
	float minHeight;
	float maxHeight;
	float deltaHeight;
	float deltaHorizontalDist;


	private void Start() {

		//get position of manager initially for when game resets
		startPosition = transform.position;

		//find the length of all the platform types in the object pools
		platformLengths = new List<float>();
		for(int i = 0; i < pools.Count; i++) {
			platformLengths.Add(pools[i].prefab.GetComponent<BoxCollider2D>().size.x);
		}

		//find the min and max height for platform placement
		minHeight = minHeightPoint.position.y;
		maxHeight = maxHeightPoint.position.y;
	}


	private void Update() {

		//activates platforms and moves them into position if the current position is behind the generationPoint
		if(transform.position.x < GenerationPoint.position.x) {

			//randomly pick the horizontal and vertical distance away for the next platform + the platform type
			deltaHorizontalDist = Random.Range(minHozirontalDist, maxHorizontalDist);
			deltaHeight = transform.position.y + Random.Range(maxDeltaHeight, -maxDeltaHeight);
			platformTypeSelected = Random.Range(0, pools.Count);

			//clamp the platform height between the min and max positions
			if(deltaHeight > maxHeight) {
				deltaHeight = maxHeight - pools[platformTypeSelected].prefab.GetComponent<BoxCollider2D>().size.y - 1.5f;
			}else if(deltaHeight < minHeight) {
				deltaHeight = minHeight + pools[platformTypeSelected].prefab.GetComponent<BoxCollider2D>().size.y;
			}

			//set the position of the next platform
			transform.position = new Vector3(transform.position.x + (platformLengths[platformTypeSelected] / 2) + deltaHorizontalDist, deltaHeight, transform.position.z);

			GameObject newPlatform = pools[platformTypeSelected].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive(true);

			//activate collectables and obstacles
			if(Random.Range(0, 100) <= obstacleManager.percentChance && platformLengths[platformTypeSelected] >= obstacleManager.MinPlatformLength) {
				Vector3 tmpPos = new Vector3(transform.position.x - (platformLengths[platformTypeSelected] / 2), transform.position.y + 0.1f, transform.position.z + 0.2f);
				obstacleManager.PlaceObstacle(tmpPos, platformLengths[platformTypeSelected]);
			} else if(Random.Range(0, 100) <= collectableManager.percentChance) {
				collectableManager.PlaceCollectable(new Vector3(transform.position.x - (platformLengths[platformTypeSelected] / 2), transform.position.y + 0.3f, transform.position.z), pools[platformTypeSelected].numItemsOnPlatform, platformLengths[platformTypeSelected]);
			}

			//reset the current transform position to the end of the platform
			transform.position = new Vector3(transform.position.x + (platformLengths[platformTypeSelected] / 2), transform.position.y, transform.position.z);
		}

	}

	public void ResetPosition() {
		transform.position = startPosition;
	}
}
