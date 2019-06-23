using System;
using System.Collections.Generic;
using CustomCollections;
using CustomDataClasses;
using Myth.BaseLib;
using UnityEngine;
using Utils;

namespace Myth.BusinessObjects {
    [Serializable]
	public class MapTilesBusinessObject: MythBussinessObject {
//		public MapTilesCollection collection = new MapTilesCollection();
		public MapTilesHashCollection collection = new MapTilesHashCollection();

        public MapTilesBusinessObject(){

        }

        public MapTilesBusinessObject(JSONArrayWrapper<MapTilesJSONData> mapTilesJSONArray){
            collection = new MapTilesHashCollection(mapTilesJSONArray);
        }

		public void Add(TileController tileController) {
//			if (!isOutOfRange(tileController)){
//				collection.addTile(tileController);
//			}
		}

		public void Remove(TileController tileController){
//			if (!isOutOfRange(tileController)){
//				collection.removeTile(tileController);
//			}
		}

		private bool isOutOfRange(TileController tileController){
			bool isOut = true;
//			if (collection.Count > tileController.tileBO.model.rowIndex) {
//				if(collection[tileController.tileBO.model.rowIndex].Count > tileController.tileBO.model.columnIndex) {
//					isOut = false;
//				} else {
//					Globals.Instance().DebugLog("Tile yIndex is bigger then array y range.");
//				}
//			} else {
//				Globals.Instance().DebugLog("Tile xIndex is bigger then array x range.");
//			}

			return isOut;
		}

        public JSONArrayWrapper<MapTilesJSONData> collectionToJSONArray(){
			Debug.Log("collectionToJSONArray");
            JSONArrayWrapper<MapTilesJSONData> mapTilesJSONArray = collection.createMapTilesJSONArray();
            return mapTilesJSONArray;
        }
	}
}