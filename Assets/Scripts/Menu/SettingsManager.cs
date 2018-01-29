using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public InputField camNumberField;
    public InputField camAngleField;
    int camInt;
    int camAngle;
    int defaultcamNumber = 0;
    int defaultcamAngle = 0;

    public void camNumberFieldTracker() {

        camInt = int.Parse(camNumberField.text);
        PlayerPrefs.SetInt("CameraNumber", camInt);
        Debug.Log("Camera Number set to " + PlayerPrefs.GetInt("CameraNumber").ToString());

    }

    public void camAngleFieldTracker()
    {

        camAngle = int.Parse(camAngleField.text);
        PlayerPrefs.SetInt("CameraAngle", camAngle);
        Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());

    }

    void Start ()
    {
        if (PlayerPrefs.HasKey("CameraNumber"))
            
        { camNumberField.text = PlayerPrefs.GetInt("CameraNumber").ToString();
            Debug.Log("Camera Number set to " + PlayerPrefs.GetInt("CameraNumber").ToString());
        }
        else
        { camNumberField.text = defaultcamNumber.ToString();
            Debug.Log("Camera Number set to " + PlayerPrefs.GetInt("CameraNumber").ToString());
        }


        if (PlayerPrefs.HasKey("CameraAngle"))
        { camAngleField.text = PlayerPrefs.GetInt("CameraAngle").ToString();
            Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());
        }
        else
        { camAngleField.text = defaultcamAngle.ToString();
            Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());
        }
    }

}
