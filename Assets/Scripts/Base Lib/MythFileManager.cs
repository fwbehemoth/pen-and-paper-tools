using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Myth.BaseLib {
	public class MythFileManager : MonoBehaviour {
		private static string baseEncryptStr = "thethingsgodsaremadeof";
		public delegate void LocalDataSuccessFunction(string data);
		public delegate void LocalDataFailureFunction(string error);

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public string encrypt(string toEncrypt){
			string encryptStr = (Globals.Instance ().userBO.model.username + baseEncryptStr);
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes (encryptStr.Substring(0, 16));
			// 256-AES key
			byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes (toEncrypt);
			RijndaelManaged rDel = new RijndaelManaged ();
			rDel.Key = keyArray;
			rDel.Mode = CipherMode.ECB;
			rDel.Padding = PaddingMode.PKCS7;
			ICryptoTransform cTransform = rDel.CreateEncryptor ();
			byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
			return Convert.ToBase64String (resultArray, 0, resultArray.Length);
		}

		public string decrypt (string toDecrypt){
			string encryptStr = (Globals.Instance ().userBO.model.username + baseEncryptStr);
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes (encryptStr.Substring(0, 16));
			// AES-256 key
			byte[] toEncryptArray = Convert.FromBase64String (toDecrypt);
			RijndaelManaged rDel = new RijndaelManaged ();
			rDel.Key = keyArray;
			rDel.Mode = CipherMode.ECB;
			// http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
			rDel.Padding = PaddingMode.PKCS7;
			// better lang support
			ICryptoTransform cTransform = rDel.CreateDecryptor ();
			byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
			return UTF8Encoding.UTF8.GetString (resultArray);
		}

		public void saveToFile(string path, string data){
            Debug.Log("saveToFile- path: " + path + " / data: " + data);
			FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete);
			StreamWriter sw = new StreamWriter(fs);
			sw.WriteLine(data);
			sw.Close();
			fs.Close();
		}

		public void loadLocalFile(string path, LocalDataSuccessFunction success, LocalDataFailureFunction failure){
            try {
                StreamReader streamReader = new StreamReader(path);
				string fileData = streamReader.ReadToEnd();
				if (string.IsNullOrEmpty(fileData)) {
					failure("No Map Data");
				} else {
                    Debug.Log("Local File Data: " + fileData);
					success(fileData);
				}
			} catch(FileNotFoundException exception) {
                failure("No Map Data: " + exception.Message);
			}
		}
	}
}
