using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

	[SerializeField]
	ObjectPool objectPool;
	[SerializeField]
	float minPlatformLength;
	[SerializeField]
	float edgeBuffer = 0.2f;

	public int percentChance;

	public float MinPlatformLength {
		get { return minPlatformLength; }
		private set { }
	}

	//places an obstacle on a given platform as long as it's longer than a given length
	public void PlaceObstacle(Vector3 position, float platformLength) {

		GameObject tmpObstacle = objectPool.GetPooledObject();
		float randPosition = Random.Range(position.x + edgeBuffer, position.x + platformLength - edgeBuffer);

		tmpObstacle.transform.position = new Vector3(randPosition, position.y, position.z);
			
		tmpObstacle.SetActive(true);

	}
}
