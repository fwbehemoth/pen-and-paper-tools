using System;
using System.Runtime.Serialization;

namespace Myth.BaseLib {
	public class MythDataObject /*: ISerializable*/ {
		private string url;

		public MythDataObject() {

		}

		public MythDataObject(SerializationInfo info, StreamingContext ctxt) {

		}

		public void GetObjectData (SerializationInfo info, StreamingContext ctxt) {

		}

		public virtual string toURL() {
			Globals.Instance().DebugLog(this.GetType().Name, "Forgt to overrise");
			return "";
		}

		//	public string DataToURL(PropertyInfo[] properties){
		//		Debug.Log ("DataToURL");
		//		string str = "";
		//		int propCount = properties.Length;
		//		Debug.Log ("Prop Count: " + propCount);
		//		int count = 0;
		//		foreach (PropertyInfo prop in properties) {
		//			str += prop.Name + "=" + prop.GetValue(dataClass, null);
		//			count++;
		//			if(count < propCount){
		//				str += "&";
		//			}
		//			Debug.Log ("Property Name: " + prop.Name);
		//		}
		//		return str;
		//	}
	}
}
