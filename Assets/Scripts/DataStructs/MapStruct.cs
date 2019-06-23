using System;
using System.Collections;
using UnityEngine;

public struct MapStruct{
	public string mapID;
	public string mapName;
	public TileStruct[,] mapTiles;
	public PieceStruct[,] mapPieces;

	public MapStruct(string _id){
		mapID = _id;
		mapName = "Test";
		mapTiles = new TileStruct[50, 50];
		mapPieces = new PieceStruct[50, 50];
	}

	public string id {
		get{ return mapID; }
		set{ mapID = value; }
	}
	
	public string name {
		get{ return mapName; }
		set{ mapName = value; }
	}
	
	public TileStruct[,] tiles {
		get{ return mapTiles; }
		set{ mapTiles = value; }
	}

	public PieceStruct[,] pieces {
		get{ return mapPieces; }
		set{ mapPieces = value; }
	}

	public string DataToURL(){
		string str = "";
		return str;
	}
}
