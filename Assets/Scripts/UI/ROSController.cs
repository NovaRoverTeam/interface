using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ROSBridgeLib;
using System.Reflection;
using System;

public class ROSController : MonoBehaviour  {
	private ROSBridgeWebSocketConnection ros = null;
    public Text voltageText, GPSText, bearingText, destinationBearing;
    public TextMesh distanceToDestination;
    public float outputVoltage, outputBearing, outputDistance, outputDestinationBearing;

    public float compassBearingStart, offset, compassBearingNow, compassPositionNow, compassBearingReverse;
    private float rawBearing, rawDestinationBearing, rawDistance, defaultCompassBearingStart = 0;
    public GameObject bearingCompass;
    public GameObject targetPointer;

    // the critical thing here is to define our subscribers, publishers and service response handlers
    void Start () {
		ros = new ROSBridgeWebSocketConnection ("ws://192.168.1.199", 9090);
		//ros.AddSubscriber(typeof(VoltageRead));
        ros.AddSubscriber(typeof(BearingRead));

        if (PlayerPrefs.GetInt("TargetExists?") == 1)
        {
            //ros.AddSubscriber(typeof(GPSRead));
           ros.AddSubscriber(typeof(RetrieveRead));
        }
        else
        {
            targetPointer.SetActive(false);
        }
        ros.Connect();
        Debug.Log("ROS is on");
    }

	// extremely important to disconnect from ROS. OTherwise packets continue to flow
	void OnApplicationQuit() {
		if(ros!=null)
               ros.Disconnect ();
    }

	void Update () {
        //voltageText.text = VoltageRead.voltage4.ToString("0.#") + "V";
        //outputVoltage = VoltageRead.voltage4;
        //GPSText.text = GPSRead.latitude.ToString("0.######") + ", " + GPSRead.longitude.ToString("0.######");
        bearingText.text = BearingRead.bearing.ToString("") + "degrees";
        outputBearing = BearingRead.bearing + offset;
        //Debug.Log(BearingRead.bearing);
        distanceToDestination.text = RetrieveRead.destinationDistance.ToString("") + "m";
        //outputDistance = RetrieveRead.destinationDistance;
        destinationBearing.text = RetrieveRead.destinationBearing.ToString("") + "degrees";
        outputDestinationBearing = RetrieveRead.destinationBearing + offset;
        bearingCompass.transform.eulerAngles = new Vector3(-90, 0, -outputBearing);
        Debug.Log("compass position should have updated");
        if (PlayerPrefs.GetInt("TargetExists?") == 1)
        {
            targetPointer.transform.position = new Vector3(750 * Mathf.Sin((outputDestinationBearing-outputBearing) * (Mathf.PI / 180)), 120 , 750 * Mathf.Cos((outputDestinationBearing-outputBearing) * (Mathf.PI / 180)));
            targetPointer.transform.eulerAngles = new Vector3(0, outputDestinationBearing-outputBearing, 0);
            Debug.Log("skyrim pointer should update");

        }
        ros.Render();
        Debug.Log("ROS rendered, whatever that means");
        //targetPointer.transform.eulerAngles = new Vector3(-90, 0, compassBearingNow + offset);
    }
}
