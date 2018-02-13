using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ROSBridgeLib;
using System.Reflection;
using System;

public class ROSController : MonoBehaviour  {
	private ROSBridgeWebSocketConnection ros = null;	
	public Text rosText, GPSText;

	// the critical thing here is to define our subscribers, publishers and service response handlers
	void Start () {
		ros = new ROSBridgeWebSocketConnection ("ws://192.168.1.199", 9090);
		ros.AddSubscriber (typeof(voltageRead));
		ros.AddSubscriber (typeof(GPSRead));
//		ros.AddPublisher (typeof(Turtle1Teleop));
//		ros.AddServiceResponse (typeof(Turtle1ServiceResponse));
		ros.Connect ();
	}

	// extremely important to disconnect from ROS. OTherwise packets continue to flow
	void OnApplicationQuit() {
		if(ros!=null)
			ros.Disconnect ();
	}

	void Update () {
		rosText.text = voltageRead.voltage.ToString("0.#") + "V";
		GPSText.text = GPSRead.latitude.ToString("0.######") + ", " + GPSRead.longitude.ToString("0.######");
		ros.Render ();
	}
}
