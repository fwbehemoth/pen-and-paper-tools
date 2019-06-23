using System.Collections;
using System.Collections.Generic;
using System.Text;
using Enums;
using LitJson;
using UnityEngine;
using Utils;

namespace Myth.BaseLib {
	public class MythAPI : MonoBehaviour {
		private string baseURL = "http://www.nickmcgough.com/TableTop/";
		public delegate void CallSuccessFunction(JsonData json);
		public delegate void CallFailureFunction(string error);
		public delegate void WithDataSuccessFunction(JsonData json);
		public delegate void WithDataFailureFunction(string error);

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public void apiCall(string path, RequestMethodTypeEnum requestMethodType, CallSuccessFunction success, CallFailureFunction failure) {
			StartCoroutine(api(path, requestMethodType, success, failure));
		}

		IEnumerator api(string path, RequestMethodTypeEnum requestMethodType, CallSuccessFunction success, CallFailureFunction failure) {
			string url = baseURL + path;
            Dictionary<string, string> postHeader = new Dictionary<string, string>();
            postHeader.Add("Content-Type", "application/json");
            postHeader.Add("X-HTTP-Method-Override", RequestMethodTypeUtils.getRequestMethodTypeString(requestMethodType));

			url = url.Replace(" ", "%20");
			Debug.Log("url:" + url);
			WWW call = new WWW(url, null, postHeader);
			yield return call;
			Debug.Log("Call_text:" + call.text);

			if (!string.IsNullOrEmpty(call.error)) {
				failure(path + "-Error: " + call.error);
			} else if (call.isDone) {
				JsonData json = JsonMapper.ToObject(call.text);
				success(json);
			}
		}

		//Sending JSON Data
		public void apiCall(string path, string json, RequestMethodTypeEnum requestMethodType, WithDataSuccessFunction success, WithDataFailureFunction failure) {
			StartCoroutine(api(path, json, requestMethodType, success, failure));
		}

		IEnumerator api(string path, string json, RequestMethodTypeEnum requestMethodType, WithDataSuccessFunction success, WithDataFailureFunction failure) {
			string url = baseURL + path;
			byte[] bytesToSend = Encoding.UTF8.GetBytes(json.ToCharArray());
			Dictionary<string, string> postHeader = new Dictionary<string, string>();
			postHeader.Add("Content-Type", "application/json");
            postHeader.Add("X-HTTP-Method-Override", RequestMethodTypeUtils.getRequestMethodTypeString(requestMethodType));

			url = url.Replace(" ", "%20");
			Debug.Log("url:" + url);
			WWW call = new WWW(url, bytesToSend, postHeader);
			yield return call;
			if (call.error != null) {
				failure(path + "-Error: " + call.error);
			} else if (call.isDone) {
                Debug.Log("call text: " + call.text);
				JsonData returned_json = JsonMapper.ToObject(call.text);
				success(returned_json);
			}
		}
	}
}
