using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderIn : MonoBehaviour
{
    [SerializeField] private float CD = 5;
    private float cdTime;
    [SerializeField]private float warnTime = 2;
    protected virtual void Update()
    {
        cdTime += Time.deltaTime;
        if (cdTime >= CD - warnTime)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (cdTime >= CD)
        {
            Flash();
            cdTime = 0;
        }
    }

    void Flash()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
