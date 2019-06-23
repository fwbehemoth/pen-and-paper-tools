using System.Collections;
using UnityEngine;

public class Player_Controller : Character_Controller {
	//public int maxTiles = 6;
	//public int selectedTilePos = 0;
	//public bool move = false;
	//public Transform currentPos;
	//private Movement movement;
	//private Interface interFace;
	//private Dice dice;
	//private GameObject engine;
	
	//public string charName;
	
	// Use this for initialization
	void Start () {
		/*startTile =  currentPos.gameObject;
		figure = this.gameObject;
		engine = GameObject.Find("Engine");
		movement = this.gameObject.GetComponent<Movement>();
		movement.SetupMovement(this);
		interFace = GameObject.Find("Engine").GetComponent<Interface>();
		interFace.cc = this;
		dice = GameObject.Find("Engine").GetComponent<Dice>();
		GameObject.Find("Main Camera").GetComponent<MouseOrbitImproved>().target = this.gameObject.transform;*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Clone(Player_Controller player){
		/*charName = player.charName;
		maxTiles = player.maxTiles;
		currentPos = player.currentPos;*/
	}
}
