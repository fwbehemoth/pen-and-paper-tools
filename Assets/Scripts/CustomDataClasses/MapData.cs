using System;
using System.Runtime.Serialization;
using CustomDataClasses;
using Myth.BaseLib;
using Utils;

[Serializable]
public class MapData:MythDataObject{
	public int id;
	public string name;

	public MapData(){

	}

	public MapData(SerializationInfo info, StreamingContext ctxt){
		id = info.GetInt32("mapID");
		name = info.GetString("mapName");
	}
	
	public void GetObjectData (SerializationInfo info, StreamingContext ctxt){
		info.AddValue("mapID", id);
		info.AddValue("mapName", name);
	}
}