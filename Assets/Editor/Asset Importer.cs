using System.Collections;
using UnityEditor;
using UnityEngine;

public class AssetImporter : AssetPostprocessor {
	void OnPostProcessModel(GameObject g){
		Apply(g.transform);
	}

	void Apply(Transform transform){
		Debug.Log ("Model: " + transform.name);
	}
}
