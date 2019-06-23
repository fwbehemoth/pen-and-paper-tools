using Myth.Utils;
using UnityEngine;

public class Networking : MonoBehaviour {
	public string IP = "127.0.0.1";
	public int Port = 25001;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitializeServer(){
		Network.InitializeServer(10, Port, false);
	}

	public void Connect(){
		Network.Connect(IP, Port);
	}

	void OnConnectedToServer() {
		Globals.Instance().DebugLog(this.GetType().Name, "isClient: " + Network.isClient + "\n" + "isServer: " + Network.isServer + "\n");
		Globals.Instance().DebugLog(this.GetType().Name, "Player Connected");
		GetComponent<NetworkView>().RPC("AddPlayer", RPCMode.Server, PlayerPrefs.GetString("Character_Name"));
	}
	
	void OnServerInitialized(){
		Globals.Instance().DebugLog(this.GetType().Name, "isClient: " + Network.isClient + "\n" + "isServer: " + Network.isServer + "\n");
		Globals.Instance().DebugLog(this.GetType().Name, "Initailized server");
	}
	
	public void SendEnemies(){
		Globals.Instance().DebugLog(this.GetType().Name, "Send Enemies");
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Globals.Instance().DebugLog(this.GetType().Name, "Num Enemies: " + enemies.Length);
		string pieces = "";
		string poses = "";
		string rots = "";
		foreach(GameObject enemy in enemies){
			pieces += "Enemy_Test$";
			poses += enemy.transform.position.ToString() + "$";
			rots += enemy.transform.rotation.ToString()  + "$";
		}
		GetComponent<NetworkView>().RPC ("AddEnemies", RPCMode.OthersBuffered, pieces, poses, rots);
	}

	[RPC]
	void AddEnemies(string pieces, string poses, string rots){
		Globals.Instance().DebugLog(this.GetType().Name, "Add Enemies");
		string[] piecesArr = pieces.Split("$"[0]);
		string[] posesArr = poses.Split("$"[0]);
		string[] rotsArr = rots.Split("$"[0]);
		for(int i = 0; i < pieces.Length; i++){
			Transform trans = new GameObject().transform;
			trans.position = Globals.Instance().Vector3FromString(posesArr[i]);
			trans.rotation = Globals.Instance().QuaternionFromString(rotsArr[i]);
			GameObject enemy = Instantiate(Resources.Load("ECs/" + piecesArr[i]) as GameObject, trans.position, trans.rotation) as GameObject;
			enemy.transform.GetComponent<Enemy_Controller>().charName = "Enemy " + (i + 1);
			enemy.transform.GetComponent<Enemy_Controller>().currentPos = trans;
			enemy.transform.GetComponent<Enemy_Nameplate>().newName = "Enemy " + (i + 1);
			enemy.transform.GetComponent<Enemy_Nameplate>().PlayerName = "Enemy " + (i + 1);
			enemy.transform.GetComponent<Enemy_Nameplate>().affiliation = "aggressive";
			FindObjectsInScene.findTileWithPosition(enemy.transform.position).GetComponent<TileController>().CharObj = enemy;
		}
	}
	
	[RPC]
	void AddPlayer(string cName){
		Globals.Instance().DebugLog(this.GetType().Name, "Add Player: " + cName);
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		if(Network.isServer){
			//Instantiate Player
			GameObject start = null;
			foreach(GameObject tile in tiles){
				if(tile.transform.GetComponent<TileController>().type != "Object" && tile.transform.GetComponent<TileController>().type != "Player" && tile.transform.GetComponent<TileController>().type != "Enemy"){
					start = tile;
					GameObject pc = Instantiate(Resources.Load("PCs/Player_Test") as GameObject, tile.transform.position, tile.transform.rotation) as GameObject;
					pc.transform.GetComponent<Player_Controller>().charName = cName;
					pc.transform.GetComponent<Player_Controller>().currentPos = tile.transform;
					pc.transform.GetComponent<Player_Nameplate>().newName = cName;
					pc.transform.GetComponent<Player_Nameplate>().PlayerName = cName;
					pc.transform.GetComponent<Player_Nameplate>().affiliation = "friendly";
					tile.GetComponent<TileController>().CharObj = pc;
					break;
				}
			}
			GetComponent<NetworkView>().RPC("StartPlayer", RPCMode.OthersBuffered, cName, start.transform.position, start.transform.rotation);
		}
	}
	
	[RPC]
	void StartPlayer(string newName, Vector3 pos, Quaternion rot){
		Globals.Instance().DebugLog(this.GetType().Name, "Start Player: " + newName);
		Transform trans = new GameObject().transform;
		trans.position = pos;
		trans.rotation = rot;
		GameObject pc = Resources.Load("PC/Player_Test") as GameObject;
		pc.transform.GetComponent<Player_Controller>().charName = newName;
		pc.transform.GetComponent<Player_Controller>().currentPos = trans;
		pc.transform.GetComponent<Player_Nameplate>().newName = newName;
		FindObjectsInScene.findTileWithPosition(trans.position).GetComponent<TileController>().CharObj = pc;
		Instantiate(pc, pos, rot);
	}  
}
