using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefStarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("TestKey", 42);
        if (!PlayerPrefs.HasKey("CameraNumber"))
        {
            Debug.Log("Overuled, Camera Number Set to 1");
            PlayerPrefs.SetInt("CameraNumber", 1);
        }



    }
	
}
