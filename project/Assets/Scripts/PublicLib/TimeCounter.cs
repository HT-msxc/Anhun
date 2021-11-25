using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    float currentTime;
    float limitTime;
    bool isArrived;
    private void Awake()
    {
        this.enabled = false;
        currentTime = 0;
    }
    void Update()
    {
        currentTime = Mathf.Clamp(currentTime + Time.deltaTime,0 ,limitTime);
        isArrived = currentTime >= limitTime;
    }
    public float GetRate()
    {
        if(limitTime != 0)
            return currentTime / limitTime;
        return -1;
    }
    public float CurrentTime
    {
        get=>currentTime;
        set=>currentTime = value;
    }
    
    public float LimitTime
    {
        get=>limitTime;
        set=>limitTime = value;
    }

    public bool IsArrived
    {
        get=>isArrived;
    }
}