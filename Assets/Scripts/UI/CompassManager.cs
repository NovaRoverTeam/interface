using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassManager : MonoBehaviour {

    public float compassBearingStart, offset, compassBearingNow, compassPositionNow, compassBearingReverse;
    private float rawBearing, rawDestinationBearing, rawDistance, defaultCompassBearingStart = 0;
    public GameObject bearingCompass;
    public GameObject targetPointer;
    public GameObject RosController;


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
                rawBearing = (float)RosController.GetComponent<ROSController>().outputBearing;
                rawDestinationBearing = RosController.GetComponent<ROSController>().outputDestinationBearing;
                rawDistance = RosController.GetComponent<ROSController>().outputDistance;
                compassBearingNow = compassBearingStart + rawBearing;
                bearingCompass.transform.eulerAngles = new Vector3(-90, 0, compassBearingNow);
                targetPointer.transform.eulerAngles = new Vector3(-90, 0, compassBearingNow + offset);
            }
            else
            {
                //perform demo mode!
                compassBearingNow = compassBearingNow + 0.01F;
                compassBearingReverse = compassBearingStart - 0.02F;
                compassPositionNow = defaultCompassBearingStart + compassBearingNow;
                bearingCompass.transform.eulerAngles = new Vector3(-90, 0, compassPositionNow);
                targetPointer.transform.eulerAngles = new Vector3(-90, 0, compassBearingReverse + offset);
            }
        }
}

    }
