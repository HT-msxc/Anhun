using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class QTE : MonoBehaviour
{
    [SerializeField]public float SlowDownTime = 0.5f;
    [SerializeField]float cameraSize = 17;
    float timeCount;
    [SerializeField]AnimationCurve curve;
    [SerializeField]CinemachineVirtualCamera m_camera;
    [SerializeField] Volume volume;
    bool isSlowDownTime;
    private void Start() {
        timeCount = SlowDownTime;
    }
    private void Update() {
        
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     StartSlowDownTime();
        // }
        OnSlowDownTime();
    }
    private void FixedUpdate() {
    }
    public void StartSlowDownTime()
    {
        timeCount = 0;
        isSlowDownTime = true;
    }
    public bool GetTimeState()
    {
        return isSlowDownTime;
    }
    void OnSlowDownTime()
    {
        if(timeCount > SlowDownTime)
        {
            isSlowDownTime =false;
            return;
        }
        timeCount += Time.deltaTime;
        Time.timeScale = curve.Evaluate(timeCount/SlowDownTime);
        m_camera.m_Lens.OrthographicSize = cameraSize + 3 * curve.Evaluate(timeCount/SlowDownTime);
        volume.profile.components[0].parameters[3].SetValue(new FloatParameter(1-curve.Evaluate(timeCount/SlowDownTime)));
    }
}
