using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {
	private Character_Controller cc;

	private Transform currentPos;
	private ArrayList selectedTiles = new ArrayList();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl==0) {
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    	if (Physics.Raycast(ray, out hit)){
				if(hit.transform.tag == "Tile"){
					if(hit.transform != cc.startTile.transform && hit.transform.GetComponent<TileController>().type == "movement"){
						//globals.DebugLog ("Tile count: " + selectedTiles.Count);
						if(hit.transform.GetComponent<TileController>().Clicked){
							if( hit.transform == (Transform)selectedTiles[cc.selectedTilePos]){
								hit.transform.GetComponent<TileController>().Clicked = false;
								selectedTiles.Remove(hit.transform);
								cc.selectedTilePos--;
								if(selectedTiles.Count != 0){
									currentPos = (Transform)selectedTiles[cc.selectedTilePos];
								} else {
									currentPos = cc.startTile.transform;
								}
							}
						} else {
							if(isAdjacent(hit.transform) && (cc.selectedTilePos < cc.maxTiles)){
								cc.selectedTilePos++;
								currentPos = hit.transform;
								selectedTiles.Add(hit.transform);
								hit.transform.GetComponent<TileController>().Clicked = true;
							}
						}
					}
				}
			}
		}
		
		if(cc.move){
			for(int i = 0; i < selectedTiles.Count; i++){
				Transform pos = (Transform)selectedTiles[i];
				cc.transform.position = pos.position;
				pos.GetComponent<TileController>().CharObj = null;
			}
			cc.move = false;
			cc.startTile = currentPos.gameObject;
			selectedTiles = new ArrayList();
			selectedTiles.Add(cc.startTile.transform);
			resetTiles();
			cc.startTile.transform.GetComponent<TileController>().Clicked = false;
			cc.startTile.transform.GetComponent<TileController>().CharObj = cc.figure;
			cc.selectedTilePos = 0;
		}
	}
	
	public void SetupMovement(Character_Controller charCont){
		cc = charCont;
		selectedTiles.Add(cc.startTile.transform);
		currentPos = cc.startTile.transform;
	}
	
	public void startMovement(){
		currentPos = cc.startTile.transform;
	}
	
	void resetTiles(){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		foreach(GameObject tile in tiles){
			tile.transform.GetComponent<TileController>().Clicked = false;
		}
	}
	
	bool isAdjacent(Transform pos){
		bool isNext = false;
		float figureXLT = currentPos.transform.position.x - (cc.startTile.transform.GetComponent<Collider>().bounds.size.x);
		float figureXRT = currentPos.transform.position.x + (cc.startTile.transform.GetComponent<Collider>().bounds.size.x);
		float figureZUP = currentPos.transform.position.z + (cc.startTile.transform.GetComponent<Collider>().bounds.size.z);
		float figureZDN = currentPos.transform.position.z - (cc.startTile.transform.GetComponent<Collider>().bounds.size.z);
		
		if(Mathf.Approximately(pos.position.x, figureXLT) && Mathf.Approximately(pos.position.z, figureZUP)){
			if(!(isWall(figureXLT, currentPos.transform.position.z) || isWall(currentPos.transform.position.x, figureZUP))){
				isNext = true;
			}
		}
		
		if(Mathf.Approximately(pos.position.x, figureXLT) && Mathf.Approximately(pos.position.z, figureZDN)){
			if(!(isWall(figureXLT, currentPos.transform.position.z) || isWall(currentPos.transform.position.x, figureZDN))){
				isNext = true;
			}
		}
		
		if(Mathf.Approximately(pos.position.x, figureXRT) && Mathf.Approximately(pos.position.z, figureZUP)){
			if(!(isWall(figureXRT, currentPos.transform.position.z) || isWall(currentPos.transform.position.x, figureZUP))){
				isNext = true;
			}
		}
		
		if(Mathf.Approximately(pos.position.x, figureXRT) && Mathf.Approximately(pos.position.z, figureZDN)){
			if(!(isWall(figureXRT, currentPos.transform.position.z) || isWall(currentPos.transform.position.x, figureZDN))){
				isNext = true;
			}
		}
		
		if(Mathf.Approximately(pos.position.x, figureXLT) && Mathf.Approximately(pos.position.z, currentPos.transform.position.z)){
			isNext = true;
			
		}
		
		if(Mathf.Approximately(pos.position.x, figureXRT) && Mathf.Approximately(pos.position.z, currentPos.transform.position.z)){	

			isNext = true;
		}
		
		if(Mathf.Approximately(pos.position.z, figureZUP) && Mathf.Approximately(pos.position.x, currentPos.transform.position.x)){
			isNext = true;
		}
		
		if(Mathf.Approximately(pos.position.z, figureZDN) && Mathf.Approximately(pos.position.x, currentPos.transform.position.x)){
			isNext = true;
		}
		
		return isNext;
	}
	
	bool isWall(float diagTileX, float diagTileZ){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		foreach(GameObject tile in tiles){
			if(tile.transform.GetComponent<TileController>().type == "wall" && Mathf.Approximately(tile.transform.position.x, diagTileX) && Mathf.Approximately(tile.transform.position.z, diagTileZ)){
				return true;
			}
		}
		return false;
	}
}
