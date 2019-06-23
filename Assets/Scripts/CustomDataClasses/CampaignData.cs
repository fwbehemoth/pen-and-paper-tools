using System;
using Myth.BaseLib;

[Serializable]
public class CampaignData:MythDataObject {
	public int id;
	public string name;
	public string game;
	public string setting;

	public override string toURL(){
		string str = "id=" + id + "&";
		str += "name=" + name + "&";
		str += "game=" + game + "&";
		str += "set=" + setting;
		return str;
	}
}
