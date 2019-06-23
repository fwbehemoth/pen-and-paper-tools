using System;
using Enums;
using LitJson;
using Myth.BaseLib;

[Serializable]
public class MapsBusinessObject:MythBussinessObject {
    public MapCollection collection = new MapCollection();

    public override void fetch() {
        UserBusinessObject userBO = Globals.Instance().userBO;
		Globals.Instance().api.apiCall("FetchMaps.php?userid=" + Globals.Instance().userBO.model.id + "&campaignid=" + Globals.Instance().userBO.model.campaignBO.model.id, RequestMethodTypeEnum.Get, success, failure);
    }

    protected override void success(JsonData json) {
        collection.Clear();
        //Debug.Log("JSON Count: " + json["count"].ToString());
        int count = int.Parse(json["count"].ToString());
        for(int i = 0; i < count; i++) {
			MapBusinessObject mapBO = new MapBusinessObject(
                    int.Parse(json[i]["Id"].ToString().Replace("\"", "")),
                    json[i]["Name"].ToString().Replace("\"", ""));
			Add(mapBO);
        }
    }

	public void Add(MapBusinessObject mapBO){
		collection.Add(mapBO.model.id, mapBO);
	}
}
