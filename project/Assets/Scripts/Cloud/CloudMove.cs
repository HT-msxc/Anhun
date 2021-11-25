using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float MoveSpeed = 0.05f;
    public float Timer1 = 2.0f;
    public float Timer2 = 4.0f;

    public float time = 0f;

    private void Update()
    {
        time +=Time.deltaTime;
        if(time <= Timer1)
        {
            MoveHorizontalRight();
        }
        else if(time <=Timer2)
        {
            MoveHorizontalRight();
            MoveVerticalUp();
        }
        else
        {
            time = 0;
        }

    }

    void MoveHorizontalRight()
    {
        this.transform.position = new Vector2(this.transform.position.x + MoveSpeed,this.transform.position.y);

    }
    void MoveHorizontalLeft()
    {
        this.transform.position = new Vector2(this.transform.position.x + MoveSpeed,this.transform.position.y);
    }
    void MoveVerticalUp()
    {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + MoveSpeed);
    }
}
