using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelTreeContronller : MonoBehaviour
{
    [Header("場景中存在的物體")]
    public GameObject ScenePlayer;

    [Header("TimeLine")]
    public GameObject timeline;

    [Header("出生點")]
    public PawnPoint point;
    public GameObject Dialog1;
    public GameObject Dialog2;
    public string NextLevelName;
    int count = 0;

    private void Update()
    {
        if(point.ok)
        {
            ScenePlayer.SetActive(true);
            point.player.SetActive(false);
            Dialog1.GetComponent<Level1Plot3Contronller>().startTrigger = true;
            if(Dialog1.GetComponent<Level1Plot3Contronller>().DialogOver)
            {
                timeline.SetActive(true);
            }
            if(Dialog2.GetComponent<Level1Plot4Contronller>().DialogOver && count ==0)
            {
                count++;
                point.player.SetActive(true);
                point.player.transform.position = ScenePlayer.transform.position;
                ScenePlayer.SetActive(false);
                SceneLoadManager.Instence.LoadSceneName = NextLevelName;
                UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
            }
        }
    }

    public void EndFirstTimeLine()
    {
        Debug.Log("Event");
        Debug.Log(Dialog2.GetComponent<Level1Plot4Contronller>().startTrigger);
        Dialog2.GetComponent<Level1Plot4Contronller>().startTrigger = true;
    }
}
