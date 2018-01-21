using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public InputField camNumberField;
    public InputField camAngleField;
    int camInt;
    int camAngle;

    public void WhenClicked() {

        camInt = int.Parse(camNumberField.text);
        PlayerPrefs.SetInt("CameraNumber", camInt);
        Debug.Log("Camera Number set to " + PlayerPrefs.GetInt("CameraNumber").ToString());

    }

    public void WhenClicked2()
    {

        camAngle = int.Parse(camAngleField.text);
        PlayerPrefs.SetInt("CameraAngle", camAngle);
        Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());

    }

}
