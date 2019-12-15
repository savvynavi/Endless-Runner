using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour {

	[SerializeField]
	ObjectPool collectablePool;
	[SerializeField]
	float collectableDist;
	[SerializeField]
	public int percentChance;

	public int PercentChance {
		get { return percentChance; }
		private set { }
	}

	public ObjectPool Pool {
		get { return collectablePool; }
		private set { }
	}

	public void PlaceCollectable(Vector3 startPos, int numOfCollectables, float platformLength) {

		float spacing = collectableDist;

		//loops over number of collectables being activated and positions them evenly along the top of the platform
		for(int i = 0; i < numOfCollectables; i++) {

			GameObject tmpCollectable = collectablePool.GetPooledObject();
			tmpCollectable.transform.position = new Vector3(startPos.x + spacing, startPos.y, startPos.z);
			tmpCollectable.SetActive(true);
			spacing += platformLength / numOfCollectables;
		}

	}
}
