using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldElement : ElementBase
{
    public Transform effectPoint;
    public GameObject hitEffect;
    void Awake()
    {
        animator = GetComponent<Animator>();
        elementState = ElementState.FreeInWorld;
        natureState = NatureState.Gold;
        effectPoint = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        IGetHurt canHurtObject = other.GetComponent<IGetHurt>();
        if (canHurtObject != null && !other.CompareTag("Player"))
        {
            canHurtObject.GetHurt(transform);
            GameObject temp = ObjectPoolManager.Instence.CreateObject(hitEffect, effectPoint.position, transform.rotation);
            CameraEffect.Instence.SetCameraShakeEffect();
        }
    }

    public override void HandleSelection()
    {
        animator.Play("Used");
    }

    public override void ReturnToPool()
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        Destroy(gameObject);
    }
}