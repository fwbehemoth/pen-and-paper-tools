using System;
using System.Runtime.Serialization;
using Myth.BaseLib;

[Serializable]
public class PieceData:MythDataObject{
	public int id;
	public string name;
    public string prefabName;
    public int rotation = 0;

	public PieceData(){

	}

	public PieceData(SerializationInfo info, StreamingContext ctxt){
		id = info.GetInt32("pieceid");
		name = info.GetString("piecename");
	}

	public void GetObjectData (SerializationInfo info, StreamingContext ctxt){
		info.AddValue("pieceid", id);
		info.AddValue("piecename", name);
	}
}
