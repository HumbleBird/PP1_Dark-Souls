using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializebleDictionary <TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    // serialization �ϱ� �ٷ� ������ call
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

    // serialization �ϰ� �ٷ� �Ŀ� call
    // load the dictionary from lists
    public void OnAfterDeserialize()
    {
        Clear();

        if(keys.Count != values.Count)
        {
            Debug.LogError("dic�� deserialize�Ϸ��� ������, ������ ������ key ���� value ���� Ʋ��");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            Add(keys[i], values[i]);
        }
    }
}
