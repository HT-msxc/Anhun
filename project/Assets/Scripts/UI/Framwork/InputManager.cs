using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : Singleton<InputManager>
{
    public int EscCounter = 0;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if(!SceneLoadManager.Instence.IsLoading)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ++EscCounter;
                if(EscCounter == 1)
                {
                    UIManager.Instence.PushUI(new PauseGameUI(),"Canvas");
                    Time.timeScale = 0;
                }
            }
        }
    }
}
