using System;
using System.Runtime.Serialization;
using Myth.BaseLib;

[Serializable]
public class TileData:MythDataObject{
	public int id;
	public string name;
	public int rowIndex;
	public int columnIndex;
    public float centerX;
    public float centerY;
    public string prefabName;

	public TileData(){

	}

	public TileData(SerializationInfo info, StreamingContext ctxt){
		id = info.GetInt32("tileid");
		name = info.GetString("tilename");
		rowIndex = info.GetInt32("rowIndex");
		columnIndex = info.GetInt32("columnIndex");
	}
	
	public void GetObjectData (SerializationInfo info, StreamingContext ctxt){
		info.AddValue("tileid", id);
		info.AddValue("tilename", name);
		info.AddValue("rowIndex", rowIndex);
		info.AddValue("columnIndex", columnIndex);
	}
}
