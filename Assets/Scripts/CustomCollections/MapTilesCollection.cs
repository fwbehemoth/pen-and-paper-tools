using System;
using System.Collections.Generic;
using System.Text;
using CustomDataClasses;
using UnityEngine;
using Utils;

namespace Myth.CustomCollections {
    [Serializable]
	public class MapTilesCollection : List<List<TileController>> {
        private int currentRowCount = 0;
        private int currentColumnCount = 0;
        private int maxRowCount = 0;
        private int maxColumnCount = 0;
//        private Dictionary<String, TileController> tilesDict = new Dictionary<string, TileController>();

        public MapTilesCollection() {

        }

        public MapTilesCollection(JSONArrayWrapper<MapTilesJSONData> mapTilesArray) {
            createMapTilesFromJSONArray(mapTilesArray);
        }

        private void setCurrentRowCount(int rowCount) {
            currentRowCount = rowCount;
            if (isGreaterThanMaxRowCount(rowCount)) {
                maxRowCount = rowCount;
            }
        }

        public int getCurrentRowCount() {
            return currentRowCount;
        }

        private void setCurrentColumnCount(int columnCount) {
            currentColumnCount = columnCount;
            if (isGreaterThanMaxColumnCount(columnCount)) {
                maxColumnCount = columnCount;
            }
        }

        public int getCurrentColumnCount() {
            return currentColumnCount;
        }

        public void setCurrentCounts(int rowCount, int columnCount) {
            setCurrentRowCount(rowCount);
            setCurrentColumnCount(columnCount);
            updateCollectionSize();
        }

        public void setMaxRowCount(int rowCount) {
            maxRowCount = rowCount;
        }

        public int getMaxRowCount() {
            return maxRowCount;
        }

        public void setMaxColumnCount(int columnCount) {
            maxColumnCount = columnCount;
        }

        public int getMaxColumnCount() {
            return maxColumnCount;
        }

        public bool isMaxRowCount() {
            return currentRowCount == maxRowCount;
        }

        public bool isMaxColumnCount() {
            return currentColumnCount == maxColumnCount;
        }

        public bool isGreaterThanMaxRowCount(int rowCount) {
            return rowCount > maxRowCount;
        }

        public bool isGreaterThanMaxColumnCount(int columnCount) {
            return columnCount > maxColumnCount;
        }

        public void addTile(TileController tileController) {
            if (tileController.tileBO.model.rowIndex + 1 >= currentRowCount || tileController.tileBO.model.columnIndex + 1 >= currentColumnCount) {
                setCurrentCounts(tileController.tileBO.model.rowIndex + 1, tileController.tileBO.model.columnIndex + 1);
            }
            this[tileController.tileBO.model.rowIndex].Insert(tileController.tileBO.model.columnIndex, tileController);
        }

        private void updateCollectionSize() {
            for (int x = this.Count; x < maxRowCount; x++) {
                this.Add(new List<TileController>());
            }

            for (int x = 0; x < maxRowCount; x++) {
                for (int y = this[x].Count; y < maxColumnCount; y++) {
                    GameObject obj = (GameObject)Resources.Load("Empty_Objects/Empty_Object");
                    TileController tileController = obj.GetComponent<TileController>();
                    this[x].Add(tileController);
                }
            }
        }

        public void removeTile(TileController tileController) {

        }

        public JSONArrayWrapper<MapTilesJSONData> createMapTilesJSONArray() {
            StringBuilder stringBuilder = new StringBuilder();
            JSONArrayWrapper<MapTilesJSONData> mapTilesJSONArray = new JSONArrayWrapper<MapTilesJSONData>();
            mapTilesJSONArray.rows = currentRowCount;
            mapTilesJSONArray.columns = currentColumnCount;
            for (int x = 0; x < currentRowCount; x++) {
                for (int y = 0; y < currentColumnCount; y++) {
                MapTilesJSONData mapTilesJSONData = new MapTilesJSONData();
                    mapTilesJSONData.tileData = this[x][y].tileBO.model;
                    stringBuilder.Append("Tile Name: " + this[x][y].tileBO.model.name);
                    if (this[x][y].piece != null) {
                        mapTilesJSONData.pieceData = this[x][y].piece.pieceBO.model;
                        stringBuilder.Append("Piece Name: " + this[x][y].piece.name);
                    }
                    mapTilesJSONArray.list.Add(mapTilesJSONData);
                    if(x == 9 && y == 9){
                        int i = 0;
                    }
                    Globals.Instance().DebugLog(this.GetType().Name, stringBuilder.ToString());
                }
            }
            return  mapTilesJSONArray;
        }

        public void createMapTilesFromJSONArray(JSONArrayWrapper<MapTilesJSONData> mapTilesJSONArray) {
            setCurrentCounts(mapTilesJSONArray.rows, mapTilesJSONArray.columns);
            for (int x = 0; x < mapTilesJSONArray.rows; x++) {
                for (int y = 0; y < mapTilesJSONArray.columns; y++) {
                    GameObject blankPrefab = new GameObject();
                    blankPrefab.AddComponent<TileController>();
                    TileController blankController = blankPrefab.GetComponent<TileController>();
                    TileBusinessObject tileBO = new TileBusinessObject();
                    tileBO.model.rowIndex = x;
                    tileBO.model.columnIndex = y;
                    blankController.tileBO = tileBO;
                    addTile(blankController);
                    GameObject.Destroy(blankPrefab);
                }
            }

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
                    addTile(tileController);
                    Globals.Instance().DebugLog(this.GetType().Name, stringBuilder.ToString());
//                    Resources.UnloadAsset(tilePrefab);
//                    GameObject.Destroy(tilePrefab);
                }
            }
        }
    }
}