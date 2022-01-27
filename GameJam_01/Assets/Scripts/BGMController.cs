using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] private string bgm;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameManager.Instance.soundManager.AudioPlayBGM(bgm, true));
    }
}
