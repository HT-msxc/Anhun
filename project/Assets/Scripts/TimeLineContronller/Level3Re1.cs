using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Re1 : MonoBehaviour
{
    public GameObject Plot2;
    public void StartDialog2()
    {
        Plot2.GetComponent<Level3Plot2>().startTrigger = true;
    }
}
