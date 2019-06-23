using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterStruct {
	private string charID;
	private string charName;
	private string charType;
	private string charRace;
	private string charAge;
	private string charSex;
	private string charHeight;
	private string charWeight;
	private Dictionary<string, string> charOther;

	public CharacterStruct(string _id){
		charID = _id;
		charName = "Name";
		charType = "Type";
		charRace = "Race";
		charAge = "Age";
		charSex = "Sex";
		charHeight = "Height";
		charWeight = "Weight";
		charOther = new Dictionary<string, string>();
		charOther.Add("Other", "Test");
	}

	public string id {
		get{ return charID; }
		set{ charID = value; }
	}

	public string name {
		get{ return charName; }
		set{ charName = value; }
	}

	public string type {
		get{ return charType; }
		set{ charType = value; }
	}

	public string race {
		get{ return charRace; }
		set{ charRace = value; }
	}

	public string age {
		get{ return charAge; }
		set{ charAge = value; }
	}

	public string sex {
		get{ return charSex; }
		set{ charSex = value; }
	}

	public string height {
		get{ return charHeight; }
		set{ charHeight = value; }
	}

	public string weight {
		get{ return charWeight; }
		set{ charWeight = value; }
	}

	public Dictionary<string, string> other {
		get{ return charOther; }
		set{ charOther = value; }
	}

	/*public int CompareTo(CharacterModel other){
		return 0;
	}*/

	public string DataToURL(){
		string str = "";
		str += "name=" + name + "&";
		str += "type=" + type + "&";
		str += "race=" + race + "&";
		str += "age=" + age + "&";
		str += "sex=" + sex + "&";
		str += "height=" + height + "&";
		str += "weight=" + weight + "&";
		str += "other=" + other;
		return str;
	}
}
