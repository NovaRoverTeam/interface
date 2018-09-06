using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GSTexperimentManager : MonoBehaviour {
    string cameraURI;
    string cameraIP;
    string cameraPort;
    string m_uri;
    public RawImage WebcamWindow;

	// Use this for initialization
	void Start () {
        cameraIP = PlayerPrefs.GetString("CameraIP");
        cameraPort = PlayerPrefs.GetInt("CameraPort").ToString();
        cameraURI = "udp://@" + cameraIP + ":" + cameraPort;
        Debug.Log("AssembledURI is " + cameraURI);
        WebcamWindow.GetComponent<GstUnityBridgeTexture>().m_URI = cameraURI;
        //m_URI = "askfhj";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
