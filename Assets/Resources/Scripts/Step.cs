using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public sealed class Step : MonoBehaviour
{
    public VideoClip SkyboxClip;
    public AudioSource AudioSource;
    private bool _firstPlay = true;

    private void SwitchTrigger(bool active)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<StepTrigger>() != null)
                child.gameObject.SetActive(active);
        }
            
    }

    private void CheckForEndOfContent(VideoPlayer vp)
    {
        if (_firstPlay)
        {
            SwitchTrigger(true);
            _firstPlay = false;
        }

        vp.frame = (int)vp.frameCount - 1;
        vp.loopPointReached -= CheckForEndOfContent;
    }

    public void Activate(Player player)
    {
        player.VideoPlayer.clip = SkyboxClip;
        player.VideoPlayer.Stop();
        player.VideoPlayer.Prepare();
        player.VideoPlayer.loopPointReached += CheckForEndOfContent;
        if (_firstPlay)
        {
            player.VideoPlayer.prepareCompleted += StartPlayback;
            SwitchTrigger(false);
        }
        else
        {
            player.VideoPlayer.prepareCompleted += StopAtEnd;
        }
    }

    private void StartPlayback(VideoPlayer vp)
    {
        AudioSource.Play();
        vp.frame = 0;
        vp.Play();
        vp.prepareCompleted -= StartPlayback;
    }

    private void StopAtEnd(VideoPlayer vp)
    {
        vp.playbackSpeed = 10000;
        vp.isLooping = false;
        vp.Play();
        SwitchTrigger(true);
        vp.prepareCompleted -= StopAtEnd;
    }

    public void Deactivate(Player player)
    {
        AudioSource.Stop();
        player.VideoPlayer.playbackSpeed = 1;
        player.VideoPlayer.clip = null;
        SwitchTrigger(false);
        gameObject.SetActive(false);
    }
}
