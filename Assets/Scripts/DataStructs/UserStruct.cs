using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UserStruct {
	private string userID;
	private string userName;
	private string userPassword;
	private CharacterStruct characterStruct;
	private CampaignStruct campaignStruct;
	private MapStruct mapStruct;
	private Dictionary<string, TileSetStruct> userTileSetList;

	public UserStruct(string _id){
		userID = _id;
		userName = "nicktest";
		userPassword = "nicktest";
		characterStruct = new CharacterStruct("0");
		campaignStruct = new CampaignStruct("0");
		mapStruct = new MapStruct("0");
		userTileSetList = new Dictionary<string, TileSetStruct>();
		TileSetStruct tileSet = new TileSetStruct("0");
		userTileSetList.Add(tileSet.id, tileSet);
	}

	public string id {
		get{ return userID; }
		set{ userID = value; }
	}
	
	public string username {
		get{ return userName; }
		set{ userName = value; }
	}

	public string password{
		get{ return userPassword; }
		set{ userPassword = value; }
	}

	public CharacterStruct character{
		get{ return characterStruct; }
		set{ characterStruct = value; }
	}

	public CampaignStruct campaign {
		get{ return campaignStruct; }
		set{ campaignStruct = value; }
	}

	public MapStruct map {
		get{ return mapStruct; }
		set{ mapStruct = value; }
	}

	public Dictionary<string, TileSetStruct> tileSetList{
		get{ return userTileSetList; }
		set{ userTileSetList = value; }
	}

	/*public int CompareTo(UserModel other){
		return 0;
	}*/

	public string DataToURL(){
		string str = "";
		return str;
	}
}
