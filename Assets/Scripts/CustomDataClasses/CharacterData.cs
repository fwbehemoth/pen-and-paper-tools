using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Myth.BaseLib;

[Serializable]
public class CharacterData:MythDataObject{
	public int id;
	public string name;
	public string type;
	public string race;
	public int age;
	public string gender;
	public string height;
	public string weight;
	public int campaignId;
	public Dictionary<string, string> other;

	public CharacterData(){

	}

	public CharacterData(SerializationInfo info, StreamingContext ctxt){
		id = info.GetInt32("charid");
		name = info.GetString("charname");
		type = info.GetString("charType");
		race = info.GetString("charRace");
		age = info.GetInt32("charage");
		gender = info.GetString("chargender");
		height = info.GetString("charheight");
		weight = info.GetString("charweight");
		campaignId = info.GetInt32("charcampaignid");
		other = (Dictionary<string, string>)info.GetValue("charother", typeof(Dictionary<string, string>));
	}

	public void GetObjectData (SerializationInfo info, StreamingContext ctxt){
		info.AddValue("charid", id);
		info.AddValue("charname", name);
		info.AddValue("chartype", type);
		info.AddValue("charrace", race);
		info.AddValue("charage", age);
		info.AddValue("chargender", gender);
		info.AddValue("charheight", height);
		info.AddValue("charweight", weight);
		info.AddValue("charother", other);
		info.AddValue("charcampaignid", campaignId);
	}

	/*public int CompareTo(CharacterModel other){
		return 0;
	}*/

	public override string toURL(){
		string str = "";
		str += "name=" + name + "&";
		str += "type=" + type + "&";
		str += "race=" + race + "&";
		str += "age=" + age + "&";
		str += "sex=" + gender + "&";
		str += "height=" + height + "&";
		str += "weight=" + weight + "&";
		str += "other=" + other;
		return str;
	}
}
