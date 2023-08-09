using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializebleDictionary <TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    // serialization 하기 바로 직전에 call
    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (KeyValuePair <TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // serialization 하고 바로 후에 call
    // load the dictionary from lists
    public void OnAfterDeserialize()
    {
        Clear();

        if(keys.Count != values.Count)
        {
            Debug.LogError("dic을 deserialize하려고 했으나, 무언가의 에러로 key 값과 value 값이 틀림");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            Add(keys[i], values[i]);
        }
    }
}
