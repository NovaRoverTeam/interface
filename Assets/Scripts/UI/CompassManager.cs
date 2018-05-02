using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassManager : MonoBehaviour {

    private bool rotating = true;
    public float compassBearingStart;
    public float offset;
    public float compassBearingNow;
    public float compassPositionNow;
    private float defaultCompassBearingStart;
    public GameObject bearingCompass;
    public GameObject destinationCompass;

    
	// Use this for initialization
	void Start () {
        compassBearingStart = defaultCompassBearingStart + offset;
    }
	
	// Update is called once per frame
	void Update () {
        {
            compassBearingNow = compassBearingNow + 0.01F;
            compassPositionNow = defaultCompassBearingStart + compassBearingNow;
            bearingCompass.transform.eulerAngles = new Vector3(-90, 0, compassPositionNow);
            destinationCompass.transform.eulerAngles = new Vector3(-90, 0, compassPositionNow + offset);

        }
}

    }
