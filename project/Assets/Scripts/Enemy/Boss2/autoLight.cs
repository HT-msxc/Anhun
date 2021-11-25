using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class autoLight : MonoBehaviour {
    private float Timer = 0;
    public AnimationCurve curve;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        GetComponent<Light2D>().intensity = curve.Evaluate(Timer);
	}
}
