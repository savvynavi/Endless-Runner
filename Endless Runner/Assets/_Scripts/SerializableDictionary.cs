using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary <TKey, TValue>:  Dictionary<TKey, TValue>, ISerializationCallbackReceiver {

	[SerializeField]
	List<TKey> keys = new List<TKey>();

	[SerializeField]
	List<TValue> values = new List<TValue>();

	[SerializeField]
	int size;

	//save dictionary to a lists
	public void OnBeforeSerialize() {
		keys.Clear();
		values.Clear();
		foreach(KeyValuePair<TKey, TValue> pair in this) {
			keys.Add(pair.Key);
			values.Add(pair.Value);
		}
	}


	//load dictionary from lists
	public void OnAfterDeserialize() {
		this.Clear();

		if(keys.Count != values.Count) {
			throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialisation. Make sure that both key and value types are serialized"));
		}

		for(int i = 0; i < keys.Count; i++) {
			this.Add(keys[i], values[i]);
		}
	}
	
}
