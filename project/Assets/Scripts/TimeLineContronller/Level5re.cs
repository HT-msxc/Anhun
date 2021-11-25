using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5re : MonoBehaviour
{
    public GameObject plot;
    public void StartDialog2()
    {
        plot.GetComponent<Level5Plot2>().startTrigger = true;
    }
}
