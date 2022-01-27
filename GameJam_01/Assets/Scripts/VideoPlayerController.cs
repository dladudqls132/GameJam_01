using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private VideoPlayer vp;
    private bool isPause;

    public void ToggleVideo()
    {
        isPause = !isPause;

        if (isPause)
            vp.Pause();
        else
            vp.Play();
    }

    public void ChangeVolume()
    {
        vp.SetDirectAudioVolume(0, slider.value);
    }
}
