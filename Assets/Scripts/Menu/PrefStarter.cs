using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefStarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("CameraNumber"))
        {
            Debug.Log("No Camera Number set, Camera Number Set to 0");
            PlayerPrefs.SetInt("CameraNumber", 0);
        }

        if (!PlayerPrefs.HasKey("CameraAngle"))
        {
            Debug.Log("No Camera Angle set, Camera Angle Set to 0");
            PlayerPrefs.SetInt("CameraAngle", 0);
        }



    }
	
}
