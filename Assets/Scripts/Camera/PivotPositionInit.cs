using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is currently used to rotate the UV sphere into a position which puts the ground at the base of the camera.
// I plan to lock all axes except the x-axis
public class PivotPositionInit : MonoBehaviour {
    int cameraAngle;
    // Use this for initialization

    void Start () {
        cameraAngle = PlayerPrefs.GetInt("CameraAngle");
        Vector3 pivotangle = new Vector3(cameraAngle, 0, 0);
        transform.eulerAngles = pivotangle;



    }
	
}
