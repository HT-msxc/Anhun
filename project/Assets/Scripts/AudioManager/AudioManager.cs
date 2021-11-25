using System.Dynamic;
using System.Threading.Tasks;
using System.Linq;
// 使用说明：设置滑动条的OnValueChangedEvent，
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public enum AudioType
{
    BackGroundMusic,
    SoundEffect
}
public class AudioManager : MonoBehaviour
{
    [Header("Mixer内的合适的最小音量")]
    public float minVolume;
    [Header("Mixer内的合适的最大音量")]
    public float maxVolume;

    [Header("主音量滑动条")]
    public Scrollbar mainBar;
    [Header("音乐滑动条")]
    public Scrollbar musicBar;

    [Header("音效滑动条")]
    public Scrollbar soundEffectBar;
    [Header("音量混合器")]
    public AudioMixer audioMixer;
    AudioMixerGroup[] audioMixerGroups;

    GameObject sourceListObject;

    AudioSource backGroundMusicPlayer;
    /// <summary>
    /// 依照字典搜索存放音频资源
    /// </summary>
    private Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();
    // private Dictionary<GameObject,List<AudioSource>> audioSourceDictionary;

    #region 单例
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject audioManager = GameObject.Find("AudioManager");
                if (audioManager == null)
                {
                    audioManager = new GameObject("AudioManager");
                }
                _instance = audioManager.GetComponent<AudioManager>();
                if (_instance == null)
                {
                    _instance = audioManager.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }
    #endregion
    private void Awake()
    {
        _instance = this;
        audioMixer = Resources.Load<AudioMixer>("AudioManager/MainMusicMaster");
        audioMixerGroups = audioMixer.FindMatchingGroups("Master");
        DontDestroyOnLoad(this.gameObject);
    }

    public void RefreshSourceList()
    {
        sourceListObject = GameObject.Find("AudioSources");
        if (clipDictionary != null)
            clipDictionary.Clear();
        if (sourceListObject != null)
        {
            AddToClipDictionary(sourceListObject.GetComponent<AudioSources>().audioClips);
        }
    }

    /// <summary>
    /// 向音源字典加入clips
    /// </summary>
    /// <param name="clips"></param>
    public void AddToClipDictionary(List<AudioClip> clips)
    {
        foreach(AudioClip clip in clips)
        {
            if (!clipDictionary.ContainsKey(clip.name))
            {
                clipDictionary.Add(clip.name, clip);
            }
            else throw new System.Exception($"The clip {clip.name} is already in the Dictionary");
        }
    }

    /// <summary>
    /// 向音源字典加入clips
    /// </summary>
    /// <param name="clip"></param>
    public void AddToClipDictionary(AudioClip clip)
    {
        if (!clipDictionary.ContainsKey(clip.name))
        {
            clipDictionary.Add(clip.name, clip);
        }
    }
    public void RemoveClip(List<AudioClip> clips)
    {
        foreach(AudioClip clip in clips)
        {
            if (clipDictionary.ContainsKey(clip.name))
            {
                clipDictionary.Remove(clip.name);
            }
        }
    }
    public void RemoveClip(AudioClip clip)
    {
        if (clipDictionary.ContainsKey(clip.name))
        {
            clipDictionary.Remove(clip.name);
        }
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="clipName">音乐名字</param>
    /// <param name="type">音乐类型</param>
    /// <param name="volume">音量，默认为1</param>
    /// <param name="spatialBlend">3D音量，默认为0</param>
    /// <param name="isLoop">是否循环，默认否</param>
    /// <param name="audioSourceOwner">播放组件拥有者，默认为null，放AudioManager上</param>
    public void PlayAudio(string clipName, AudioType type, GameObject audioSourceOwner = null, float spatialBlend = 0, bool isLoop = false)
    {
        AudioSource source = null;
        if (!clipDictionary.ContainsKey(clipName))
        {
            Debug.LogError($"Do not find the clip! Check the {clipName} Please!");
            return;
        }
        if (audioSourceOwner == null)
        {
            audioSourceOwner = gameObject;
        }
        AudioSource [] sourceList = audioSourceOwner.GetComponents<AudioSource>();
        if (sourceList != null)
        {
            foreach(AudioSource temp in sourceList)
            {
                if (!temp.isPlaying)
                {
                    source = temp;
                    break;
                }
            }
        }
        if (source == null)
        {
            source = audioSourceOwner.AddComponent<AudioSource>();
        }
        source.clip = clipDictionary[clipName];
        if (type == AudioType.BackGroundMusic)
        {
            backGroundMusicPlayer = source;
            source.outputAudioMixerGroup = audioMixerGroups[1];
        }
        else
        {
            source.outputAudioMixerGroup = audioMixerGroups[2];
        }
        source.spatialBlend = spatialBlend;
        source.loop = isLoop;
        source.Play();
    }

    public void StopAudio(string clipName, GameObject audioSourceOwner = null)
    {
        if (audioSourceOwner)
        {
            AudioSource [] sourceList = audioSourceOwner.GetComponents<AudioSource>();
            foreach(AudioSource source in sourceList)
            {
                if (source.isPlaying && source.clip.name.CompareTo(clipName) == 0)
                {
                    source.Stop();
                    return;
                }
            }
        }
    }
    public void SetMainMusicVolume(float value)
    {
        audioMixer.SetFloat("mainVolume", value * (maxVolume - minVolume) + minVolume);
    }

    public void SetBackGroundMusicVolume(float value)
    {
        audioMixer.SetFloat("musicVolume", value * (maxVolume - minVolume) + minVolume);
    }

    public void SetSoundEffectVolume(float value)
    {
        audioMixer.SetFloat("soundEffectVolume", value * (maxVolume - minVolume) + minVolume);
    }

    public void ClearAudioManager()
    {
        clipDictionary.Clear();
    }

    public void StopBackGroundMusic()
    {
        if (backGroundMusicPlayer != null)
            backGroundMusicPlayer.Stop();
    }

    public void PlayBackGroundMusic()
    {
        if(sourceListObject != null)
        PlayAudio(sourceListObject.GetComponent<AudioSources>().audioClips[0].name,AudioType.BackGroundMusic,null,0,true);
    }
}