using System;
using Enums;
using LitJson;
using Myth.BaseLib;
using UnityEngine;

[Serializable]
public class CampaignBusinessObject:MythBussinessObject {
    public CampaignData model = new CampaignData();

    public CampaignBusinessObject(){}

    public CampaignBusinessObject(string name, string game, string setting){
        model.name = name;
        model.game = game;
        model.setting = setting;
    }

    public CampaignBusinessObject(int id, string name, string game, string setting) {
		model.id = id;
        model.name = name;
        model.game = game;
        model.setting = setting;
	}

    public void copy(CampaignData data){
        new CampaignBusinessObject(data.id, data.name, data.game, data.setting);
    }

    public override void save(){
        Globals.Instance().api.apiCall("CreateCampaign.php?" + "userid=" + Globals.Instance().userBO.model.id + "&" + model.toURL(), RequestMethodTypeEnum.Post, success, failure);
    }

    public override void fetch(){
        Globals.Instance().api.apiCall("FetchCampaign.php?" + "campaignid=" + Globals.Instance().userBO.model.characterBO.model.campaignId + "&userid=" + Globals.Instance().userBO.model.id, RequestMethodTypeEnum.Get, fetchSuccess, failure);
    }

    protected void fetchSuccess(JsonData json) {
                model.id = int.Parse(json["Id"].ToString().Replace("\"", ""));
                model.name = json["Name"].ToString().Replace("\"", "");
                model.game = json["Game"].ToString().Replace("\"", "");
                model.setting = json["Setting"].ToString().Replace("\"", "");
    }
}
