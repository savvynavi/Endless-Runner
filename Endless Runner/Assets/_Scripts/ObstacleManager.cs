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
	[SerializeField]
	int percentChance;

	public int PercentChance {
		get { return percentChance; }
		private set { }
	}


	public ObjectPool Pool {
		get { return objectPool; }
		private set { }
	}

	public float MinPlatformLength {
		get { return minPlatformLength; }
		private set { }
	}

	//places an obstacle on a given platform as long as it's longer than a given length
	public void PlaceObstacle(Vector3 position, float platformLength, float playerWidth) {

		GameObject tmpObstacle = objectPool.GetPooledObject();

		//rinds random position between the edge of the platform plus the players width and the edge of the platform and sets this as its position
		float randPosition = Random.Range(position.x + edgeBuffer + playerWidth, position.x + platformLength - edgeBuffer);
		tmpObstacle.transform.position = new Vector3(randPosition, position.y, position.z);
		tmpObstacle.SetActive(true);
	}
}
