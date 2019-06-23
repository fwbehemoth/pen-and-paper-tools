using System;
using System.Collections.Generic;
using System.Text;
using CustomDataClasses;
using UnityEngine;
using Utils;

namespace CustomCollections {
    [Serializable]
    public class MapTilesHashCollection : Dictionary<string, TileController> {
        public MapTilesHashCollection(){

        }

        public MapTilesHashCollection(JSONArrayWrapper<MapTilesJSONData> mapTilesArray){
            createMapTilesHashCollection(mapTilesArray);
        }

        public void addTile(TileController tileController){
            this[createKey(tileController.tileBO.model)] = tileController;
        }

        public void removeTile(TileController tileController){
            this.Remove(createKey(tileController.tileBO.model));
        }

        public void updateTile(TileController tileController){
            this[createKey(tileController.tileBO.model)] = tileController;
        }

        public JSONArrayWrapper<MapTilesJSONData> createMapTilesJSONArray() {
            JSONArrayWrapper<MapTilesJSONData> mapTilesJSONArray = new JSONArrayWrapper<MapTilesJSONData>();
            foreach(TileController tileController in this.Values){
                StringBuilder stringBuilder = new StringBuilder();
                MapTilesJSONData mapTilesJSONData = new MapTilesJSONData();
                mapTilesJSONData.tileData = tileController.tileBO.model;
                stringBuilder.Append("Tile Name: " + tileController.tileBO.model.name);
                if (tileController.piece != null) {
                    mapTilesJSONData.pieceData = tileController.piece.pieceBO.model;
                    stringBuilder.Append(" Piece Name: " + tileController.piece.name);
                }
                mapTilesJSONArray.list.Add(mapTilesJSONData);
                Globals.Instance().DebugLog(this.GetType().Name, stringBuilder.ToString());
            }
            return  mapTilesJSONArray;
        }

        public void createMapTilesHashCollection(JSONArrayWrapper<MapTilesJSONData> mapTilesJSONArray){
            foreach (MapTilesJSONData mapTilesJSONData in mapTilesJSONArray.list) {
                StringBuilder stringBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(mapTilesJSONData.tileData.prefabName)) {
//                    GameObject tilePrefab = (GameObject)Resources.Load("Tiles/" + mapTilesJSONData.tileData.prefabName);
                    GameObject tilePrefab = new GameObject();
                    tilePrefab.AddComponent<TileController>();
                    TileController tileController = tilePrefab.GetComponent<TileController>();
                    TileBusinessObject tileBO = new TileBusinessObject();
                    tileBO.model = mapTilesJSONData.tileData;
                    tileController.tileBO = tileBO;
                    stringBuilder.Append("row: " + tileController.tileBO.model.rowIndex + " / " + "col: " + tileController.tileBO.model.columnIndex);
                    stringBuilder.Append(" - Tile name: " + tileController.tileBO.model.name);
//                    Globals.Instance().DebugLog(this.GetType().Name, "row: " + tileController.tileBO.model.rowIndex + " / " + "col: " + tileController.tileBO.model.columnIndex);
                    if (!string.IsNullOrEmpty(mapTilesJSONData.pieceData.prefabName)) {
                        GameObject piecePrefab = (GameObject)Resources.Load("Pieces/" + mapTilesJSONData.pieceData.prefabName);
                        PieceController pieceController = piecePrefab.GetComponent<PieceController>();
                        PieceBusinessObject pieceBO = new PieceBusinessObject();
                        pieceBO.model = mapTilesJSONData.pieceData;
                        pieceController.pieceBO = pieceBO;
                        tileController.piece = pieceController;
//                        Resources.UnloadAsset(piecePrefab);
//                        GameObject.Destroy(piecePrefab);
                        stringBuilder.Append(" - Piece name: " + tileController.piece.name);
                    }
                    this[createKey(tileBO.model)] = tileController;
                    Globals.Instance().DebugLog(this.GetType().Name, stringBuilder.ToString());
//                    Resources.UnloadAsset(tilePrefab);
//                    GameObject.Destroy(tilePrefab);
                }
            }
        }

        static public string createKey(TileData tileData){
            return  createKey(tileData.rowIndex, tileData.columnIndex);
        }

        static public string createKey(int x, int y){
            StringBuilder key = new StringBuilder();
            key.Append(x);
            key.Append(",");
            key.Append(y);
            return  key.ToString();
        }
    }
}