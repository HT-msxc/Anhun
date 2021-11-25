using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AuthorListControl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] Animator end;
    bool isStart;

    void Update()
    {
        if (isStart)
        {
            scrollbar.value -= speed * Time.deltaTime;
            if(scrollbar.value <= 0)
            {
                isStart = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            end.Play("End");
            isStart = false;
        }
    }
    void StartPlay()
    {
        isStart = true;
    }
    void EndPlay()
    {
        SceneManager.LoadScene("MainScene");
        UIManager.Instence.PopUI();
    }
}
