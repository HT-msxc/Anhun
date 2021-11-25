using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public bool IsLoading = false;
    public string LoadSceneName{get; set;}
    public bool IsLoaded = false;
    public bool StartLoadNext = false;
    public GameObject LoadUI;
    //public Slider slider;
    public AsyncOperation asyncLoad;
    AnimatorStateInfo info;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void StartLoad()
    {

        StartCoroutine(LoadSceneAsync());
        IsLoading = true;
    }
    IEnumerator LoadSceneAsync()
    {
        yield return null;
        asyncLoad = SceneManager.LoadSceneAsync(LoadSceneName);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            //slider.value = asyncLoad.progress;
            if(asyncLoad.progress >= 0.9f)
            {
                LoadUI = GameObject.Find("LoadNextLevel");
                info = LoadUI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                Debug.Log(info.normalizedTime);
                if(info.normalizedTime >= 1.0f)
                {
                    //这里已经加载完毕， 那么上个场景的所有UI 预制体字典也得清空
                    UIManager.Instence.UIStack.Clear();
                    UIManager.Instence.UIInstance.Clear();
                    asyncLoad.allowSceneActivation = true;
                    AudioManager.Instance.StopBackGroundMusic();
                    if (GameManager.Instence.CurrentPlayer != null)
                    {   
                        GameManager.Instence.CurrentPlayer.GetComponent<GhostShadow>().RefreshGhostShadows();
                        GameManager.Instence.CurrentPlayer.GetComponent<PlayerInteractionWithSoulPiece>().RefreshSoulCages();
                    }
                }
            }
            yield return null;
        }
    }
}
