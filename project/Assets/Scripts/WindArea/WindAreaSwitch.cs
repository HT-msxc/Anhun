using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAreaSwitch : MonoBehaviour
{
    public GameObject m_wind;
    AudioSource m_AudioSource;
    bool isTirrger;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!m_wind.activeInHierarchy && other.tag == "Player")
        {
            //AudioManager.Instance.PlayAudioClip(AudioClips.player_SwitchOff, this.gameObject);
            GetComponent<Animator>().Play("WindSwitchOpen");
            m_wind.SetActive(true);
        }
    }
}
