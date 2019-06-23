using System.Collections.Generic;
using Constants;
using CustomCollections;
using Myth.BaseLib;
using Myth.Constants;
using Myth.CustomCollections;
using Myth.Engine;
using Myth.UI;
using Myth.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EditMap : MonoBehaviour {
	private MythAPI api;
	private MapEditorGUI editGUI;
	private TileSetsBusinessObject tileSetsBO = new TileSetsBusinessObject();
	private GameObject moveSelection;
	private bool isMoveSelect = false;
	private Color origColor;
    private EditGrid grid;
    private float tileSize;
    private static Material gridMaterial;
    private int originalNumRows = 0;
    private int originalNumCols = 0;

	void Awake(){
        tileSetsBO.fetch();
        Globals.Instance().userBO.model.tileSetsBO = tileSetsBO;

		api = GameObject.Find("Globals").GetComponent<MythAPI>();
		editGUI = GameObject.Find("Canvas").GetComponentInChildren<MapEditorGUI>();
        grid =  GameObject.Find("Main Camera").GetComponent<EditGrid>();

		gridMaterial = Resources.Load<Material>("Materials/Gray");
        tileSize = GameObjectUtils.getTileBoundsSize();
	}

	// Use this for initialization
	void Start () {
        if (originalNumCols != grid.numCols || originalNumRows != grid.numRows) {
            Globals.Instance().removeAllPlanes();
            originalNumRows = grid.numRows;
            originalNumCols = grid.numCols;
        }
        Globals.Instance ().userBO.model.mapBO.fetch();
	}
	
	// Update is called once per frame
	void Update () {
		if(editGUI.enabled){
			if(editGUI.mode == MapEditorGUI.ModeType.PAINT){
				if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0) {
                    createInteraction();
				} else {
					if(moveSelection != null){
						moveSelection.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", origColor);
						moveSelection = null;
						isMoveSelect = false;
					}
				}
			} else if(editGUI.mode == MapEditorGUI.ModeType.SELECT){
				if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0) { 
					createInteraction();
				}
			}
		}
	}

	public void createMap(MapTilesHashCollection mapTilesHashCollection){
        Globals.Instance().DebugLog(this.GetType().Name, "create map");
        Globals.Instance().userBO.model.mapBO.mapTilesBO.collection = mapTilesHashCollection;
		bool hasMap = (Globals.Instance().userBO.model.mapBO.mapTilesBO.collection != null || Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.Count > 0);
		for (int x = 0; x < grid.numRows; x++) {
			for (int y = 0; y < grid.numCols; y++) {
				string key = MapTilesHashCollection.createKey(x, y);
				if(hasMap && mapTilesHashCollection.ContainsKey(key)){
					Globals.Instance().userBO.model.mapBO.mapTilesBO.collection[key].instantiateTile();
				} else {
					GameObject plane = MythSquarePlane.CreatePlane(tileSize, x, y, gridMaterial, true);
					Globals.Instance().userBO.model.mapBO.mapTilesBO.Add(plane.GetComponent<TileController>());
				}
			}
		}
//        } else {
//            sendRefreshMapSize(rowCount,columnCount);
			/// Use this for the play map
//			foreach (TileController tileController in mapTilesHashCollection.Values){
//				string key = MapTilesHashCollection.createKey(tileController.tileBO.model);
//				if (string.IsNullOrEmpty(Globals.Instance().userBO.model.mapBO.mapTilesBO.collection[key].tileBO.model.prefabName)) {
//					GameObject plane = MythSquarePlane.CreatePlane(tileSize, tileController.tileBO.model.rowIndex, tileController.tileBO.model.columnIndex, gridMaterial, true);
//					Globals.Instance().userBO.model.mapBO.mapTilesBO.Add(plane.GetComponent<TileController>());
//				} else {
//					tileController.instantiateTile();
//				}
//			}
//		}
	}

    private void sendRefreshMapSize(int rows, int columns){
        Dictionary<string, int> counts = new Dictionary<string, int>();
        counts.Add("row", rows);
        counts.Add("column", columns);
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.Find("Edit Map").SendMessage("refreshMapSize", counts);
    }

	void createInteraction(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		MapTilesHashCollection tilesCollection = Globals.Instance().userBO.model.mapBO.mapTilesBO.collection;
		if (Physics.Raycast(ray, out hit)){
            Vector3 gridCoord = grid.WorldToGridCoordinates(hit.point);
//*************DELETE
			if(editGUI.tool == MapEditorGUI.ToolType.DELETE){
				if(hit.transform.tag == GameObjectTags.TILE_TAG){
					GameObject tile = hit.transform.gameObject;
                    TileController tileController = tile.GetComponent<TileController>();
					tilesCollection.removeTile(tileController);
					if(hit.transform.GetComponent<TileController>().piece != null){
                        hit.transform.GetComponent<TileController>().piece.destroyPiece();
					}
					Material material = MaterialUtil.CreateGridMaterial(Colors.gridColor);
                    tile.GetComponent<Renderer>().material = material;
					tile.tag = GameObjectTags.PLANE_TAG;
                    TileData tileData = tileController.tileBO.model;
                    tileData.id = 0;
                    tileData.name = "";
                    tileData.prefabName = "";
				} else if(hit.transform.tag == GameObjectTags.PIECE_TAG){
					hit.transform.GetComponent<PieceController>().destroyPiece();
				}
//*****************ADD
			} else if(editGUI.tool == MapEditorGUI.ToolType.ADD){
				if(hit.transform.tag == GameObjectTags.PLANE_TAG){
					GameObject plane = hit.transform.gameObject;
					if(editGUI.selection.transform.tag == GameObjectTags.TILE_TAG){
                        TileController tileController = plane.GetComponent<TileController>();
                        TileData selectionData = editGUI.selection.transform.GetComponent<TileController>().tileBO.model;
						tileController.updateNamesAndID(selectionData);
						plane.tag = GameObjectTags.TILE_TAG;
                        Material material = editGUI.selection.transform.GetComponent<Renderer>().sharedMaterial;
						plane.GetComponent<Renderer>().material = material;
						tilesCollection.addTile(tileController);
						Globals.Instance().DebugLog(this.GetType().Name, "Add: Tile");
					} else {
						Globals.Instance().DebugLog(this.GetType().Name, "Need to add a Tile first");
					}
				} else if(hit.transform.tag == GameObjectTags.TILE_TAG){
					GameObject tile = hit.transform.gameObject;
					TileController tileController = tile.GetComponent<TileController>();
					if(editGUI.selection.transform.tag == GameObjectTags.TILE_TAG){
                        TileData selectionData = editGUI.selection.transform.GetComponent<TileController>().tileBO.model;
						tileController.updateNamesAndID(selectionData);
						Material material = editGUI.selection.transform.GetComponent<Renderer>().sharedMaterial;
						tile.GetComponent<Renderer>().sharedMaterial = material;
						tilesCollection.addTile(tileController);
						Globals.Instance().DebugLog(this.GetType().Name, "Replace: Tile");
					} else if(editGUI.selection.transform.tag == GameObjectTags.PIECE_TAG){
						Vector3 centerPoint = new Vector3(tileController.tileBO.model.centerX, tileController.tileBO.model.centerY, 0);
						if(hit.transform.GetComponent<TileController>().piece == null){
							tileController.piece = editGUI.selection.transform.gameObject.GetComponent<PieceController>();
							tileController.piece.instantiatePiece(tileController.piece.pieceBO.model, editGUI.selection, centerPoint, editGUI.selection.transform.rotation);
							tilesCollection.addTile(tileController);
							Globals.Instance().DebugLog(this.GetType().Name, "Add: Piece");
						} else {
							if(!tileController.piece.pieceBO.model.prefabName.Equals(editGUI.selection.transform.GetComponent<PieceController>().pieceBO.model.prefabName)){
								tileController.piece.destroyPiece();
								tileController.piece.instantiatePiece(tileController.piece.pieceBO.model, editGUI.selection, centerPoint, editGUI.selection.transform.rotation);
								tilesCollection.addTile(tileController);
								Globals.Instance().DebugLog(this.GetType().Name, "Replace: Piece");
							}
						}
					}
				} else if(hit.transform.tag == GameObjectTags.PIECE_TAG){
					GameObject tile = FindObjectsInScene.findTileWithPosition(hit.transform.position);
                    TileController tileController = tile.transform.GetComponent<TileController>();
                    Vector3 centerPoint = new Vector3(tileController.tileBO.model.centerX, tileController.tileBO.model.centerY, 0);
					if(editGUI.selection.transform.tag == GameObjectTags.TILE_TAG){
						Material material = editGUI.selection.transform.GetComponent<Renderer>().sharedMaterial;
						tile.GetComponent<Renderer>().sharedMaterial = material;
					} else if(editGUI.selection.transform.tag == GameObjectTags.PIECE_TAG){
						if(!hit.transform.GetComponent<PieceController>().pieceBO.model.prefabName.Equals(editGUI.selection.transform.GetComponent<PieceController>().pieceBO.model.prefabName)){
                            Debug.Log("Piece: " + tileController.piece.pieceBO.model.prefabName);
                            Debug.Log("Selection: " + editGUI.selection.transform.GetComponent<PieceController>().pieceBO.model.prefabName);
							hit.transform.GetComponent<PieceController>().destroyPiece();
                            tileController.piece.instantiatePiece(tileController.piece.pieceBO.model, editGUI.selection, centerPoint, editGUI.selection.transform.rotation);
						}
					}
					tilesCollection.addTile(tileController);
					Globals.Instance().DebugLog(this.GetType().Name, "Replace: Piece");
				}
//*****************MOVE
			} else if(editGUI.tool == MapEditorGUI.ToolType.MOVE){
				Debug.Log ("Move");
				if(hit.transform.tag == GameObjectTags.TILE_TAG && !isMoveSelect){
					Globals.Instance().DebugLog(this.GetType().Name, "Piece-Move Selected");
					moveSelection = hit.transform.gameObject;
					origColor = moveSelection.transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_Color");
					Color newColor = Colors.moveSelectionColor(origColor);
					moveSelection.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", newColor);
					isMoveSelect = true;
				} else if(hit.transform.tag == GameObjectTags.TILE_TAG && isMoveSelect){
					Debug.Log("Moved Tile");
					if(hit.transform.GetComponent<TileController>().piece == null){
						TileController tileController = hit.transform.GetComponent<TileController>();
						moveSelection.transform.position = new Vector3(tileController.tileBO.model.centerX, tileController.tileBO.model.centerY);
                        PieceData pieceData = tileController.piece.pieceBO.model;
                        PieceData selectionData = editGUI.selection.transform.GetComponent<PieceController>().pieceBO.model;
                        pieceData.id = selectionData.id;
                        pieceData.name = selectionData.name;
                        pieceData.prefabName = selectionData.prefabName;
						if(editGUI.mode == MapEditorGUI.ModeType.SELECT){
							moveSelection.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", origColor);
							moveSelection = null;
							isMoveSelect = false;
						}
					} else {
						Globals.Instance().DebugLog(this.GetType().Name, "Tile-Moved:Piece already at that position");
					}
				}
//**************ROTATE
			} else if(editGUI.tool == MapEditorGUI.ToolType.ROTATE){
				if(hit.transform.tag == GameObjectTags.PIECE_TAG){
					Globals.Instance().DebugLog(this.GetType().Name, "Piece-Rotate Piece");
					//hit.transform.rotation = Quaternion.Euler(-90, hit.transform.rotation.y + 90, 0);
                    hit.transform.gameObject.GetComponent<TileController>().piece.pieceBO.model.rotation += -90;
					hit.transform.Rotate(0, 0, -90, Space.World);
				}
			}
		}
	}

	private void enableMapEditorGUI(){
		editGUI.enabled = true;
	}

    private void setTile(TileController originalTileController, TileController settingTileController){

    }
}
