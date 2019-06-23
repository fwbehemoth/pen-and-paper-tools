using Myth.Utils;
using UnityEngine;

[System.Serializable]
public class PieceController : MonoBehaviour {
	public PieceBusinessObject pieceBO = new PieceBusinessObject();
	public int totTiles = 0;

	// Use this for initialization
	public PieceController(PieceData pieceData){
		pieceBO.model = pieceData;
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		foreach(Transform dummy in this.transform){
			RaycastHit[] hits = Physics.RaycastAll(dummy.position, Vector3.down, 1);
			foreach(RaycastHit hit in hits){
				if(hit.transform.tag == "Tile"){
					//Globals.Instance().DebugLog("Hit name: " + hit.transform.name);
				}
			}
			/*if(Physics.Raycast(dummy.position, Vector3.down, out hit, 1, 10)){
				Globals.Instance().DebugLog("Hit name: " + hit.transform.name);
				//Debug.DrawRay(dummy.position, Vector3.down, Color.red);
			}*/
		}
	}

	public void destroyPiece(){
		Globals.Instance().DebugLog(this.GetType().Name, "Destroy Piece");
		GameObject tile = FindObjectsInScene.findTileWithPosition(this.transform.position);
		tile.GetComponent<TileController>().piece = null;
		Destroy(this.gameObject);
	}

    public void instantiatePiece(Vector3 position, Quaternion rotation){
        GameObject prefab = (GameObject)Resources.Load("Pieces/" + pieceBO.model.prefabName);
        Instantiate(prefab, position, rotation);
    }

    public void instantiatePiece(PieceData pieceData, GameObject prefab, Vector3 position, Quaternion rotation){
		pieceBO.updateData(pieceData);
        Instantiate(prefab, position, rotation);
    }

    public void instatiatePiece(Transform placement){
        GameObject prefab = (GameObject)Resources.Load("Pieces/" + pieceBO.model.prefabName);
        Instantiate(prefab, placement.position, placement.rotation);
    }

	public void setToTiles(){

	}
	
}
