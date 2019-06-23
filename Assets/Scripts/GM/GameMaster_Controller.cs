using Myth.UI;
using UnityEngine;

public class GameMaster_Controller : Character_Controller {
	private Movement movement;
	private MapGUI interFace;
	private Dice dice;
	
	// Use this for initialization
	void Start () {
		if(Network.isServer){
			interFace = GameObject.Find("Engine").GetComponent<MapGUI>();
			interFace.cc = this;
			GameObject.Find("Main Camera").GetComponent<MouseOrbitImproved>().target = GameObject.Find("GMTarget").transform;
			Transform pos;
			for(int i = 0; i < 2; i++){
				Globals.Instance().DebugLog(this.GetType().Name, "Enemy num: " + i);
				GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
				foreach(GameObject tile in tiles){
					Globals.Instance().DebugLog(this.GetType().Name, "tile Object Type: " + tile.transform.GetComponent<TileController>().type);
					if(tile.transform.GetComponent<TileController>().type != "Object" && tile.transform.GetComponent<TileController>().type != "Player" && tile.transform.GetComponent<TileController>().type != "Enemy"){
						pos = tile.transform; 
						tile.transform.GetComponent<TileController>().type = "Enemy";
						GameObject enemy = Instantiate(Resources.Load("ECs/Enemy_Test") as GameObject, pos.position, pos.rotation) as GameObject;
						enemy.transform.GetComponent<Enemy_Controller>().charName = "Enemy " + (i + 1);
						enemy.transform.GetComponent<Enemy_Controller>().currentPos = pos;
						enemy.transform.GetComponent<Enemy_Controller>().startTile = tile;
						enemy.transform.GetComponent<Enemy_Nameplate>().newName = "Enemy "  + (i + 1);
						enemy.transform.GetComponent<Enemy_Nameplate>().PlayerName = "Enemy "  + (i + 1);
						enemy.transform.GetComponent<Enemy_Nameplate>().affiliation = "aggressive";
						tile.transform.GetComponent<TileController>().CharObj = enemy;
						
						break;
					}
				}
			}
			//Globals.Instance().SendEnemies();
		} else {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
