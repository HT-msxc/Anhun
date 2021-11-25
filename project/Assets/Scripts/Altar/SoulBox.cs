using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class SoulBox : MonoBehaviour , IGetHurt
{
    [SerializeField]NatureState m_nuterState;
    Animator m_animator;
    private void Start() {
        m_animator = GetComponent<Animator>();
    }
    
    public void CloseBox()
    {
        //TODO 。。。。。。 播放音效\

        m_animator.Play("FadeAway");
        Invoke(nameof(SetActiveFalse) ,1);
    }

    void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(m_nuterState != NatureState.Gold && m_nuterState == other.gameObject.GetComponent<Player>().GetNatureState())
        {
            GetHurt(other.transform);
        }
    }

    public void GetHurt(Transform attacker)
    {
        var boxes = transform.parent.GetComponentsInChildren<SoulBox>();
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].CloseBox();
        }
    }
}
