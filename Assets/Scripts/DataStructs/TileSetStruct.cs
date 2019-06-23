using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileSetStruct {
	private string setID;
	private string setName;
	private Dictionary<string, TileStruct> setTileList;
	private Dictionary<string, PieceStruct> setPieceList;

	public TileSetStruct(string _id){
		setID = _id;
		setName = "Test_Brick";

		TileStruct tile = new TileStruct("0");
		setTileList = new Dictionary<string, TileStruct>();
		setTileList.Add(tile.id, tile);

		PieceStruct piece = new PieceStruct("0");
		setPieceList = new Dictionary<string, PieceStruct>();
		setPieceList.Add (piece.id, piece);
	}

	public string id {
		get{ return setID; }
		set{ setID = value; }
	}
	
	public string name {
		get{ return setName; }
		set{ setName = value; }
	}

	public Dictionary<string, TileStruct> tileList {
		get{ return setTileList; }
		set{ setTileList = value; }
	}

	public void addToTileList(TileStruct tile){
		setTileList.Add(tile.id, tile);
	}

	public Dictionary<string, PieceStruct> pieceList {
		get{ return setPieceList; }
		set{ setPieceList = value; }
	}

	public void addToPieceList(PieceStruct piece){
		setPieceList.Add(piece.id, piece);
	}

	public void clearTileSet(){
		setTileList = new Dictionary<string, TileStruct>();
		setPieceList = new Dictionary<string, PieceStruct>();
	}

	public string DataToURL(){
		string str = "";
		return str;
	}

}
