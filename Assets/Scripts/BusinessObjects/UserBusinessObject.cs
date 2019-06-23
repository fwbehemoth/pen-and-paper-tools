using System;
using Enums;
using LitJson;
using Myth.BaseLib;
using Myth.UI;
using UnityEngine;

[Serializable]
public class UserBusinessObject:MythBussinessObject {
    public UserData model = new UserData();
    private String path = "user.php";

    public UserBusinessObject(){
        model = new UserData();
    }

    public UserBusinessObject(string username, string password){
        model.username = username;
        model.password = password;
    }

    public override void fetch() {
        Globals.Instance().api.apiCall(path + "?" + model.toURL(), RequestMethodTypeEnum.Get, success, failure);
    }

    public override void save() {
        Globals.Instance().api.apiCall(path + "?" + model.toURL(), RequestMethodTypeEnum.Post, success, failure);
    }

    protected override void success(JsonData json) {
        if(json["result"].ToString().Replace("\"", "") == "right") {
            model.id = int.Parse(json["id"].ToString().Replace("\"", ""));
            Globals.Instance().LoginWinType = GUIManager.WindowType.PlayerSelection;
        } else if(json["result"].ToString().Replace("\"", "") == "wrong") {
            GameObject guiObj = GameObject.Find("Login Panel");
            guiObj.SendMessage("invalidLoginTrue");
        }
    }

    public void checkName() {
        Globals.Instance().api.apiCall(path + "?" + model.toURL(), RequestMethodTypeEnum.Get, checkNameSuccess, failure);
    }

    private void checkNameSuccess(JsonData json) {
        GameObject guiObj = GameObject.Find("New Account Panel");
        if(json["result"].ToString().Replace("\"", "") == "right") {
            guiObj.SendMessage("nameDoesExist");
        } else {
            guiObj.SendMessage("nameDoesNotExist");
        }
    }

	public void setUsernameAndPassword(string username, string password){
		model.username = username;
		model.password = password;
	}
}
