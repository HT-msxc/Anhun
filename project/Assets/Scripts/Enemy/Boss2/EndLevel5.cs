using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel5 : MonoBehaviour
{
    public string NextLevelName = "Level06";
    void ToNextLevel()
    {
        SceneLoadManager.Instence.LoadSceneName = NextLevelName;
        UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
    }
}
