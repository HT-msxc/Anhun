using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissovle : MonoBehaviour
{
    public float FlashSpeed =10f;
    float _dissovleCount = 1;
    float _speed= -15.0f;
    bool _startDissovle = false;
    bool _reload = false;
    bool _startFlash = false;
    public GameObject PlatformCollider2D;
    public Material FlashMaterial;
    Material _material = null;
    SpriteRenderer _spriteRenderer = null;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer == null)
        {
            throw new System.Exception("Cann't find the SpriteRender");
        }
        _material = _spriteRenderer.material;
    }

    private void Update()
    {
        if(_startDissovle)
        {
            _dissovleCount -= Mathf.Clamp01(Time.deltaTime);
            _material.SetFloat("_DissovleCount", _dissovleCount);
            if(_dissovleCount <= 0)
            {
                _startDissovle = false;
                Debug.Log(_startDissovle);
            }
        }
        if(_reload)
        {
            _dissovleCount += Mathf.Clamp01(Time.deltaTime);
            _material.SetFloat("_DissovleCount", _dissovleCount);
            Debug.Log(_dissovleCount);
            if(_dissovleCount >= 1)
            {
                Debug.Log("Reload complete!");
                 StartFlash();
                _reload = false;
            }
        }
        if(_startFlash)
        {
            _speed += FlashSpeed * Time.deltaTime;
            FlashMaterial.SetFloat("Speed", _speed);
            Debug.Log(_speed);
            if(_speed >= 20)
            {
                this.gameObject.GetComponent<Collider2D>().enabled = true;
                PlatformCollider2D.GetComponent<Collider2D>().enabled = true;
                _startFlash = false;
                _spriteRenderer.material = _material;
            }
        }
        else
        {
            _speed = -15f;
        }
    }
    public void StartDissolve()
    {
        _startDissovle = true;
    }
    public void CallBackDissolve()
    {
        Debug.Log("yes start!");
        _reload = true;
    }
    public void StartFlash()
    {
        _spriteRenderer.material = FlashMaterial;
        FlashMaterial.SetFloat("Speed", -15f);
        FlashMaterial.SetFloat("_Width", 2.5f);
        _startFlash = true;
    }
}
