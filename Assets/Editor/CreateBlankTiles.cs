using System.Collections;
using UnityEditor;
using UnityEngine;

public class CreateBlankTiles : EditorWindow{
	string xPos = "";
	string zPos = "";

	[MenuItem("Tools/Create Blank Tiles")]
	public static void ShowWindow(){
		EditorWindow.GetWindow(typeof(CreateBlankTiles));
	}

	void OnGUI(){
		GUILayout.Label ("X", EditorStyles.boldLabel);
		xPos = EditorGUILayout.TextField ("Text Field", xPos);

		GUILayout.Label ("Z", EditorStyles.boldLabel);
		zPos = EditorGUILayout.TextField ("Text Field", zPos);

		if(GUILayout.Button("Generate")){
			CreateTiles();
		}

		if(GUILayout.Button("Remove")){
			RemoveTiles();
		}
	}

	void CreateTiles(){
		//GameObject tile = (GameObject)Resources.Load("Tiles/Blank_Tile") as GameObject;
		//tile.transform.GetComponent<TileController>().tileData.name = "Blank_Tile";
		for(int i = 0; i < int.Parse(xPos); i++){
			for(int j = 0; j < int.Parse(zPos); j++){
				float x = i * .6f;
				float z = j * .6f;
				Vector3 pos = new Vector3(x, 0, z);
				Quaternion rot = Quaternion.Euler(-90,0,0);
				GameObject tile = PrefabUtility.InstantiatePrefab(Resources.Load("Tiles/Blank_Tile") as GameObject) as GameObject;
				tile.transform.position = pos;
				tile.transform.rotation = rot;
			}
		}
	}

	void RemoveTiles(){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("BlankTile");
		foreach(GameObject tile in tiles){
			DestroyImmediate(tile);
		}
	}
}