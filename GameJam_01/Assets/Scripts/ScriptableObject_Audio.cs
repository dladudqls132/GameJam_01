using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Data", menuName = "Scriptable Object/Audio Data")]

[System.Serializable]
public class AudioInfo
{
    public AudioClip clip;
    public string name;

    [Range(0, 1)]
    public float volume = 1.0f;

    [Range(0, 3)]
    public float pitch_min = 1.0f;
    [Range(0, 3)]
    public float pitch_max = 1.0f;
}

public class ScriptableObject_Audio : ScriptableObject
{
    public List<AudioInfo> audioInfos_BGM = new List<AudioInfo>();
    public List<AudioInfo> audioInfos_SFX = new List<AudioInfo>();
    public AudioInfo GetAudioInfo(string name) 
    {
        if (audioInfos_BGM.Exists(n => n.name.Equals(name)))
            return audioInfos_BGM.Find(a => a.name.Equals(name));
        else if (audioInfos_SFX.Exists(n => n.name.Equals(name)))
            return audioInfos_BGM.Find(a => a.name.Equals(name));
        else
            return null;
    }
}
