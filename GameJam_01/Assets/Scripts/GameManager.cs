using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            return instance;
        }
        set { instance = value; }
    }

    public SoundManager soundManager = null;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();

        if(soundManager != null)
            soundManager.Init();
    }
}
