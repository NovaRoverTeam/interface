using ROSBridgeLib;
using ROSBridgeLib.gps_msgs;
using ROSBridgeLib.std_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

/**
 * This is a toy example of the Unity-ROS interface talking to the TurtleSim 
 * tutorial (circa Groovy). Note that due to some changes since then this will have
 * to be slightly re-written, but as its a test ....
 * 
 * This defines the callback that links the pose message. It moves the Dalek with
 * the turtlesim
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 **/

public class GPSRead : ROSBridgeSubscriber {
	public static float latitude, longitude;

	public new static string GetMessageTopic() {
		return "/gps/gps_data";
	}  

	public new static string GetMessageType() {
		return "gps/Gps";
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new gpsMsg(msg);
	}

	public new static void CallBack(ROSBridgeMsg msg) {
		gpsMsg gps = (gpsMsg) msg;
		latitude = gps.GetLatitude();
		longitude = gps.GetLongitude();
	}
}