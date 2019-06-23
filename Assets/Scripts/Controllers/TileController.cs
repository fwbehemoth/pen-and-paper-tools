using Constants;
using Myth.BaseLib;
using Myth.Utils;
using UnityEngine;

[System.Serializable]
public class TileController : MonoBehaviour {
	public bool Clicked = false;
	public string type;
	public GameObject CharObj = null;
	public TileBusinessObject tileBO = new TileBusinessObject();
	public PieceController piece = null;

    public TileController (TileData tileData){
        tileBO.model = tileData;
    }

	// Use this for initialization
	void Start () {
		//checkType();

	}
	
	// Update is called once per frame
	void Update () {
		//checkType();
		/*if(Clicked){
			if(Network.isClient){
				transform.renderer.material = highlight;
			} else {
				transform.renderer.material = Resources.Load("Materials/Red")  as Material;
			}
		} else {
			if(CharObj){ 
				if(type == "Player"){
					transform.renderer.material = Resources.Load("Materials/Yellow") as Material;
				} else if (type == "Enemy"){
					transform.renderer.material = Resources.Load("Materials/Red")  as Material;
				} else if(type == "NPC"){
					transform.renderer.material = Resources.Load("Materials/Blue")  as Material;
				} 
			} else {
				transform.renderer.material = normal;
			}
		}*/
		RaycastHit hit = new RaycastHit();
//		Debug.DrawRay (new Vector3(planeData.centerX, planeData.centerY), this.transform.forward*-1, Color.cyan);
		if(this.transform.tag != GameObjectTags.PLANE_TAG){
			if(Physics.Raycast(new Vector3(tileBO.model.centerX, tileBO.model.centerY), TransformUtils.backwards(this.transform), out hit, 1)){
				if(hit.transform.tag == GameObjectTags.PIECE_TAG && piece == null){
//					Debug.Log("TileController-Has Piece: " + this.transform.name);
					piece = hit.transform.GetComponent<PieceController>();
					piece.GetComponent<PieceController>().totTiles++;
				}
			} else {
				if(piece != null){
//					Debug.Log("TileController-No Piece: " + piece.tag);
					piece.transform.GetComponent<PieceController>().totTiles--;
					piece = null;
				}
			}
		}
	}
	
	public void checkType(){
		/*if(CharObj){
			if(CharObj.transform.tag == "Object"){
				type = "Object";
			} else if(CharObj.transform.tag == "Enemy") {
				type = "Enemy";
			} else if(CharObj.transform.tag == "Player") {
				type = "Player";
			} else if(CharObj.transform.tag == "NPC") {
				type = "NPC";
			} else {
				type = "Movement";
			}
		} else {
			type = "Movement";
		}*/
	}

	public void destroyTile(){
		Globals.Instance().DebugLog(this.GetType().Name, "Destroy Tile");
		Destroy(this.gameObject);
	}

    public void instantiateTile(){
        instantiateTile(tileBO.model.prefabName);
    }

    public void instantiateTile(string prefabName){
        GameObject prefab = (GameObject)Resources.Load("Tiles/" + prefabName);
        Material material = prefab.transform.GetComponent<Renderer>().sharedMaterial;
        instantiateTile(material);
    }

	public void instantiateTile(Material material){
//        Debug.Log("instantiateTile - Row: " + tileBO.model.rowIndex + " / Column: " + tileBO.model.columnIndex);
        GameObject tile = MythSquarePlane.CreatePlane(GameObjectUtils.getTileBoundsSize(), tileBO.model.rowIndex, tileBO.model.columnIndex, material, true);
        TileController tileController = tile.GetComponent<TileController>();
        tile.tag = GameObjectTags.TILE_TAG;
        tileController = this;
        if(this.piece != null) {
			this.piece.instantiatePiece(new Vector3(tileBO.model.centerX, tileBO.model.centerY, 0), this.piece.transform.rotation);
		}
//        tileBO = tile.GetComponent<TileController>().tileBO;
        GameObject.Instantiate(tile);
	}

	public void updateNamesAndID(TileData tileData){
		tileBO.updateNamesAndID(tileData);
	}

	/*void OnTriggerEnter(Collider trigger){
		Globals.Instance().DebugLog("Trigger Enter");
	}
	
	void OnTriggerExit(){
		Globals.Instance().DebugLog("Trigger Exit");
	}
	
	void OnCollisionEnter(Collision trigger){
		Globals.Instance().DebugLog("Collision");
	}
	
	void OnCollisionExit(){

	}*/
}
