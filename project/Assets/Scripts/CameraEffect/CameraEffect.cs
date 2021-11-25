using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffect : Singleton<CameraEffect>
{
    public CinemachineBrain cinemachineBrain; 
    #region 相机抖动
    bool isStartShake;
    public float shakeTime = 0.1f;
    public Vector2 shakeRange = new Vector2(0.1f, 0.1f);
    public float shakeTimeCounter;
    #endregion
    protected override void Awake() 
    {
        base.Awake();
        cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    private void FixedUpdate() 
    {
        CameraShake();
    }

    public void SetCameraShakeEffect()
    {
        cinemachineBrain.enabled = false;
        isStartShake = true;
    }
    public void CameraShake()
    {
        if (isStartShake && shakeTimeCounter < shakeTime)
        {
            float x = UnityEngine.Random.Range(-shakeRange.x, shakeRange.x);
            float y = UnityEngine.Random.Range(-shakeRange.y, shakeRange.y);
            transform.position += new Vector3(x, y, 0);
            shakeTimeCounter += Time.fixedDeltaTime;
            if (shakeTimeCounter > shakeTime)
            {
                isStartShake = false;
                shakeTimeCounter = 0;
                cinemachineBrain.enabled = true;
            }
        }
    }
}
