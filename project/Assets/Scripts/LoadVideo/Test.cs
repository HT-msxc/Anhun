using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Test : MonoBehaviour
{
    public GameObject video;
    public VideoPlayer videoPlayer;
    private void Start()
    {
        video.SetActive(true);
    }
    private void Update() {
        videoPlayer.loopPointReached += LogOut;
    }
    public void LogOut(VideoPlayer videoPlayer)
    {
        video.SetActive(false);
    }
}
