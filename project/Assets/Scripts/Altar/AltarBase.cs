using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魂台基类
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class AltarBase : MonoBehaviour
{
    [SerializeField]private float CD = 5;
    [SerializeField]private bool isCD;
    public bool IsCD{get => isCD;}
    private float cdTime;
    private Collider2D m_collider;
    protected Animator m_animator;
    protected NatureState thisNatureState;
    public NatureState ThisNatureState{get => thisNatureState;}
    protected virtual void Awake() {
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider2D>();
    }
    protected virtual void Update()
    {
        isCD = m_animator.GetBool("IsCD");
        if(isCD)
        {
            cdTime += Time.deltaTime;
            if(cdTime >= CD)
            {
                OpenAltar();
                cdTime = 0;
            }
        }
    }

    protected virtual void OpenAltar()
    {
        m_animator.SetBool("IsCD",false);
        m_collider.enabled = true;
    }

    public virtual void CloseAltar()
    {
        m_animator.SetBool("IsCD",true);
        m_collider.enabled = false;
    }

    // protected virtual void OnTriggerEnter2D(Collider2D other) {
    //     if(other.tag == "Player")
    //     {
    //         Invoke(nameof(CloseAltar) ,Time.deltaTime*2);
    //     }
    // }
}
