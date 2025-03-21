﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Serializer {

	public static T Load<T>(string filename) where T: class{
		if (File.Exists(filename)){
			try{
				using (Stream stream = File.OpenRead(filename)){
					BinaryFormatter formatter = new BinaryFormatter();
					return formatter.Deserialize(stream) as T;
				}
			}
			catch (Exception e){
				Debug.Log(e.Message);
			}
		}
		return default(T);
	}
	
	public static void Save<T>(string filename, T data) where T: class{
		using (Stream stream = File.OpenWrite(filename)){
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, data);
		}
	}
}
