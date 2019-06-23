using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PieceStruct {
	private string pieceID;
	private string pieceName;

	public PieceStruct(string _id){
		pieceID = _id;
		pieceName = "";
		//pieceName = "Test_Brick_Pillar";
	}

	public string id {
		get{ return pieceID; }
		set{ pieceID = value; }
	}
	
	public string name {
		get{ return pieceName; }
		set{ pieceName = value; }
	}

	public string DataToURL(){
		string str = "";
		return str;
	}
}
