using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Myth.BaseLib;

[Serializable]
public class UserData:MythDataObject{
	public int id;
	public string username = " ";
	public string password = " ";
	public CharacterBusinessObject characterBO;
	public CampaignBusinessObject campaignBO;
	public MapBusinessObject mapBO;
	public TileSetsBusinessObject tileSetsBO;

	public UserData(){

	}

	public UserData(SerializationInfo info, StreamingContext ctxt){
		id = info.GetInt32("userid");
		username = info.GetString("username");
		password = info.GetString("password");
		characterBO = (CharacterBusinessObject)info.GetValue("playerbusinessobject", typeof(CharacterBusinessObject));
		campaignBO = (CampaignBusinessObject)info.GetValue("campaignbusinessobject", typeof(CampaignBusinessObject));
		mapBO = (MapBusinessObject)info.GetValue("mapbusinessobject", typeof(MapBusinessObject));
		tileSetsBO = (TileSetsBusinessObject)info.GetValue("tilesetsbusinessobject", typeof(Dictionary<string, TileSetsBusinessObject>));
	}

	public void GetObjectData (SerializationInfo info, StreamingContext ctxt){
		info.AddValue("userid", id);
		info.AddValue("username", username);
		info.AddValue("password", password);
		info.AddValue("playerbusinessobject", characterBO);
		info.AddValue("campaignbusinessobject", campaignBO);
		info.AddValue("mapbusinessobject", mapBO);
		info.AddValue("tilesetbusinessobject", tileSetsBO);
	}

    /*public int CompareTo(UserModel other){
		return 0;
	}*/

    public override string toURL()
    {
        string str = "";
        str += "id=" + id + "&";
        str += "username=" + username + "&";
        str += "password=" + password;

        return str;
    }
}
