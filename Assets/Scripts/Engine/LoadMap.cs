using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomCollections;
using Myth.BaseLib;

public class LoadMap : MonoBehaviour {

    // Use this for initialization
    void Start () {
        Globals.Instance ().userBO.model.mapBO.fetch();
    }

    // Update is called once per frame
    void Update () {

    }

    public void createMap(MapTilesHashCollection mapTilesHashCollection) {
        Globals.Instance().DebugLog(this.GetType().Name, "create map");
        Globals.Instance().userBO.model.mapBO.mapTilesBO.collection = mapTilesHashCollection;

        foreach (TileController tileController in mapTilesHashCollection.Values){
            tileController.instantiateTile();
        }
    }
}