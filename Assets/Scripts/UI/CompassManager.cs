using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassManager : MonoBehaviour {

    public float compassBearingStart, offset, compassBearingNow, compassPositionNow;
    private float rawBearing, rawDestinationBearing, rawDistance, defaultCompassBearingStart = 0;
    public GameObject bearingCompass;
    public GameObject destinationCompass;

    
	// Use this for initialization
    // Find the starting compass bearing
	void Start () {
        if (PlayerPrefs.GetInt("rosOn") == 1)
        { compassBearingStart = defaultCompassBearingStart + offset; }

        else
        {
            rawBearing = (float)GameObject.Find("Battery Voltage Level").GetComponent<ROSController>().outputBearing;
            compassBearingStart = rawBearing;
        }
    }
	
	// Update is called once per frame
	void Update () {
        {
            if (PlayerPrefs.GetInt("rosOn") == 1)
            {
                rawBearing = (float)GameObject.Find("Battery Voltage Level").GetComponent<ROSController>().outputBearing;
                rawDestinationBearing = GameObject.Find("Battery Voltage Level").GetComponent<ROSController>().outputDestinationBearing;
                rawDistance = GameObject.Find("Battery Voltage Level").GetComponent<ROSController>().outputDistance;
                compassBearingNow = compassBearingStart + rawBearing;
                bearingCompass.transform.eulerAngles = new Vector3(-90, 0, compassBearingNow);
                destinationCompass.transform.eulerAngles = new Vector3(-90, 0, compassBearingNow + offset);
            }
            else
            {
                //perform demo mode!
                compassBearingNow = compassBearingNow + 0.01F;
                compassPositionNow = defaultCompassBearingStart + compassBearingNow;
                bearingCompass.transform.eulerAngles = new Vector3(-90, 0, compassPositionNow);
                destinationCompass.transform.eulerAngles = new Vector3(-90, 0, compassPositionNow + offset);
            }
        }
}

    }
