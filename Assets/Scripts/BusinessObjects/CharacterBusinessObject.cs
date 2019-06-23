using System;
using Enums;
using Myth.BaseLib;

[Serializable]
public class CharacterBusinessObject : MythBussinessObject {
    public CharacterData model = new CharacterData();

    public override void save(){
        Globals.Instance().api.apiCall("CreateCharacter.php?" + "userid=" + Globals.Instance().userBO.model.id + "&" + model.toURL(), RequestMethodTypeEnum.Post, success, failure);
    }

    public CharacterBusinessObject(string name, string type, string race, int age, string gender, string height, string weight, int campaignId){
        model.name = name;
        model.type = type;
        model.race = race;
        model.age = age;
        model.gender = gender;
        model.height = height;
        model.weight = weight;
        model.campaignId = campaignId;
    }

	public CharacterBusinessObject(int id, string name, string type, string race, int age, string gender, string height, string weight, int campaignId){
        model.id = id;
        model.name = name;
		model.type = type;
		model.race = race;
		model.age = age;
		model.gender = gender;
		model.height = height;
		model.weight = weight;
        model.campaignId = campaignId;
	}
}
