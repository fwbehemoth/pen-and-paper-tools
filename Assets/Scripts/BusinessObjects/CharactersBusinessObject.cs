using System;
using Enums;
using LitJson;
using Myth.BaseLib;

[Serializable]
public class CharactersBusinessObject:MythBussinessObject {
    public CharacterCollection collection = new CharacterCollection();
    public override void fetch() {
        Globals.Instance().api.apiCall("FetchCharacters.php?userid=" + Globals.Instance().userBO.model.id, RequestMethodTypeEnum.Get, success, failure);
    }

    protected override void success(JsonData json) {
        collection.Clear();
        //Debug.Log("JSON Count: " + json["count"].ToString());
        int count = int.Parse(json["count"].ToString());
        for(int i = 0; i < count; i++) {
			CharacterBusinessObject charBO = new CharacterBusinessObject(
                    int.Parse(json[i]["Id"].ToString().Replace("\"", "")),
                    json[i]["Name"].ToString().Replace("\"", ""),
                    json[i]["Type"].ToString().Replace("\"", ""),
                    json[i]["Race"].ToString().Replace("\"", ""),
                    int.Parse(json[i]["Age"].ToString().Replace("\"", "")),
                    json[i]["Sex"].ToString().Replace("\"", ""),
                    json[i]["Height"].ToString().Replace("\"", ""),
                    json[i]["Weight"].ToString().Replace("\"", ""),
                    int.Parse(json[i]["CampaignId"].ToString().Replace("\"", ""))
            );
            Add (charBO);
        }
    }

    public void Add(CharacterBusinessObject charBO){
        collection.Add(charBO.model.id, charBO);
    }
}
