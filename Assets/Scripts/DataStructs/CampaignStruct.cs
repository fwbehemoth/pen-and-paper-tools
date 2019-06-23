using System;
using System.Collections;
using UnityEngine;

public struct CampaignStruct {
	private string campID;
	private string campName;
	private string campGame;
	private string campSetting;

	public CampaignStruct(string _id){
		campID = _id;
		campName = "Name";
		campGame = "Game";
		campSetting = "Setting";
	}

	public string id {
		get{ return campID; }
		set{ campID = value; }
	}

	public string name {
		get{ return campName; }
		set{ campName = value; }
	}

	public string game {
		get{ return campGame; }
		set{ campGame = value; }
	}

	public string setting {
		get{ return campSetting; }
		set{ campSetting = value; }
	}

	/*public int CompareTo(CampaignModel other){
		return 0;
	}*/

	public string DataToURL(){
		string str = "";
		return str;
	}
}
