using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLogo : MonoBehaviour
{
    private void Start()
    {
        Invoke("ToMain",3f);
    }
    public void ToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
