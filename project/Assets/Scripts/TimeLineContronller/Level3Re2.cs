using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Re2 : MonoBehaviour
{
    public string NextLevelName;
    public void ToNext()
    {
        SceneLoadManager.Instence.LoadSceneName = NextLevelName;
        UIManager.Instence.PushUI(new LoadNextLevel(), "Canvas");
    }
}
