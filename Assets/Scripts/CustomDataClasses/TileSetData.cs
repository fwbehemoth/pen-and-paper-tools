using System;
using System.Runtime.Serialization;
using Myth.BaseLib;

[Serializable]
public class TileSetData:MythDataObject{
	public int id;
	public string name;
	public TilesBusinessObject tilesBO = new TilesBusinessObject();
	public PiecesBusinessObject piecesBO = new PiecesBusinessObject();

	public TileSetData(){
		
	}
	
	public TileSetData(SerializationInfo info, StreamingContext ctxt){
		id = info.GetInt32("setid");
		name = info.GetString("setname");
		tilesBO = (TilesBusinessObject)info.GetValue("settilelist", typeof(TilesBusinessObject));
		piecesBO = (PiecesBusinessObject)info.GetValue("setpiecelist", typeof(PiecesBusinessObject));
	}
	
	public void GetObjectData (SerializationInfo info, StreamingContext ctxt){
		info.AddValue("setid", id);
		info.AddValue("setname", name);
		info.AddValue("settilelist", tilesBO);
		info.AddValue("setpiecelist", piecesBO);
	}
}
