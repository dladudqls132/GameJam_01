using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioSourceType
{
    BGM,
    SFX
}


public class SoundManager : MonoBehaviour
{
    [SerializeField] private ScriptableObject_Audio scriptableObject_Audio;

    [SerializeField] private Transform audioSourceObject_3D_parent;
    [SerializeField] private Transform audioSourceObject_2D_parent;
    [SerializeField] private Transform audioSourceObject_BGM_parent;

    [SerializeField] private int audioSourceNum;
    [SerializeField] private GameObject audioSourceObject_3D;
    [SerializeField] private GameObject audioSourceObject_2D;
    [SerializeField] private GameObject audioSourceObject_BGM;

    
    private List<KeyValuePair<AudioSourceType, AudioSource>> audioSources = new List<KeyValuePair<AudioSourceType, AudioSource>>();

    private AudioSource[] allAudioSources;
    

    // Start is called before the first frame update
    public void Init()
    {
        StartCoroutine(CreateAudioSources());
    }

    private void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    IEnumerator AddAudioSources()
    {
        for (int i = 0; i < audioSourceObject_BGM_parent.childCount; i++)
        {
            audioSources.Add(new KeyValuePair<AudioSourceType, AudioSource>(AudioSourceType.BGM, audioSourceObject_BGM_parent.GetChild(i).GetComponent<AudioSource>()));
        }

        for (int i = 0; i < audioSourceObject_2D_parent.childCount; i++)
        {
            audioSources.Add(new KeyValuePair<AudioSourceType, AudioSource>(AudioSourceType.SFX, audioSourceObject_2D_parent.GetChild(i).GetComponent<AudioSource>()));
        }

        for (int i = 0; i < audioSourceObject_3D_parent.childCount; i++)
        {
            audioSources.Add(new KeyValuePair<AudioSourceType, AudioSource>(AudioSourceType.SFX, audioSourceObject_3D_parent.GetChild(i).GetComponent<AudioSource>()));
        }

        yield return null;
    }

    IEnumerator CreateAudioSources()
    {
        Instantiate(audioSourceObject_BGM, audioSourceObject_BGM_parent);

        for (int i = 0; i < audioSourceNum; i++)
        {
            Instantiate(audioSourceObject_2D, audioSourceObject_2D_parent);
            Instantiate(audioSourceObject_2D, audioSourceObject_3D_parent);
        }

        yield return StartCoroutine(AddAudioSources());
    }

    public void SetPauseAll(bool value)
    {
        if (value)
        {
            foreach (AudioSource audios in allAudioSources)
            {
                audios.Pause();
            }
        }
        else
        {
            foreach (AudioSource audios in allAudioSources)
            {
                audios.UnPause();
            }
        }
    }

    public IEnumerator AudioPlayOneShotSFX(string name, Vector3 position)
    {
        AudioInfo audioInfo = scriptableObject_Audio.GetAudioInfo(name);

        if (audioInfo != null)
        {
            foreach (var a in audioSources)
            {
                if (a.Key == AudioSourceType.SFX)
                {
                    if (!a.Value.isPlaying)
                    {
                        a.Value.transform.position = position;
                        a.Value.volume = audioInfo.volume + Random.Range(-0.01f, 0.01f);
                        a.Value.pitch = Random.Range(audioInfo.pitch_min, audioInfo.pitch_max);
                        a.Value.PlayOneShot(audioInfo.clip);
                        break;
                    }
                }
            }
        }

        yield return null;
    }

    public IEnumerator AudioPlayBGM(string name, bool loop)
    {
        AudioInfo audioInfo = scriptableObject_Audio.GetAudioInfo(name);

        if (audioInfo != null)
        {
            foreach (var a in audioSources)
            {
                if (a.Key == AudioSourceType.BGM)
                {
                    if (!a.Value.isPlaying)
                    {
                        a.Value.clip = audioInfo.clip;
                        a.Value.volume = audioInfo.volume + Random.Range(-0.01f, 0.01f);
                        a.Value.pitch = Random.Range(audioInfo.pitch_min, audioInfo.pitch_max);
                        a.Value.loop = loop;
                        a.Value.Play();
                        break;
                    }
                    else
                    {
                        yield return StartCoroutine(AudioVolumeLerpToVal(a.Value, 0.1f, 2.0f));

                        a.Value.clip = audioInfo.clip;
                        a.Value.volume = audioInfo.volume + Random.Range(-0.01f, 0.01f);
                        a.Value.pitch = Random.Range(audioInfo.pitch_min, audioInfo.pitch_max);
                        a.Value.loop = loop;
                        a.Value.Play();
                        break;
                    }
                }
            }
        }
    }

    IEnumerator AudioVolumeLerpToVal(AudioSource source, float destVal, float lerpSpeed)
    {
        while (source.volume > destVal)
        {
            source.volume -= lerpSpeed * Time.deltaTime;

            if (source.volume <= destVal)
            {
                source.Stop();
            }

            yield return null;
        }
    }
}
