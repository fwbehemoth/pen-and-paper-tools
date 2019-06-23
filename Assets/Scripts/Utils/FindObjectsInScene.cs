using System;
using UnityEngine;

namespace Myth.Utils {
	public class FindObjectsInScene {
		public static GameObject findTileWithPosition(Vector3 pos){
			return findObjectWithCenterPoint(pos, "Tile");
		}

		public static GameObject findBlankTileWithPosition(Vector3 pos){
			return findObjectWithPosition(pos, "BlankTile");
		}

		public static GameObject findPlaneWithPosition(Vector3 pos){
			return findObjectWithCenterPoint(pos, "Plane");
		}

		public static GameObject findPieceWithPosition(Vector3 pos){
			return findObjectWithPosition(pos, "Piece");
		}

		private static GameObject findObjectWithPosition(Vector3 pos, string tag){
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
			foreach(GameObject gameObject in gameObjects){
				if(gameObject.transform.position.x == pos.x && gameObject.transform.position.y == pos.y && gameObject.transform.position.z == pos.z){
					return gameObject;
				}
			}
			return null;
		}

		private static GameObject findObjectWithCenterPoint(Vector3 pos, string tag){
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
			foreach(GameObject gameObject in gameObjects){
				TileController tileController = gameObject.GetComponent<TileController>();
				if(tileController.tileBO.model.centerX == pos.x && tileController.tileBO.model.centerY == pos.y && gameObject.transform.position.z == pos.z){
					return gameObject;
				}
			}
			return null;
		}
	}
}