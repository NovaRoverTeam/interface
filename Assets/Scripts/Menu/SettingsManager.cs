﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public InputField camAngleField;
    public Toggle webcamOn;
    int camInt;
    int camPIPInt;
    int camAngle;
    int webslidercamOn;
    int defaultcamNumber = 0;
    int defaultcamAngle = 0;
    int defaultcamPIPNumber = 1;
    int defaultwebslidercamOn = 0;

    //Dropdown Objects for main cam
    public Dropdown m_Dropdown;
    string m_MyString;
    int m_Index;

    //Dropdown Objects for PIP cam
    public Dropdown m_Dropdown_sidekick;
    string m_MyString_sidekick;
    int m_Index_sidekick;

    //Drop down options:
    //Use these for adding options to the Dropdown List
    Dropdown.OptionData m_NewData;
    //The list of messages for the Dropdown
    List<Dropdown.OptionData> m_Messages = new List<Dropdown.OptionData>();


    public void CamNumberFieldTracker() {

        camInt = m_Dropdown.value;
        PlayerPrefs.SetInt("CameraNumber", camInt);
        Debug.Log("Camera Number set to " + PlayerPrefs.GetInt("CameraNumber").ToString());

    }

    public void CamPIPNumberFieldTracker()
    {

        camPIPInt = m_Dropdown_sidekick.value;
        PlayerPrefs.SetInt("CameraPIPNumber", camPIPInt);
        Debug.Log("PIP Webcam Number set to " + PlayerPrefs.GetInt("CameraPIPNumber").ToString());

    }

    public void CamAngleFieldTracker()
    {

        camAngle = int.Parse(camAngleField.text);
        PlayerPrefs.SetInt("CameraAngle", camAngle);
        Debug.Log("Camera Angle set to " + PlayerPrefs.GetInt("CameraAngle").ToString());

    }

    public void WebCamSliderTracker()
    {
        if (webcamOn.isOn)
        {   webslidercamOn = 1;
            Debug.Log("Webcam is on");
        }
        else
        {   webslidercamOn = 0;
            Debug.Log("Webcam is off");
        }
        PlayerPrefs.SetInt("WebcamOn", webslidercamOn);
        Debug.Log("Webcam On value is " + PlayerPrefs.GetInt("WebcamOn").ToString());

    }

    void Start ()
    {

        //Fetch the Dropdown GameObject the script is attached to
        //m_Dropdown = GetComponent<Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        m_Dropdown_sidekick.ClearOptions();

        //grab the webcam list
        WebCamDevice[] devices = WebCamTexture.devices;

        //populate a message with all of the USB webcam devices
        for (int i = 0; i < devices.Length; i++)
        {       m_NewData = new Dropdown.OptionData();
                m_NewData.text = devices[i].name;
                //Debug.Log("Following added to Dropdown list " + m_NewData.text);
                m_Messages.Add(m_NewData);
        }

        //Take each entry in the message List to generate both menus
        foreach (Dropdown.OptionData message in m_Messages)
        {
            m_Dropdown.options.Add(message);
            m_Index = m_Messages.Count - 1;
            m_Dropdown_sidekick.options.Add(message);
            m_Index_sidekick = m_Messages.Count - 1;
        }

        if (PlayerPrefs.HasKey("CameraNumber"))   
        {
            m_Dropdown.value = PlayerPrefs.GetInt("CameraNumber");
        }
        else
        {
            m_Dropdown.value = defaultcamNumber;
        }
        if (m_Dropdown.value == 0)
        { m_Dropdown.itemText.text = devices[0].name; }

        if (PlayerPrefs.HasKey("CameraPIPNumber"))

        {
            m_Dropdown_sidekick.value = PlayerPrefs.GetInt("CameraPIPNumber");
        }
        else
        {
            m_Dropdown_sidekick.value = defaultcamPIPNumber;
        }
        if (m_Dropdown_sidekick.value == 0)
        { m_Dropdown_sidekick.itemText.text = devices[0].name; }

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
            if (webslideronint == 1)
            { webcamOn.isOn = true; }
            else
            { webcamOn.isOn = false; }
        }

    }

}
