using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AudioDictionary : ISerializationCallbackReceiver
{
    [System.Serializable]
    public struct Entry
    {
        public string name;
        public AudioClip clip;
    }

    // Inspector view
    [SerializeField]
    private List<Entry> _entries = new List<Entry>();

    // Code view
    private Dictionary<string, AudioClip> _dictionary = new Dictionary<string, AudioClip>();

    // Accessor
    public AudioClip this[string key]
    {
        get
        {
            if (_dictionary.TryGetValue(key, out AudioClip clip)) return clip;
            Debug.LogWarning($"Audio clip '{key}' not found.");
            return null;
        }
    }

    // Sync Logic
    public void OnAfterDeserialize()
    {
        _dictionary.Clear();
        foreach (var entry in _entries)
        {
            if (!string.IsNullOrEmpty(entry.name) && !_dictionary.ContainsKey(entry.name))
            {
                _dictionary.Add(entry.name, entry.clip);
            }
        }
    }

    public void OnBeforeSerialize() { }
}