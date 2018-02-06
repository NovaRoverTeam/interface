using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public InputField camNumberField;
    public InputField camAngleField;
    public InputField camPIPField;
    public Slider webcamOn;
    int camInt;
    int camPIPInt;
    int camAngle;
    int webslidercamOn;
    int defaultcamNumber = 0;
    int defaultcamAngle = 0;
    int defaultcamPIPNumber = 1;
    int defaultwebslidercamOn = 0;

    public void camNumberFieldTracker() {

        camInt = int.Parse(camNumberField.text);
        PlayerPrefs.SetInt("CameraNumber", camInt);
        Debug.Log("Camera Number set to " + PlayerPrefs.GetInt("CameraNumber").ToString());

    }

    public void camPIPNumberFieldTracker()
    {

        camPIPInt = int.Parse(camPIPField.text);
        PlayerPrefs.SetInt("CameraPIPNumber", camPIPInt);
        Debug.Log("PIP Webcam Number set to " + PlayerPrefs.GetInt("CameraPIPNumber").ToString());

    }

    public void camAngleFieldTracker()
    {

        camAngle = int.Parse(camAngleField.text);
        PlayerPrefs.SetInt("CameraAngle", camAngle);
        Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());

    }

    public void webCamSliderTracker()
    {

        webslidercamOn = (int)webcamOn.value;
        PlayerPrefs.SetInt("WebcamOn", webslidercamOn);
        Debug.Log("Webcam On value is " + PlayerPrefs.GetInt("WebcamOn").ToString());

    }

    void Start ()
    {
        if (PlayerPrefs.HasKey("CameraNumber"))
            
        { camNumberField.text = PlayerPrefs.GetInt("CameraNumber").ToString();
            //Debug.Log("Camera Number set to " + PlayerPrefs.GetInt("CameraNumber").ToString());
        }
        else
        { camNumberField.text = defaultcamNumber.ToString();
            //Debug.Log("Camera Number set to " + PlayerPrefs.GetInt("CameraNumber").ToString());
        }

        if (PlayerPrefs.HasKey("CameraPIPNumber"))

        {
            camPIPField.text = PlayerPrefs.GetInt("CameraPIPNumber").ToString();
            //Debug.Log("PIP Webcam Number set to " + PlayerPrefs.GetInt("CameraPIPNumber").ToString());
        }
        else
        {
            camPIPField.text = defaultcamPIPNumber.ToString();
            //Debug.Log("PIP Webcam Number set to " + PlayerPrefs.GetInt("CameraPIPNumber").ToString());
        }


        if (PlayerPrefs.HasKey("CameraAngle"))
        { camAngleField.text = PlayerPrefs.GetInt("CameraAngle").ToString();
            //Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());
        }
        else
        { camAngleField.text = defaultcamAngle.ToString();
            //Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());
        }

        if (PlayerPrefs.HasKey("WebcamOn"))
        {
            int webslideronint = PlayerPrefs.GetInt("WebcamOn");
            webcamOn.value = (float)webslideronint;
            //Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());
        }
        else
        {
            webcamOn.value = (float)defaultwebslidercamOn;
            //Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());
        }
    }

}
