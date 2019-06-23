using System;
using Enums;
using LitJson;
using Myth.BaseLib;
using UnityEngine;

[Serializable]
public class CampaignsBusinessObject:MythBussinessObject {
    public CampaignCollection collection = new CampaignCollection();

    public override void fetch() {
        Globals.Instance().api.apiCall("FetchCampaigns.php?userid=" + Globals.Instance().userBO.model.id, RequestMethodTypeEnum.Get, success, failure);
    }

    protected override void success(JsonData json) {
        Debug.Log("JSON Count: " + json["count"].ToString());
        collection.Clear();
        int count = int.Parse(json["count"].ToString());
        for(int i = 0; i < count; i++) {
            CampaignBusinessObject tempCampBO = new CampaignBusinessObject(
                    int.Parse(json[i]["Id"].ToString().Replace("\"", "")),
                    json[i]["Name"].ToString().Replace("\"", ""),
                    json[i]["Game"].ToString().Replace("\"", ""),
                    json[i]["Setting"].ToString().Replace("\"", ""));

            collection.Add(tempCampBO.model.id, tempCampBO);
        }
    }
}
