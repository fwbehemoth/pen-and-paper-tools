using System;
using System.Collections.Generic;
using CustomCollections;
using CustomDataClasses;
using Enums;
using LitJson;
using Myth.BaseLib;
using Myth.BusinessObjects;
using Myth.CustomCollections;
using UnityEngine;
using Utils;

[Serializable]
public class MapBusinessObject:MythBussinessObject {
    public MapData model = new MapData();
    public MapTilesBusinessObject mapTilesBO = new MapTilesBusinessObject();
    public JSONArrayWrapper<MapTilesJSONData> mapTilesJSONArray;

	public MapBusinessObject(int id, string name){
		model.id = id;
		model.name = name;
	}

    public MapBusinessObject(string name){
        model.name = name;
    }

    public void createMapTilesJSONArray(){

    }

    private void parse(JsonData json) {
        foreach(Dictionary<string, Dictionary<string, string>> tileDict in json) {
            Debug.Log(this.GetType().Name + " - TileDict: " + tileDict.ToString());
        }
    }

    public void save(int rows, int columns) {
        if(model.id == 0){
            Debug.Log(this.GetType().Name + " - userid=" + Globals.Instance().userBO.model.id + "&campaignid=" + Globals.Instance().userBO.model.campaignBO.model.id + "&mapName=" + model.name);
            Globals.Instance().api.apiCall("CreateMap.php?userid=" + Globals.Instance().userBO.model.id + "&campaignid=" + Globals.Instance().userBO.model.campaignBO.model.id + "&mapName=" + model.name, RequestMethodTypeEnum.Post, createSuccess, createFailure);
        } else {
            mapTilesJSONArray = mapTilesBO.collectionToJSONArray();
            mapTilesJSONArray.rows = rows;
            mapTilesJSONArray.columns = columns;
            string jsonString = JsonUtility.ToJson(mapTilesJSONArray);
            Debug.Log(this.GetType().Name + " - JSON String: " + jsonString);
//            string encryptedStr = Globals.Instance().fileManager.encrypt(jsonString);
            string encryptedStr = jsonString;
            Globals.Instance().fileManager.saveToFile("maps/" + Globals.Instance().userBO.model.id + "_" + Globals.Instance().userBO.model.campaignBO.model.id + "_" + model.id + ".map", encryptedStr);
//            saveToDB(jsonString);
        }
    }

    private void saveToDB(string data) {
        Globals.Instance().api.apiCall("SaveMapData.php", data, RequestMethodTypeEnum.Post, serverSuccess, serverFailure);
    }

    //Create
    protected void createSuccess(JsonData json) {
        Debug.Log(this.GetType().Name + " - Create Success: " + json.ToString());
    }

    protected void createFailure(string error){
        Debug.Log(this.GetType().Name + " - Create Failure: " + error);
    }

    //Fetch
	override
    public void fetch() {
        Debug.Log(this.GetType().Name + " - Fetch");
        fetchFromFile();
    }

    public void fetchFromFile() {
        Globals.Instance().fileManager.loadLocalFile("maps/" + Globals.Instance().userBO.model.id + "_" + Globals.Instance().userBO.model.campaignBO.model.id + "_" + model.id + ".map", fileSuccess, fileFailure);
    }

    public void fetchFromServer() {
        Globals.Instance().api.apiCall("FetchMapData.php?id=" + model.id, RequestMethodTypeEnum.Get, serverSuccess, serverFailure);
    }

    //Failure Callbacks
    protected void serverFailure(string error) {
        Globals.Instance().DebugLog(this.GetType().Name, "Server LOAD MAP FAILURE: " + error);
//        Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.setCurrentCounts(MapEditorConstants.EDIT_GRID_DEFAULT_ROW, MapEditorConstants.EDIT_GRID_DEFAULT_COLUMN);
        GameObject.Find("Engine").SendMessage("createMap", new MapTilesCollection());
    }

    protected void fileFailure(string error) {
        Debug.Log(this.GetType().Name + " - File LOAD MAP FAILURE: " + error);
        fetchFromServer();
    }

    //Success Callbacks
    protected void serverSuccess(JsonData json) {
        Debug.Log("Server LOAD MAP: " + json.ToString());
        if(json["success"].Equals("false")){
            JSONArrayWrapper<MapTilesJSONData> mapTilesArray = JsonUtility.FromJson<JSONArrayWrapper<MapTilesJSONData>>(json.ToString());
            mapTilesBO.collection = new MapTilesHashCollection(mapTilesArray);
        } else {
            Debug.Log(this.GetType().Name + " - No Map found on SERVER");
            GameObject.Find("Engine").SendMessage("createMap", new MapTilesHashCollection());
        }
    }

    public void fileSuccess(string text) {
//        string map = Globals.Instance().fileManager.decrypt(text);
        string map = text;
        Debug.Log(this.GetType().Name + " - File LOAD MAP Success: " + map);
        JSONArrayWrapper<MapTilesJSONData> mapTilesArray = JsonUtility.FromJson<JSONArrayWrapper<MapTilesJSONData>>(map);
//        mapTilesBO.collection = new MapTilesCollection(mapTilesArray);
        GameObject.Find("Engine").SendMessage("createMap", new MapTilesHashCollection(mapTilesArray));
    }
}
