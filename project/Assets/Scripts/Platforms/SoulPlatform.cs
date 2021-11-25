using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulPlatform : MonoBehaviour
{
    [Header("勾选采用 溶解特效")]
    public bool ChooseDissovle;

    [Header("崩坏时间")]
    public float CrashTime = 3f;
    //float _alphSpeed = 0f;
    float _alph = 0;
    bool _startReload = false;
    int _inCount = 0;
    public GameObject _collider;
    Dissovle dissovle = null;
    SplitSprite _splitSprite;
    SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        //gameObject.tag = "SoulPlatform";
    }
    private void Start()
    {
        if(_collider == null)
        {
            throw new System.Exception("Cann't Find SoulPlatform Collider!");
        }
        _splitSprite = GetComponent<SplitSprite>();
        if(_splitSprite == null)
        {
            Debug.Log("Cann't find Split!");
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(ChooseDissovle)
        {
            dissovle = GetComponent<Dissovle>();
        }
    }
    void Update()
    {

        if(_startReload)
        {
            if(_alph <= 1)
            {
                _alph += 0.001f;
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, 0, 0, _alph);
            }
            else
            {
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, 255, 255, _alph);
                _collider.GetComponent<Collider2D>().enabled = true;
                this.gameObject.GetComponent<Collider2D>().enabled = true;
                _startReload = false;
                _inCount = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(ChooseDissovle)
        {
            if(other.tag == "Player")
            {
                ++_inCount;
                if(_inCount == 1)
                {
                    Debug.Log("Dissovle Player in");
                    Invoke("StartDissolve",CrashTime);
                }
            }
        }
        else
        {
            if(other.tag == "Player")
            {
                ++_inCount;
                Debug.Log("Player in!");
                if(_inCount == 1)
                    Invoke("SplitDelay",CrashTime);
            }
        }
    }
    void SplitDelay()
    {
        _splitSprite.Split();
        _collider.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        _alph = 0;
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _alph);
        Invoke("ReloadDelay",CrashTime);
    }
    void ReloadDelay()
    {
        _startReload = true;
    }

    void StartDissolve()
    {
        _collider.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        dissovle.StartDissolve();
        Debug.Log("start dissove");
        AudioManager.Instance.PlayAudio("灵魂平台碎裂",AudioType.SoundEffect,gameObject);
        Invoke("StartReload", CrashTime);
    }

    void StartReload()
    {
        dissovle.CallBackDissolve();
        _inCount = 0;
        Debug.Log("Start call back");
    }
}
