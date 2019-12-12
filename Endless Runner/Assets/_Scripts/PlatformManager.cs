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
	List<ObjectPool> pools;

	[SerializeField]
	Transform minHeightPoint;
	[SerializeField]
	float maxDeltaHeight;
	[SerializeField]
	Transform maxHeightPoint;
	


	int platformTypeSelected;
	List<float> platformLengths;
	float minHeight;
	float maxHeight;
	float deltaHeight;
	float deltaHorizontalDist;


	private void Start() {

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
				deltaHeight = maxHeight;
			}else if(deltaHeight < minHeight) {
				deltaHeight = minHeight;
			}

			//set the position of the next platform
			transform.position = new Vector3(transform.position.x + (platformLengths[platformTypeSelected] / 2) + deltaHorizontalDist, deltaHeight, transform.position.z);

			GameObject newPlatform = pools[platformTypeSelected].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive(true);

			//reset the current transform position to the end of the platform
			transform.position = new Vector3(transform.position.x + (platformLengths[platformTypeSelected] / 2), transform.position.y, transform.position.z);
		}

	}
}
