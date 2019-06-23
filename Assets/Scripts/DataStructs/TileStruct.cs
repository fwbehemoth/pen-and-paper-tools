using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileStruct {
	private string tileID;
	private string tileName;

	public TileStruct(string _id){
		tileID = _id;
		tileName = "";
		//tileName = "Test_Brick_Tile";
	}

	public string id {
		get{ return tileID; }
		set{ tileID = value; }
	}

	public string name {
		get{ return tileName; }
		set{ tileName = value; }
	}

	public string DataToURL(){
		string str = "";
		return str;
	}
}
