using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioSources :  MonoBehaviour{
    public List<AudioClip> audioClips;
    private void Awake() 
    {
        AudioManager.Instance.RefreshSourceList();
    }
}
