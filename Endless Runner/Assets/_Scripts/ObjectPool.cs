using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {


	public GameObject prefab;
	[SerializeField]
	Transform parent;
	[SerializeField]
	int numInPool = 20;

	List<GameObject> pooledObjects;

	private void Awake() {
		pooledObjects = new List<GameObject>();

		//instantiating all the objects and setting them inactive
		for(int i = 0; i < numInPool; i++) {
			GameObject obj = Instantiate(prefab, parent);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject() {

		//loop over list, return first inactive obj in list to use
		for(int i = 0; i < pooledObjects.Count; i++) {
			if(!pooledObjects[i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}

		//if all objects are taken, instantiate a new one and return that
		GameObject obj = Instantiate(prefab, parent);
		obj.SetActive(false);
		pooledObjects.Add(obj);
		return obj;
	}
}
