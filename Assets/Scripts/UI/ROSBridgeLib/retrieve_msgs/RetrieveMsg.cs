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
	namespace retrieve_msgs {
		public class RetrieveMsg : ROSBridgeMsg {
			private float _bearing, _distance;

			public RetrieveMsg(JSONNode msg) {
                //Debug.Log (msg.ToString ());
                _bearing = float.Parse(msg["bearing"]);
                _distance = float.Parse(msg["distance"]);
			}

			public RetrieveMsg(float bearing, float distance) {
                _bearing = bearing;
                _distance = distance;
			}

			public static string getMessageType() {
				return "rover/Retrieve";
			}

			public float GetBearing() {
				return _bearing;
			}

			public float GetDistance() {
				return _distance;
			}

			public override string ToString() {
				return "rover/Retrieve [bearing=" + _bearing + ",  distance=" + _distance + "]";
			}

			public override string ToYAMLString() {
				return "{\"bearing\": " + _bearing + ", \"distance\": " + _distance + "}";
			}
		}
	}
}
