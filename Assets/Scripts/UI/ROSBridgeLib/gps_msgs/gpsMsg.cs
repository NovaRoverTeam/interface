/*
 * Define a turtle pose message. This has been hand-crafted from the corresponding
 * turtle message file.
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 */
using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace gps_msgs {
		public class gpsMsg : ROSBridgeMsg {
			private double _latitude, _longitude;

			public gpsMsg(JSONNode msg) {
				//Debug.Log (msg.ToString ());
				_latitude = double.Parse(msg["latitude"]["data"]);
				_longitude = double.Parse(msg["longitude"]["data"]);
			}

			public gpsMsg(double latitude, double longitude) {
				_latitude = latitude;
				_longitude = longitude;
			}

			public static string getMessageType() {
				return "gps/Gps";
			}

			public double GetLatitude() {
				return _latitude;
			}

			public double GetLongitude() {
				return _longitude;
			}

			public override string ToString() {
				return "gps/Gps [latitude=" + _latitude + ",  longitude=" + _longitude + "]";
			}

			public override string ToYAMLString() {
				return "{\"latitude\": " + _latitude + ", \"longitude\": " + _longitude + "}";
			}
		}
	}
}
